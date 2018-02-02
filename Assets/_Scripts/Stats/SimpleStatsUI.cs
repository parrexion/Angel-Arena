using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Class for displaying the information of the selected item.
/// </summary>
public class SimpleStatsUI : MonoBehaviour {

	public Transform[] statsTextList;
	//public Transform[] modifierTextList;

	//Player stats
	public IntVariable playerLevel;
	public IntVariable playerMaxHP;
	public IntVariable playerCurrentHP;
	public IntVariable playerExp;
	public IntVariable playerMoney;

	private Text[] names;
	private Text[] values;


	void Start () {
		//Set up the texts showing the stats
		Text[] t;
		names = new Text[statsTextList.Length];
		values = new Text[statsTextList.Length];
		for (int i = 0; i < statsTextList.Length; i++) {
			t = statsTextList[i].GetComponentsInChildren<Text>();
			names[i] = t[0];
			values[i] = t[1];
		}

	}

	void Update () {
		//Update values
		UpdateValues();
	}

	/// <summary>
	/// Updates the information text of the currently selected item.
	/// </summary>
	void UpdateValues() {
		values[0].text = playerLevel.value.ToString();
		values[1].text = playerCurrentHP.value.ToString() + " / " + playerMaxHP.value.ToString();
		values[2].text = playerExp.value.ToString();
		values[3].text = playerMoney.value.ToString();
	}
}
