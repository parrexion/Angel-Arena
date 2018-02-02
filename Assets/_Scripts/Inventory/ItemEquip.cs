using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "LibraryEntries/ItemEquip")]
public class ItemEquip : ItemEntry {

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

}
