using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEntry : ScrObjLibraryEntry {

	public enum ItemType {WEAPON, ARMOR, HELM, AMULET, CONSUMABLE, DESTROY}
	public ItemType item_type = ItemType.CONSUMABLE;
	public Sprite icon = null;
	public Color tintColor = Color.white;
	public int moneyValue = 0;
	

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
	public override void ResetValues() {
		base.ResetValues();

		item_type = ItemType.CONSUMABLE;
		icon = null;
		tintColor = Color.white;
		moneyValue = 0;
		
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
	public override void CopyValues(ScrObjLibraryEntry other) {
		base.CopyValues(other);
		ItemEntry item = (ItemEntry)other;

		item_type = item.item_type;
		icon = item.icon;
		tintColor = item.tintColor;
		moneyValue = item.moneyValue;
	}
}
