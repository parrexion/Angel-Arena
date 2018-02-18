using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ItemEntry : ScriptableObject {

	public enum Rarity { GREEN, BLUE, PURPLE }
	public enum ItemType {WEAPON, ARMOR, HELM, AMULET, CONSUMABLE, RECIPE, DESTROY = 31}

	public string itemName;
	public ItemType type = ItemType.CONSUMABLE;
	public Rarity rarity;
	public SpriteEntry icon = null;
	public Color tintColor = Color.white;
	public int moneyValue = 0;
	public bool isRecipe = false;
	public bool bought = false;
	

	public int healthMod;
	public int manaMod;

	public int damageMod;      // Increase/decrease in damage
	public int armorMod;		// Increase/decrease in defense

	public int healthRegMod;
	public int manaRegMod;

	public int magicResMod;
	public int critMod;
	public int lifestealMod;


	/// <summary>
	/// Resets the values to default.
	/// </summary>
	public void ResetValues() {
		itemName = "";
		type = ItemType.RECIPE;
		icon = null;
		tintColor = Color.white;
		moneyValue = 0;
		bought = false;
		
		healthMod = 0;
		manaMod = 0;
		damageMod = 0;
		armorMod = 0;
		healthRegMod = 0;
		manaRegMod = 0;
		magicResMod = 0;
		critMod = 0;
		lifestealMod = 0;
	}

	/// <summary>
	/// Copies the values from another entry.
	/// </summary>
	/// <param name="other"></param>
	public void CopyValues(ItemEntry item) {
		itemName = item.itemName;
		type = item.type;
		icon = item.icon;
		tintColor = item.tintColor;
		moneyValue = item.moneyValue;
		bought = item.bought;
	}

	public SavedItem Save() {
		SavedItem data = new SavedItem();

		data.itemName = itemName;
		data.type = type;
		data.iconID = icon.uuid;
		data.rarity = rarity;
		data.tintColor = tintColor;
		data.moneyValue = moneyValue;
		data.isRecipe = isRecipe;
		data.bought = bought;
		
		data.healthMod = healthMod;
		data.manaMod = manaMod;
		data.damageMod = damageMod;
		data.armorMod = armorMod;
		data.healthRegMod = healthRegMod;
		data.manaRegMod = manaRegMod;
		data.magicResMod = magicResMod;
		data.critMod = critMod;
		data.lifestealMod = lifestealMod;

		return data;
	}

	public void Load(SavedItem data) {
		itemName = data.itemName;
		type = data.type;
		rarity = data.rarity;
		tintColor = data.tintColor;
		moneyValue = data.moneyValue;
		isRecipe = data.isRecipe;
		bought = data.bought;
		
		healthMod = data.healthMod;
		manaMod = data.manaMod;
		damageMod = data.damageMod;
		armorMod = data.armorMod;
		healthRegMod = data.healthRegMod;
		manaRegMod = data.manaRegMod;
		magicResMod = data.magicResMod;
		critMod = data.critMod;
		lifestealMod = data.lifestealMod;
	}
}

[System.Serializable]
/// <summary>
/// Class used to save an item to file.
/// </summary>
public class SavedItem {
	public bool isItem = false;
	public string itemName;
	public ItemEntry.ItemType type;
	public ItemEntry.Rarity rarity;
	public string iconID;
	public Color tintColor;
	public int moneyValue;
	public bool isRecipe;
	public bool bought;
	
	public int healthMod;
	public int manaMod;
	public int damageMod;
	public int armorMod;
	public int healthRegMod;
	public int manaRegMod;
	public int magicResMod;
	public int critMod;
	public int lifestealMod;
}
