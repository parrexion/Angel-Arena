using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerActionController : MonoBehaviour {

	public enum AttackType { NONE = 0, MAGIC1 = 1, MAGIC2 = 2, MAGIC3 = 3, MAGIC4 = 4, BASIC = 5 }

	public IntVariable currentBattleState;
	public UnityEvent nextPhaseEvent;

	public EnemyController enemies;
	public PlayerMagicController magicController;
	public PlayerStatsController statsController;
	public Canvas actionCanvas;
	public Canvas selectCanvas;
	public Transform playerTransform;
	public Transform damageNumber;

	public AttackType currentAttackType = AttackType.NONE;


	// Use this for initialization
	void Start () {
		statsController.currentHP.value = statsController.maxHP.value;
		statsController.currentMana.value = statsController.maxMana.value;
		currentAttackType = AttackType.NONE;
		selectCanvas.enabled = false;
	}

	public void PreTurn() {
		statsController.currentHP.value += statsController.healthReg.value;
		statsController.currentMana.value += statsController.manaReg.value;
	}

	public void StartTurn() {
		actionCanvas.enabled = true;
	}

	public void BasicAttackSelect() {
		selectCanvas.enabled = true;
		currentAttackType = AttackType.BASIC;
	}

	public void MagicAttackSelect(int magicType) {
		selectCanvas.enabled = true;
		currentAttackType = (AttackType)magicType;
		Debug.Log("Magictype: " + currentAttackType.ToString());
	}

	public void Attack(int index) {
		if (currentAttackType == AttackType.BASIC) {
			BasicAttack(index);
		}
		else if (currentAttackType > 0) {
			MagicAttack(index);
		}
		else {
			Debug.Log("FAIL!!!");
		}
	}

	void BasicAttack(int index) {
		selectCanvas.enabled = false;
		Debug.Log("Dealt damage to the enemy!");
		int damage = statsController.damage.value;
		if (PlayerStatsController.PercentChanceTrigger(statsController.crit.value)) {
			Debug.Log("CRITICAL HIT");
			damage *= 3;
		}

		enemies.TakeDamage(index, damage);
		currentBattleState.value++;
		nextPhaseEvent.Invoke();
		currentAttackType = AttackType.NONE;
	}

	void MagicAttack(int index) {
		selectCanvas.enabled = false;
		magicController.UseSpell(index, ((int)currentAttackType)-1);
		nextPhaseEvent.Invoke();
		currentAttackType = AttackType.NONE;
	}

	public void TakeDamage(int damage) {
		int realDamage = Mathf.Max(0, damage - statsController.armor.value);
		statsController.currentHP.value -= realDamage;
		Debug.Log("Enemy hurt player for " + realDamage + " damage.");
		Transform dmg = Instantiate(damageNumber, playerTransform.position, Quaternion.identity);
		dmg.GetComponent<DamageNumberDisplay>().damage = realDamage;
	}

	public void HealDamage(int heal) {
		heal = Mathf.Min(heal, statsController.maxHP.value - statsController.currentHP.value);
		statsController.currentHP.value += heal;
		Debug.Log("Player healed " + heal + " health.");
	}

	public void LoseMana(int amount) {
		statsController.currentMana.value -= amount;
		Debug.Log("Player used " + amount + " mana.");
	}

	public void HealMana(int heal) {
		heal = Mathf.Min(heal, statsController.maxMana.value - statsController.currentMana.value);
		statsController.currentMana.value += heal;
		Debug.Log("Player healed " + heal + " mana.");
	}
}
