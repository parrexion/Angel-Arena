using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseProbabiltyTuple {
	public int probability;
	public ItemEquip.ItemType baseType;
}

[System.Serializable]
public class ModProbabiltyTuple {
	public int probability;
	public ItemModifier modifier;
}

public class ItemGenerator : MonoBehaviour {

	public ItemEntryReference selectedItem;
	public Sprite[] baseImages;

	[Header("Base Item")]
	public List<BaseProbabiltyTuple> greenBase;
	public List<BaseProbabiltyTuple> blueBase;
	public List<BaseProbabiltyTuple> purpleBase;

	[Header("Weapons")]
	public List<ModProbabiltyTuple> greenModWeapon;
	public List<ModProbabiltyTuple> blueModWeapon;
	public List<ModProbabiltyTuple> purpleModWeapon;

	[Header("Armor")]
	public List<ModProbabiltyTuple> greenModArmor;
	public List<ModProbabiltyTuple> blueModArmor;
	public List<ModProbabiltyTuple> purpleModArmor;

	[Header("Helms")]
	public List<ModProbabiltyTuple> greenModHelm;
	public List<ModProbabiltyTuple> blueModHelm;
	public List<ModProbabiltyTuple> purpleModHelm;

	[Header("Amulets")]
	public List<ModProbabiltyTuple> greenModAmulets;
	public List<ModProbabiltyTuple> blueModAmulets;
	public List<ModProbabiltyTuple> purpleModAmulets;

	[Header("Consumables")]
	public List<ModProbabiltyTuple> greenModConsumable;
	public List<ModProbabiltyTuple> blueModConsumable;
	public List<ModProbabiltyTuple> purpleModConsumable;



	public void Generate() {
		ItemEquip testItem = (ItemEquip)selectedItem.reference;
		if (testItem == null)
			return;

		testItem.ResetValues();
		testItem.icon = baseImages[0];
		testItem.item_type = ItemEntry.ItemType.WEAPON;
		testItem.tintColor = Random.ColorHSV(0,1,0.5f,1,0,1,1,1);
		testItem.moneyValue = 10;

		int attempts = 1;
		while (attempts > 0) {

			int roll = Random.Range(1,101);
			roll -= greenModWeapon[0].probability;
			ItemModifier selectedMod = greenModWeapon[0].modifier;
			int position = 1;

			while (roll > 0) {
				roll -= greenModWeapon[position].probability;
				selectedMod = greenModWeapon[position].modifier;
				position++;
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
}
