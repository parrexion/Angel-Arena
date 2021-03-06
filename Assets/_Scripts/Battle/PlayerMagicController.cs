﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMagicController : MonoBehaviour {

	public PlayerActionController playerController;
	public EnemyController enemyController;
	public IntVariable playerCurrentMana;

	public SpellReference[] spells;
	public Button[] spellButtons;

	private int[] cooldowns;
	private Text[] buttonTexts;


	void Start() {
		ColorBlock cb = spellButtons[0].colors;
        cb.disabledColor = Color.white;

		cooldowns = new int[spells.Length];
		buttonTexts = new Text[spells.Length];
		for (int i = 0; i < cooldowns.Length; i++) {
			cooldowns[i] = 0;
			spellButtons[i].GetComponent<Image>().sprite = spells[i].reference.icon;
			buttonTexts[i] = spellButtons[i].GetComponentInChildren<Text>();
			if (!spells[i].reference.useable){
				spellButtons[i].colors = cb;
			}
		}

		UpdateSpells();
	}

	/// <summary>
	/// Counts down the cooldown of the spells.
	/// </summary>
	public void ReduceCooldowns() {
		for (int i = 0; i < spells.Length; i++) {
			cooldowns[i]--;
		}
		UpdateSpells();
	}

	/// <summary>
	/// Updates the icons for the spells with cooldown and mana cost.
	/// </summary>
	public void UpdateSpells() {
		for (int i = 0; i < spells.Length; i++) {
			buttonTexts[i].text = (cooldowns[i] > 0) ? cooldowns[i].ToString() : "";
			bool interact = spells[i].reference.useable && cooldowns[i] <= 0 && spells[i].reference.ManaCost(spells[i].level) <= playerCurrentMana.value;
			spellButtons[i].interactable = interact;

			ColorBlock cb = spellButtons[0].colors;
        	cb.disabledColor = (spells[i].reference.ManaCost(spells[i].level) <= playerCurrentMana.value) ? Color.white : new Color(0.4f,0.4f,1f,0.75f);
			spellButtons[i].colors = cb;
		}
	}

	/// <summary>
	/// Uses the selected spell on the target (if any).
	/// Different effects depending on type.
	/// </summary>
	/// <param name="target"></param>
	/// <param name="spellIndex"></param>
	public void UseSpell(int target, int spellIndex) {
		Debug.Log("Used magic no " + spellIndex + " on the enemy!");
		Spell spell = spells[spellIndex].reference;
		cooldowns[spellIndex] = spell.Cooldown(spells[spellIndex].level);
		playerController.LoseMana(spell.ManaCost(spells[spellIndex].level));
		switch (spell.spellType)
		{
			case Spell.SpellType.SINGLE:
				enemyController.TakeMagicDamage(target,spell.Damage(spells[spellIndex].level), spell.Stuns(spells[spellIndex].level));
				break;
			case Spell.SpellType.HEAL:
				playerController.HealDamage(spell.Heal(spells[spellIndex].level));
				break;
			case Spell.SpellType.FAMILIAR:
				playerController.CreateFamiliar(spell.Heal(spells[spellIndex].level));
				break;
		}
		UpdateSpells();
	}

	// public void SingleSpell(int target, int spellIndex) {
	// 	enemyController.TakeDamage(target,spells[spellIndex].reference.damage);
	// }

	// public void HealSpell(int spellIndex) {
	// 	playerController.HealDamage(spells[spellIndex].reference.heal);
	// }

	// public void Familiar(int spellIndex) {
	// 	playerController.CreateFamiliar(spells[spellIndex].reference.heal);
	// }
}
