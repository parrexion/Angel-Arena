using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// Class for displaying the information of the selected item.
/// </summary>
public class SelectedSpellUI : MonoBehaviour {

	[Header("Selected spell")]
	public SpellReference selectedSpell;
	private Spell currentSpell;
	public Text nameText;
	public Image spellIcon;
	public Text levelText;
	public Text descriptionText;

	[Header("Leveling stuff")]
	public IntVariable playerLevel;
	public Button levelupButton;
	public BoolVariable levelupMode;


	void OnEnable() {
		selectedSpell.reference = null;
		levelupButton.interactable = true; //TODO - levelup exp req.
	}

	void Update () {
		UpdateValues();
	}

	/// <summary>
	/// Updates the information text of the currently selected item.
	/// </summary>
	void UpdateValues(){

		if (selectedSpell.reference != null) {
			currentSpell = selectedSpell.reference;
			nameText.text = currentSpell.spellName;
			spellIcon.sprite = currentSpell.icon;
			spellIcon.enabled = true;
			levelText.text = "Level " + selectedSpell.level;
			if (levelupMode.value && selectedSpell.reference.CanLevelup(playerLevel.value, selectedSpell.level))
				descriptionText.text = currentSpell.generateSpellDescription(Spell.DetailMode.LEVELUP, selectedSpell.level);
			else
				descriptionText.text = currentSpell.generateSpellDescription(Spell.DetailMode.SIMPLE, selectedSpell.level);
		}
		else {
			nameText.text = "";
			spellIcon.enabled = false;
			levelText.text = "";
			descriptionText.text = "";
		}
	}
}
