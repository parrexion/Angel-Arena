using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
	
	[Header("Familiar")]
	public Transform familiarTransform;
	public IntVariable familiarHealth;
	public IntVariable familiarMaxHealth;
	public IntVariable familiarDamage;


	// Use this for initialization
	void Start () {
		statsController.CalculateSpellPassives(true);
		statsController.currentHP.value = statsController.maxHP.value;
		statsController.currentMana.value = statsController.maxMana.value;
		currentAttackType = AttackType.NONE;
		selectCanvas.enabled = false;

		familiarHealth.value = 0;
		familiarTransform.gameObject.SetActive(false);
	}

	/// <summary>
	/// Actions done before the start of each player turn.
	/// </summary>
	public void PreTurn() {
		statsController.currentHP.value += statsController.healthReg.value;
		statsController.currentMana.value += statsController.manaReg.value;
	}

	/// <summary>
	/// Actions done whenever it's the player's action.
	/// </summary>
	public void StartTurn() {
		actionCanvas.enabled = true;
		if (enemies.CheckWin()) {
			currentBattleState.value = BattleStateMachine.VICTORY_INT;
			nextPhaseEvent.Invoke();
			return;
		}
	}

	/// <summary>
	/// Shows the attack selection for basic attacks.
	/// </summary>
	public void BasicAttackSelect() {
		selectCanvas.enabled = true;
		actionCanvas.enabled = false;
		currentAttackType = AttackType.BASIC;
	}

	/// <summary>
	/// Shows the attack selection for magic attacks if the spell needs that.
	/// </summary>
	/// <param name="magicType"></param>
	public void MagicAttackSelect(int magicType) {
		currentAttackType = (AttackType)magicType;
		Spell.SpellType type = magicController.spells[magicType-1].reference.spellType;

		Debug.Log("Magicindex: " + (magicType-1) + ", Type:  " + type.ToString());

		if (type == Spell.SpellType.SINGLE) {
			selectCanvas.enabled = true;
		}
		else {
			Attack(-1);
		}
	}

	/// <summary>
	/// Called when the attack should be made.
	/// </summary>
	/// <param name="index"></param>
	public void Attack(int index) {
		if (currentAttackType == AttackType.BASIC) {
			StartCoroutine(BasicAttack(index));
		}
		else if (currentAttackType > 0) {
			MagicAttack(index);
		}
		else {
			Debug.LogError("FAIL!!!");
		}
	}

	/// <summary>
	/// Makes a basic attack and adds attack modifiers.
	/// The familiar follows up with an attack if possible.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	IEnumerator BasicAttack(int index) {
		selectCanvas.enabled = false;
		Debug.Log("Dealt damage to the enemy!");
		int damage = statsController.damage.value;
		if (PlayerStatsController.PercentChanceTrigger(statsController.crit.value)) {
			Debug.Log("CRITICAL HIT");
			damage *= 3;
		}

		int dealtDamage = enemies.TakePhysicalDamage(index, damage);

		if (dealtDamage > 0) {
			float lifestealMult = statsController.lifesteal.value / 100.0f;
			Debug.Log("Lifesteal multi: " + lifestealMult);
			int gainedHealth = (int)(lifestealMult * dealtDamage);
			HealDamage(gainedHealth);
		}

		int famIndex = FamiliarAttack();
		if (famIndex >= 0) {
			yield return new WaitForSeconds(0.5f);
			enemies.TakePhysicalDamage(famIndex, familiarDamage.value);
		}

		currentBattleState.value++;
		nextPhaseEvent.Invoke();
		currentAttackType = AttackType.NONE;
		yield break;
	}

	/// <summary>
	/// Returns the enemy index to attack or -1 if the familiar can't attack.
	/// </summary>
	/// <returns></returns>
	int FamiliarAttack() {
		if (familiarHealth.value <= 0)
			return -1;
		return enemies.FindFirstEnemy();
	}

	/// <summary>
	/// Makes a magic attack and then returns the turn back to the player again.
	/// </summary>
	/// <param name="index"></param>
	void MagicAttack(int index) {
		selectCanvas.enabled = false;
		magicController.UseSpell(index, ((int)currentAttackType)-1);
		nextPhaseEvent.Invoke();
		currentAttackType = AttackType.NONE;
	}

	/// <summary>
	/// Deals the physical damage to the player after subtracting defenses.
	/// If there is a familiar in play then the familiar takes the damage instead.
	/// </summary>
	/// <param name="damage"></param>
	public void TakePhysicalDamage(int damage) {
		if (familiarHealth.value > 0) {
			familiarHealth.value -= damage;
			Debug.Log("Enemy hurt familiar for " + damage + " damage.");
			if (familiarHealth.value <= 0) {
				familiarTransform.gameObject.SetActive(false);
			}
			return;
		}
		int realDamage = Mathf.Max(0, damage - statsController.armor.value);
		statsController.currentHP.value -= realDamage;
		Debug.Log("Enemy hurt player for " + realDamage + " damage.");
		Transform dmg = Instantiate(damageNumber, playerTransform.position, Quaternion.identity);
		dmg.GetComponent<DamageNumberDisplay>().damage = realDamage;
	}

	/// <summary>
	/// Deals the magic damage to the player.
	/// If there is a familiar in play then the familiar takes the damage instead.
	/// </summary>
	/// <param name="damage"></param>
	public void TakeMagicDamage(int damage) {
		Transform dmg;
		if (familiarHealth.value > 0) {
			familiarHealth.value -= damage;
			Debug.Log("Enemy hurt familiar for " + damage + " magic damage.");
			dmg = Instantiate(damageNumber, familiarTransform.position, Quaternion.identity);
			dmg.GetComponent<DamageNumberDisplay>().damage = damage;
			if (familiarHealth.value <= 0) {
				familiarTransform.gameObject.SetActive(false);
			}
			return;
		}
		int magicRes = Random.Range(0, 100);
		if (magicRes < statsController.magicRes.value)
			damage = 0;

		int realDamage = Mathf.Max(0, damage - statsController.armor.value);
		if (realDamage <= 0) {
			Debug.Log("Player resisted the spell!");
			dmg = Instantiate(damageNumber, playerTransform.position, Quaternion.identity);
			dmg.GetComponent<DamageNumberDisplay>().damage = realDamage;
			return;
		}
		statsController.currentHP.value -= realDamage;
		Debug.Log("Enemy hurt player for " + realDamage + " magic damage.");
		dmg = Instantiate(damageNumber, playerTransform.position, Quaternion.identity);
		dmg.GetComponent<DamageNumberDisplay>().damage = realDamage;

		if (statsController.currentHP.value <= 0) {
			PlayerDeath();
		}
	}

	/// <summary>
	/// Heals the player the specified amount.
	/// </summary>
	/// <param name="heal"></param>
	public void HealDamage(int heal) {
		heal = Mathf.Min(heal, statsController.maxHP.value - statsController.currentHP.value);
		statsController.currentHP.value += heal;
		Debug.Log("Player healed " + heal + " health.");
	}

	/// <summary>
	/// Makes the player lose the specified amount of mana.
	/// </summary>
	/// <param name="amount"></param>
	public void LoseMana(int amount) {
		statsController.currentMana.value -= amount;
		Debug.Log("Player used " + amount + " mana.");
	}

	/// <summary>
	/// Gives the player back the specified amount of mana.
	/// </summary>
	/// <param name="heal"></param>
	public void HealMana(int heal) {
		heal = Mathf.Min(heal, statsController.maxMana.value - statsController.currentMana.value);
		statsController.currentMana.value += heal;
		Debug.Log("Player healed " + heal + " mana.");
	}

	/// <summary>
	/// Creates a familiar with the amount of health specified in the heal field of the spell.
	/// </summary>
	/// <param name="health"></param>
	public void CreateFamiliar(int health) {
		familiarHealth.value = health;
		familiarMaxHealth.value = familiarHealth.value;
		familiarTransform.gameObject.SetActive(true);
	}

	/// <summary>
	/// Called when an enemy is killed and exp are rewarded.
	/// </summary>
	/// <param name="exp"></param>
	public void EnemyKilled(int exp) {
		statsController.ManaKillGained();
		statsController.totalExp.value += exp;
	}

	/// <summary>
	/// Called when the player's health reaches 0.
	/// </summary>
	void PlayerDeath() {
		Debug.Log("### PLAYER DIED ###");
		currentBattleState.value = BattleStateMachine.DEFEAT_INT;
		nextPhaseEvent.Invoke();
	}

	
}
