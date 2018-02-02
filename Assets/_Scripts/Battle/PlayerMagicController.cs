using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMagicController : MonoBehaviour {

	public PlayerActionController playerController;
	public EnemyController enemyController;
	public IntVariable playerCurrentMana;

	public Spell[] spells;
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
			spellButtons[i].GetComponent<Image>().sprite = spells[i].icon;
			buttonTexts[i] = spellButtons[i].GetComponentInChildren<Text>();
			if (!spells[i].useable){
				spellButtons[i].colors = cb;
			}
		}

		UpdateSpells();
	}

	public void ReduceCooldowns() {
		for (int i = 0; i < spells.Length; i++) {
			cooldowns[i]--;
		}
		UpdateSpells();
	}

	public void UpdateSpells() {
		for (int i = 0; i < spells.Length; i++) {
			buttonTexts[i].text = (cooldowns[i] > 0) ? cooldowns[i].ToString() : "";
			bool interact = spells[i].useable && cooldowns[i] <= 0 && spells[i].manaCost <= playerCurrentMana.value;
			spellButtons[i].interactable = interact;

			ColorBlock cb = spellButtons[0].colors;
        	cb.disabledColor = (spells[i].manaCost <= playerCurrentMana.value) ? Color.white : new Color(0.4f,0.4f,1f,0.75f);
			spellButtons[i].colors = cb;
		}
	}

	public void UseSpell(int target, int spellIndex) {
		Debug.Log("Used magic no " + spellIndex + " on the enemy!");
		cooldowns[spellIndex] = spells[spellIndex].cooldown;
		enemyController.TakeDamage(target,spells[spellIndex].damage);
		playerController.LoseMana(spells[spellIndex].manaCost);
		UpdateSpells();
	}
}
