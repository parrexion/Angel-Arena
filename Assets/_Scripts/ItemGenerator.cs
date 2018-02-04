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
	public Sprite[] baseImages;
	public BattleEntry.Difficulty difficulty;

	[Header("Item Rarity")]
	public RandomRarityListVariable[] rarityList;

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



	public void Generate() {
		ItemEntry testItem = selectedItem.reference;
		if (testItem == null)
			return;
		
		int rarity = (int)rarityList[(int)difficulty].GetRandomItem();
		ItemEntry.ItemType type = typeList[rarity].GetRandomItem();

		testItem.ResetValues();
		testItem.type = type;
		testItem.rarity = (ItemEntry.Rarity)rarity;
		testItem.tintColor = Random.ColorHSV(0,1,0.5f,1,0,1,1,1);

		switch (type)
		{
			case ItemEntry.ItemType.AMULET:
				testItem.entryName = "AMULET";
				testItem.icon = baseImages[3];
				break;
			case ItemEntry.ItemType.ARMOR:
				testItem.entryName = "ARMOR";
				testItem.icon = baseImages[1];
				break;
			case ItemEntry.ItemType.CONSUMABLE:
				testItem.entryName = "CONSUMABLE";
				testItem.icon = baseImages[4];
				break;
			case ItemEntry.ItemType.HELM:
				testItem.entryName = "HELM";
				testItem.icon = baseImages[2];
				break;
			case ItemEntry.ItemType.WEAPON:
				testItem.entryName = "WEAPON";
				testItem.icon = baseImages[0];
				break;
		}
		ColorByRarity(testItem);

		int attempts = 1;
		while (attempts > 0) {
			ItemModifier selectedMod = null;
			switch (type)
			{
				case ItemEntry.ItemType.AMULET:
					selectedMod = modAmulet[rarity].GetRandomItem();
					testItem.moneyValue = 30;
					break;
				case ItemEntry.ItemType.ARMOR:
					selectedMod = modArmor[rarity].GetRandomItem();
					testItem.moneyValue = 20;
					break;
				case ItemEntry.ItemType.CONSUMABLE:
					// selectedMod = modConsumable[rarity].GetRandomItem();
					selectedMod = modArmor[2].list[3].modifier;
					testItem.moneyValue = 0;
					break;
				case ItemEntry.ItemType.HELM:
					selectedMod = modHelm[rarity].GetRandomItem();
					testItem.moneyValue = 10;
					break;
				case ItemEntry.ItemType.WEAPON:
					selectedMod = modWeapon[rarity].GetRandomItem();
					testItem.moneyValue = 10;
					break;
			}
			

			if (selectedMod == null){
				Debug.Log("Lucky you!");
				attempts++;
				continue;
			}

			testItem.moneyValue += selectedMod.cost;
			testItem.healthMod += selectedMod.health;
			testItem.manaMod += selectedMod.mana;
			testItem.damageMod += selectedMod.damage;
			testItem.armorMod += selectedMod.armor;
			testItem.healthRegMod += selectedMod.healthReg;
			testItem.manaRegMod += selectedMod.manaReg;
			testItem.magicResMod += selectedMod.magicRes;
			testItem.critMod += selectedMod.crit;
			testItem.lifestealMod += selectedMod.lifesteal;

			attempts--;
		}
	}

	void ColorByRarity(ItemEntry item) {
		switch (item.rarity)
		{
		case ItemEntry.Rarity.GREEN:
			item.entryName = "<color=#3BD248FF>" + item.entryName + "</color>";
			break;
		case ItemEntry.Rarity.BLUE:
			item.entryName = "<color=#2F5DE5FF>" + item.entryName + "</color>";
			break;
		case ItemEntry.Rarity.PURPLE:
			item.entryName = "<color=#F91DF3FF>" + item.entryName + "</color>";
			break;
		}
	}
}
