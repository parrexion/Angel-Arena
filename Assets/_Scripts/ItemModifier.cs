using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ItemModifier")]
public class ItemModifier : ScriptableObject {

	public int cost;
	public int health;
	public int mana;
	public int damage;
	public int armor;
	public int healthReg;
	public int manaReg;
	public int magicRes;
	public int crit;
	public int lifesteal;
}
