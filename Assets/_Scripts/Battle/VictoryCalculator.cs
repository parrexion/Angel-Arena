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
	public BattleEntryReference selectedBattle;
	public IntVariable noOfEnemies;
	public IntVariable totalExp;
	public IntVariable totalMoney;

	
	public void CalculateWinnings() {
		int gainedExp = selectedBattle.reference.enemy.exp * noOfEnemies.value;
		int gainedMoney = selectedBattle.reference.enemy.money * noOfEnemies.value;
		totalExp.value += gainedExp;
		totalMoney.value += gainedMoney;

		expText.text = "EXP gained:   " + gainedExp;
		moneyText.text = "Money gained:   " + gainedMoney;

		Debug.Log("############WIWNWIWNWINW");
		Debug.Log("EXP gained:   " + selectedBattle.reference.enemy.exp);
		Debug.Log("Money gained:   " + selectedBattle.reference.enemy.money);
		Debug.Log("EXP gained:   " + gainedExp);
		Debug.Log("Money gained:   " + gainedMoney);

		//Handle loot here
		lootObject.gameObject.SetActive(false);
	}
}
