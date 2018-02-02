using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EnemyController : MonoBehaviour {

	public IntVariable currentBattleState;
	public EnemyUIController enemyUIController;
	public PlayerActionController player;
	public Transform damageNumber;
	public IntVariable noOfEnemies;
	public UnityEvent nextPhaseEvent;

	[Header("Enemies")]
	public BattleEntry battleEntry;
	public Transform[] enemyTransforms;
	public Button[] targetButtons;

	private int currentEnemy;
	private int[] enemyHealth;


	// Use this for initialization
	void Start() {
		InitializeEnemies();
	}

	void InitializeEnemies() {
		Debug.Log("Init");
		currentEnemy = 0;
		noOfEnemies.value = Random.Range(battleEntry.noOfEnemiesMin, battleEntry.noOfEnemiesMax+1);
		enemyHealth = new int[noOfEnemies.value];
		for (int i = 0; i < enemyTransforms.Length; i++) {
			if (i >= noOfEnemies.value) {
				enemyTransforms[i].gameObject.SetActive(false);
				targetButtons[i].gameObject.SetActive(false);
				continue;
			}
			enemyTransforms[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = battleEntry.enemy.color;
			enemyHealth[i] = battleEntry.enemy.health;
			enemyUIController.UpdateValue(i, 1);
		}
	}

	public void EnemyAttacks() {
		
		//Have we won?
		if (CheckWin()) {
			currentBattleState.value = 9;
			nextPhaseEvent.Invoke();
			return;
		}

		FindNextEnemy();

		if (currentEnemy < enemyHealth.Length) {
			Debug.Log("Enemy attacked the player!");
			player.TakeDamage(battleEntry.enemy.attack);
			currentEnemy++;
			FindNextEnemy();
		}

		if (currentEnemy >= noOfEnemies.value) {
			currentBattleState.value = 1;
			currentEnemy = 0;
			FindNextEnemy();
		}
		nextPhaseEvent.Invoke();
	}

	void FindNextEnemy() {
		while (currentEnemy < enemyHealth.Length && enemyHealth[currentEnemy] <= 0) {
			currentEnemy++;
			Debug.Log("Skipping....");
		}
	}

	public void TakeDamage(int enemyIndex, int damage) {
		int realDamage = Mathf.Max(0, damage - battleEntry.enemy.defense);
		enemyHealth[enemyIndex] -= realDamage;
		float value = (float)enemyHealth[enemyIndex] / (float)battleEntry.enemy.health;
		enemyUIController.UpdateValue(enemyIndex, value);
		Debug.Log("Player hurt enemy " + enemyIndex + " for " + realDamage + " damage.");
		Transform dmg = Instantiate(damageNumber, enemyTransforms[enemyIndex].position, Quaternion.identity);
		dmg.GetComponent<DamageNumberDisplay>().damage = realDamage;

		if (enemyHealth[enemyIndex] <= 0)
			Die(enemyIndex);
	}

	void Die(int index) {
		Debug.Log("Enemy " + index + " died!");
		enemyTransforms[index].gameObject.SetActive(false);
		targetButtons[index].gameObject.SetActive(false);
	}

	public bool CheckWin() {
		//Have we won?
		bool alive = false;
		for (int i = 0; i < enemyHealth.Length; i++) {
			if (enemyHealth[i] > 0) {
				Debug.Log("Someone is still alive!");
				alive = true;
				break;
			}
		}
		return !alive;
	}
}
