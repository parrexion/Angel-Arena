using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsController : MonoBehaviour {

	[Header("Gains")]
	public IntVariable level;
	public IntVariable totalExp;
	public IntVariable totalMoney;

	[Header("Player Stats")]
	public IntVariable currentHP;
	public IntVariable currentMana;
	public IntVariable maxHP;
	public IntVariable maxMana;

	public IntVariable damage;
	public IntVariable armor;

	public IntVariable healthReg;
	public IntVariable manaReg;

	public IntVariable magicRes;
	public IntVariable crit;
	public IntVariable lifesteal;
	public IntVariable manaKill;

	[Header("Spells")]
	public SpellReference[] spells;

	[Header("Inventory")]
	public InvListVariable equippedItems;

	List<Buff> activeBuffs = new List<Buff>();


	public void ResetStats() {
		maxHP.value = 90 + 10 * level.value;
		maxMana.value = 45 + 5 * level.value;
		currentHP.value = maxHP.value;
		currentMana.value = maxMana.value;

		damage.value = 10;
		armor.value = 0;

		healthReg.value = 0;
		manaReg.value = 0;

		magicRes.value = 0;
		crit.value = 0;
		lifesteal.value = 0;
		manaKill.value = 0;
	}

	public void CalculateSats() {
		ResetStats();

		for (int i = 0; i < equippedItems.values.Length; i++) {
			if (equippedItems.values[i] == null)
				continue;

			ItemEntry item = equippedItems.values[i];
			maxHP.value += item.healthMod;
			maxMana.value += item.manaMod;
			currentHP.value = maxHP.value;
			currentMana.value = maxMana.value;

			damage.value += item.damageMod;
			armor.value += item.armorMod;

			healthReg.value += item.healthRegMod;
			manaReg.value += item.manaRegMod;

			magicRes.value += item.magicResMod;
			crit.value += item.critMod;
			lifesteal.value += item.lifestealMod;
		}
	}

	public static bool PercentChanceTrigger(int chance) {
		int res = Random.Range(0, 100);
		return (res < chance);
	}

	public void CalculateSpellPassives(bool addSpells) {
		for (int i = 0; i < spells.Length; i++) {
			if (spells[i].reference.spellType != Spell.SpellType.PASSIVE) 
				continue;
			int level = spells[i].level -1;
			if (addSpells)
				AddModifier(spells[i].reference.buffModifiers[level]);
			else
				RemoveModifier(spells[i].reference.buffModifiers[level]);
		}
		if (!addSpells)
			ResetBuffs();
	}

	public void ManaKillGained() {
		currentMana.value = Mathf.Min(maxMana.value, currentMana.value + manaKill.value); 
	}

	// BUFFS

	public void AddNewBuff(int duration, ItemModifier modifier) {
		Buff buff = new Buff();
		buff.durationLeft = duration;
		buff.modifier = modifier;
		activeBuffs.Add(buff);
		AddModifier(modifier);
	}

	public void ResetBuffs() {
		for (int i = 0; i < activeBuffs.Count; i++) {
			RemoveModifier(activeBuffs[i].modifier);
		}
		activeBuffs = new List<Buff>();
	}

	public void UpdateBuffs() {
		List<int> removeList = new List<int>();
		bool stillActive;
		for (int i = 0; i < activeBuffs.Count; i++) {
			stillActive = activeBuffs[i].UpdateCooldown();
			if (!stillActive)
				removeList.Add(i);
		}
		for (int i = removeList.Count-1; i >= 0; i--) {
			activeBuffs.RemoveAt(removeList[i]);
		}
	}

	void AddModifier(ItemModifier mod) {
		CalculateBuff(mod, 1);
	}

	void RemoveModifier(ItemModifier mod) {
		CalculateBuff(mod,-1);
	}

	void CalculateBuff(ItemModifier mod, int dir) {
		damage.value = Mathf.Max(0, damage.value + dir * mod.damage);
		armor.value = Mathf.Max(0, armor.value + dir * mod.armor);

		manaKill.value = Mathf.Max(0, manaKill.value + dir * mod.manaKill);
	}
}
