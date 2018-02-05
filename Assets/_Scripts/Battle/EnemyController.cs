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
	public UnityEvent nextPhaseEvent;

	[Header("Enemies")]
	public BattleEntryReference selectedBattle;
	public IntVariable noOfEnemies;
	public Transform[] enemyTransforms;
	public Button[] targetButtons;

	private int currentEnemy;
	public int[] enemyHealth;

	public Text enemyTypeText;


	// Use this for initialization
	void Start() {
		InitializeEnemies();
	}

	void InitializeEnemies() {
		Debug.Log("Init");
		enemyTypeText.text = selectedBattle.reference.entryName;
		currentEnemy = 0;
		noOfEnemies.value = selectedBattle.reference.GetRandomNumber();
		enemyHealth = new int[noOfEnemies.value];
		for (int i = 0; i < enemyTransforms.Length; i++) {
			if (i >= noOfEnemies.value) {
				enemyTransforms[i].gameObject.SetActive(false);
				targetButtons[i].gameObject.SetActive(false);
				continue;
			}
			enemyTransforms[i].GetChild(0).GetComponent<SpriteRenderer>().sprite = selectedBattle.reference.enemy.color;
			enemyHealth[i] = selectedBattle.reference.enemy.health;
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
		int damage = selectedBattle.reference.enemy.damage;
		int crit = Random.Range(0, 100);
		if (crit < selectedBattle.reference.enemy.crit)
			damage *= 3;

		if (currentEnemy < enemyHealth.Length) {
			Debug.Log("Enemy attacked the player!");
			player.TakePhysicalDamage(selectedBattle.reference.enemy.damage);
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

	/// <summary>
	/// Returns the index of the first alive enemy.
	/// </summary>
	/// <returns></returns>
	public int FindFirstEnemy(){
		for (int i = 0; i < enemyHealth.Length; i++) {
			if (enemyHealth[i] > 0)
				return i;
		}
		return -1;
	}

	public void TakeMagicDamage(int enemyIndex, int damage) {
		Transform dmg;

		int magicRes = Random.Range(0, 100);
		if (magicRes < selectedBattle.reference.enemy.magicResist) {
			Debug.Log("Enemy " + enemyIndex + " resisted the magic!");
			dmg = Instantiate(damageNumber, enemyTransforms[enemyIndex].position, Quaternion.identity);
			dmg.GetComponent<DamageNumberDisplay>().damage = 0;
			dmg.GetComponent<DamageNumberDisplay>().dodged = true;
			return;
		}

		enemyHealth[enemyIndex] -= damage;
		float value = (float)enemyHealth[enemyIndex] / (float)selectedBattle.reference.enemy.health;
		enemyUIController.UpdateValue(enemyIndex, value);
		Debug.Log("Player hurt enemy " + enemyIndex + " for " + damage + " magic damage.");
		dmg = Instantiate(damageNumber, enemyTransforms[enemyIndex].position, Quaternion.identity);
		dmg.GetComponent<DamageNumberDisplay>().damage = damage;
		dmg.GetComponent<DamageNumberDisplay>().dodged = false;

		if (enemyHealth[enemyIndex] <= 0)
			Die(enemyIndex);
	}

	public int TakePhysicalDamage(int enemyIndex, int damage) {
		Transform dmg;

		int dodge = Random.Range(0, 100);
		if (dodge < selectedBattle.reference.enemy.dodge) {
			Debug.Log("Enemy " + enemyIndex + " dodged the attack!");
			dmg = Instantiate(damageNumber, enemyTransforms[enemyIndex].position, Quaternion.identity);
			dmg.GetComponent<DamageNumberDisplay>().damage = 0;
			dmg.GetComponent<DamageNumberDisplay>().dodged = true;
			return 0;
		}

		int realDamage = Mathf.Max(0, damage - selectedBattle.reference.enemy.armor);
		if (realDamage <= 0) {
			Debug.Log("Enemy " + enemyIndex + " resisted the attack!");
			dmg = Instantiate(damageNumber, enemyTransforms[enemyIndex].position, Quaternion.identity);
			dmg.GetComponent<DamageNumberDisplay>().damage = realDamage;
			dmg.GetComponent<DamageNumberDisplay>().dodged = false;
			return 0;
		}
		enemyHealth[enemyIndex] -= realDamage;
		float value = (float)enemyHealth[enemyIndex] / (float)selectedBattle.reference.enemy.health;
		enemyUIController.UpdateValue(enemyIndex, value);
		Debug.Log("Player hurt enemy " + enemyIndex + " for " + realDamage + " damage.");
		dmg = Instantiate(damageNumber, enemyTransforms[enemyIndex].position, Quaternion.identity);
		dmg.GetComponent<DamageNumberDisplay>().damage = realDamage;
		dmg.GetComponent<DamageNumberDisplay>().dodged = false;

		if (enemyHealth[enemyIndex] <= 0)
			Die(enemyIndex);
		return realDamage;
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
