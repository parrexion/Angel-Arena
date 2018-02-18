using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TownTimeUI : MonoBehaviour {

	public IntVariable playtime;
	public Text timeText;


	// Use this for initialization
	void Start () {
		SetTimeString();
	}
	
	void SetTimeString() {
		int days = playtime.value / 10 + 1;
		timeText.text = "Day  " + days;
	}
}
