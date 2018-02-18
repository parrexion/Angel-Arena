using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour {

	public enum BattleState { START, PLAYER, ENEMY, ENDROUND, LOSE, WIN }
	public const int VICTORY_INT = 9;
	public const int DEFEAT_INT = 10;

	public float delay = 1f;

	public IntVariable currentBattleState;
	public PlayerMagicController magicController;
	public PlayerStatsController statsController;
	public Canvas playerActions;
	public Canvas victoryCanvas;
	public VictoryCalculator victoryCalc;
	public Text bigText;

	public IntVariable totalExp;
	public IntVariable totalMoney;

	public UnityEvent playerTurnEvent;
	public UnityEvent enemyTurnEvent;


	// Use this for initialization
	void Start() {
		currentBattleState.value = 0;
		TriggerState();
	}

	public void ReturnToMap() {
		SceneManager.LoadScene("Map");
	}
	
	// Update is called once per frame
	public void TriggerState() {

		playerActions.enabled = false;
		switch (currentBattleState.value)
		{
			case 0:
				StartCoroutine(Setup());
				break;
			case 1:
				PrePlayerTurn();
				break;
			case 2:
				StartCoroutine(PlayerTurn());
				break;
			case 3:
				PreEnemyTurn();
				break;
			case 4:
				StartCoroutine(EnemyTurn());
				break;
			case VICTORY_INT:
				StartCoroutine(Victory());
				break;
			case DEFEAT_INT:
				StartCoroutine(Defeat());
				break;
		}
	}

	IEnumerator Setup() {
		currentBattleState.value = 1;
		victoryCanvas.enabled = false;
		statsController.CalculateSpellPassives(true);

		TriggerState();
		yield break;
	}

	void PrePlayerTurn() {
		magicController.ReduceCooldowns();
		currentBattleState.value = 2;
		TriggerState();
	}

	IEnumerator PlayerTurn() {
		yield return new WaitForSeconds(delay);
		bigText.text = "PLAYER";
		playerTurnEvent.Invoke();
		yield break;
	}

	void PreEnemyTurn() {
		currentBattleState.value = 4;
		TriggerState();
	}

	IEnumerator EnemyTurn() {
		yield return new WaitForSeconds(delay);
		bigText.text = "ENEMY";
		enemyTurnEvent.Invoke();

		yield break;
	}

	IEnumerator Victory() {
		bigText.text = "VICTORY";
		statsController.CalculateSpellPassives(false);
		yield return new WaitForSeconds(delay);
		bigText.text = "";
		victoryCalc.CalculateWinnings();
		victoryCanvas.enabled = true;
		yield break;
	}

	IEnumerator Defeat() {
		bigText.text = "YOU DIED";
		statsController.CalculateSpellPassives(false);
		yield return new WaitForSeconds(4f);
		SceneManager.LoadScene("Town");
	}
}
