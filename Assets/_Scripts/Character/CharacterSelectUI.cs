using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelectUI : MonoBehaviour {

	//Selected character
	public CharacterReference selectedCharacter;
	public SpellReference selectedSpell;
	private PlayerCharacter currentCharacter;
	public Text nameText;
	public Image characterIcon;
	public Text descriptionText;
	public Image[] spellImages;
	public string nextScene;

	[Header("Player stats")]
	public IntVariable playtime;
	public IntVariable playerLevel;
	public IntVariable totalExp;
	public IntVariable totalMoney;
	public SpellReference[] playerSpells = new SpellReference[4];


	void OnEnable() {
		selectedCharacter.reference = null;
	}

	void Update() {
		//Update values
		UpdateValues();
	}

	/// <summary>
	/// Updates the information text of the currently selected item.
	/// </summary>
	void UpdateValues() {
		if (selectedCharacter.reference != null) {
			currentCharacter = selectedCharacter.reference;
			nameText.text = currentCharacter.entryName;
			characterIcon.sprite = currentCharacter.icon;
			characterIcon.enabled = true;
			if (selectedSpell.reference != null) {
				descriptionText.text = selectedSpell.reference.generateSpellDescription(Spell.DetailMode.DESC,1);
			}
			else
				descriptionText.text = currentCharacter.description;
			
			for (int i = 0; i < spellImages.Length; i++) {
				spellImages[i].sprite = currentCharacter.spells[i].icon;
				spellImages[i].enabled = true;
			}
		}
		else {
			nameText.text = "";
			characterIcon.enabled = false;
			descriptionText.text = "";
			for (int i = 0; i < spellImages.Length; i++) {
				spellImages[i].enabled = false;
			}
		}
	}


	public void SelectCharacter() {
		ResetPlayer();

		for (int i = 0; i < playerSpells.Length; i++) {
			playerSpells[i].reference = selectedCharacter.reference.spells[i];
			playerSpells[i].level = 0;
		}

		SceneManager.LoadScene(nextScene);
	}

	void ResetPlayer() {
		playtime.value = 0;
		playerLevel.value = 0;
		totalExp.value = 0;
		totalMoney.value = 0;
	}
}
