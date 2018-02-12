using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for displaying the information of the selected item.
/// </summary>
public class SelectedItemUI : MonoBehaviour {

	[Header("Settings")]
	public bool showTotal;
	public bool showChanges;

	[Header("Transforms")]
	public Transform[] statsTextList;
	public IntVariable[] playerStats;

	private Text[] names;
	private Text[] values;

	[Header("Selected Item")]
	public ItemEntryReference selectedItem;
	public Text itemName;
	public Text itemType;
	public Image itemIcon;

	private ItemEntry currentItem;
	private Text[] changes;


	void Start () {
		//Set up the texts showing the stats
		Text[] t;
		names = new Text[statsTextList.Length];
		values = new Text[statsTextList.Length];
		changes = new Text[statsTextList.Length];
		for (int i = 0; i < statsTextList.Length; i++) {
			t = statsTextList[i].GetComponentsInChildren<Text>();
			names[i] = t[0];
			values[i] = t[1];
			changes[i] = t[2];
			values[i].text = "";
			changes[i].text = "";
		}
	}

	void OnEnable() {
		selectedItem.reference = null;
	}

	void Update () {
		//Update values
		UpdateValues();
	}

	/// <summary>
	/// Updates the information text of the currently selected item.
	/// </summary>
	void UpdateValues(){

		if (showTotal) {
			values[0].text = playerStats[0].value.ToString();
			values[1].text = playerStats[1].value.ToString();
			values[2].text = playerStats[2].value.ToString();
			values[3].text = playerStats[3].value.ToString();
			values[4].text = playerStats[4].value.ToString();
			values[5].text = playerStats[5].value.ToString();
			values[6].text = playerStats[6].value + "%";
			values[7].text = playerStats[7].value + "%";
			values[8].text = playerStats[8].value + "%";
		}

		if (!showChanges)
			return;

		if (selectedItem.reference != null) {
			currentItem = selectedItem.reference;
			itemName.text = currentItem.entryName;
			itemType.text = currentItem.type.ToString();
			itemIcon.sprite = currentItem.icon;
			itemIcon.color = currentItem.tintColor;
			itemIcon.enabled = true;
			
			changes[0].text = (currentItem.healthMod != 0) ? "+" + currentItem.healthMod.ToString() : "";
			changes[1].text = (currentItem.manaMod != 0) ? "+" + currentItem.manaMod.ToString() : "";
			changes[2].text = (currentItem.damageMod != 0) ? "+" + currentItem.damageMod.ToString() : "";
			changes[3].text = (currentItem.armorMod != 0) ? "+" + currentItem.armorMod.ToString() : "";
			changes[4].text = (currentItem.healthRegMod != 0) ? "+" + currentItem.healthRegMod.ToString() : "";
			changes[5].text = (currentItem.manaRegMod != 0) ? "+" + currentItem.manaRegMod.ToString() : "";
			changes[6].text = (currentItem.magicResMod != 0) ? "+" + currentItem.magicResMod.ToString() : "";
			changes[7].text = (currentItem.critMod != 0) ? "+" + currentItem.critMod.ToString() : "";
			changes[8].text = (currentItem.lifestealMod != 0) ? "+" + currentItem.lifestealMod.ToString() : "";
		}
		else {
			itemName.text = "";
			itemType.text = "";
			changes[0].text = "";
			changes[1].text = "";
			changes[2].text = "";
			itemIcon.enabled = false;
		}
	}
}
