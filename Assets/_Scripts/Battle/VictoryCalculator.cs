using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryCalculator : MonoBehaviour {

	[Header("Objects")]
	public Text expText;
	public Text moneyText;
	public Transform lootObject;

	[Header("Values")]
	public BattleEntry battleEntry;
	public IntVariable noOfEnemies;
	public IntVariable totalExp;
	public IntVariable totalMoney;


	// Use this for initialization
	void OnEnable() {
		CalculateWinnings();
	}
	

	void CalculateWinnings() {
		int gainedExp = battleEntry.enemy.exp * noOfEnemies.value;
		int gainedMoney = battleEntry.enemy.money * noOfEnemies.value;
		totalExp.value += gainedExp;
		totalMoney.value += gainedMoney;

		expText.text = "EXP gained:   " + gainedExp;
		moneyText.text = "Money gained:   " + gainedMoney;

		//Handle loot here
		lootObject.gameObject.SetActive(false);
	}
}
