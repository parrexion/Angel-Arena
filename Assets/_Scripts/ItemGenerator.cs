using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RarityProbabiltyTuple {
	public int probability;
	public ItemEntry.Rarity rarity;
}

[System.Serializable]
public class BaseProbabiltyTuple {
	public int probability;
	public ItemEntry.ItemType baseType;
}

[System.Serializable]
public class ModProbabiltyTuple {
	public int probability;
	public ItemModifier modifier;
}


public class ItemGenerator : MonoBehaviour {

	public ItemEntryReference selectedItem;
	public SpriteEntry[] baseImages;
	public BattleEntry.Difficulty difficulty;

	[Header("Item Rarity")]
	public RandomRarityListVariable[] rarityList;
	public RandomRarityListVariable levelRarityList;

	[Header("Base Item")]
	public RandomTypeListVariable[] typeList;

	[Header("Weapons")]
	public RandomModListVariable[] modWeapon;

	[Header("Armor")]
	public RandomModListVariable[] modArmor;

	[Header("Helms")]
	public RandomModListVariable[] modHelm;

	[Header("Amulets")]
	public RandomModListVariable[] modAmulet;

	[Header("Consumables")]
	public RandomModListVariable[] modConsumable;


	public ItemEntry GenerateBattle(BattleEntry.Difficulty difficulty) {
		int rarity = (int)rarityList[(int)difficulty].GetRandomItem();
		return Generate(rarity, false);
	}

	public ItemEntry GenerateLevel(int level) {
		int chance = 100 + level * 10;

		int rarity = Random.Range(0,chance) / 100;
		return Generate(rarity, true);
	}

	ItemEntry Generate(int rarity, bool bought) {
		ItemEntry item = (ItemEntry)ScriptableObject.CreateInstance("ItemEntry");
		ItemEntry.ItemType type = typeList[rarity].GetRandomItem();

		item.ResetValues();
		item.bought = bought;
		item.isRecipe = true;
		item.itemName = type.ToString();
		item.type = type;
		item.icon = baseImages[5];
		item.rarity = (ItemEntry.Rarity)rarity;
		item.tintColor = Random.ColorHSV(0,1,0.5f,1,0,1,1,1);

		ColorByRarity(item);

		int attempts = 1;
		while (attempts > 0) {
			ItemModifier selectedMod = null;
			switch (type)
			{
				case ItemEntry.ItemType.AMULET:
					selectedMod = modAmulet[rarity].GetRandomItem();
					item.moneyValue = 30;
					break;
				case ItemEntry.ItemType.ARMOR:
					selectedMod = modArmor[rarity].GetRandomItem();
					item.moneyValue = 20;
					break;
				case ItemEntry.ItemType.CONSUMABLE:
					selectedMod = modConsumable[rarity].GetRandomItem();
					item.moneyValue = 0;
					break;
				case ItemEntry.ItemType.HELM:
					selectedMod = modHelm[rarity].GetRandomItem();
					item.moneyValue = 10;
					break;
				case ItemEntry.ItemType.WEAPON:
					selectedMod = modWeapon[rarity].GetRandomItem();
					item.moneyValue = 10;
					break;
			}
			
			if (selectedMod == null){
				Debug.Log("Lucky you!");
				attempts++;
				continue;
			}

			item.moneyValue += selectedMod.cost;
			item.healthMod += selectedMod.health;
			item.manaMod += selectedMod.mana;
			item.damageMod += selectedMod.damage;
			item.armorMod += selectedMod.armor;
			item.healthRegMod += selectedMod.healthReg;
			item.manaRegMod += selectedMod.manaReg;
			item.magicResMod += selectedMod.magicRes;
			item.critMod += selectedMod.crit;
			item.lifestealMod += selectedMod.lifesteal;

			attempts--;
		}

		Debug.Log("Generated a recipe");

		return item;
	}

	/// <summary>
	/// Colors the item name according to rarity.
	/// </summary>
	/// <param name="item"></param>
	void ColorByRarity(ItemEntry item) {
		switch (item.rarity)
		{
		case ItemEntry.Rarity.GREEN:
			item.itemName = "<color=#3BD248FF>" + item.itemName + "</color>";
			break;
		case ItemEntry.Rarity.BLUE:
			item.itemName = "<color=#2F5DE5FF>" + item.itemName + "</color>";
			break;
		case ItemEntry.Rarity.PURPLE:
			item.itemName = "<color=#F91DF3FF>" + item.itemName + "</color>";
			break;
		}
	}

	/// <summary>
	/// Takes an item which is a recipe and changes it into the corresponding item.
	/// </summary>
	public void CreateFromRecipe() {

		ItemEntry item = selectedItem.reference;
		if (item == null || !item.isRecipe)
			return;

		item.isRecipe = false;
		item.bought = false;
		ItemEntry.ItemType type = item.type;

		switch (type)
		{
			case ItemEntry.ItemType.AMULET:
				item.icon = baseImages[3];
				break;
			case ItemEntry.ItemType.ARMOR:
				item.icon = baseImages[1];
				break;
			case ItemEntry.ItemType.CONSUMABLE:
				item.icon = baseImages[4];
				break;
			case ItemEntry.ItemType.HELM:
				item.icon = baseImages[2];
				break;
			case ItemEntry.ItemType.WEAPON:
				item.icon = baseImages[0];
				break;
		}
	}
}
