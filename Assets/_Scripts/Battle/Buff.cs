using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff : MonoBehaviour {

	public int durationLeft;
	public ItemModifier modifier;

	[Header("Player Stats")]
	public IntVariable currentHP;
	public IntVariable currentMana;
	public IntVariable maxHP;
	public IntVariable maxMana;

	public IntVariable damage;
	public IntVariable armor;

	public IntVariable healthReg;
	public IntVariable manaReg;

	public IntVariable magicRes;
	public IntVariable crit;
	public IntVariable lifesteal;
	public IntVariable manaKill;


	// Use this for initialization
	void Start () {
		
	}
	
	public void UpdateCooldown() {
		durationLeft--;
		if (durationLeft == 0) {
			
		}
	}

	void AddAttributes() {

	}
}
