using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="ItemModifier")]
[System.Serializable]
public class ItemModifier : ScriptableObject {

	public enum Type { HP, MANA, DAMAGE, ARMOR, HPREG, MANAREG, 
				MAGICRES, CRIT, LIFESTEAL, MANAKILL, DODGE}

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
	public int manaKill;
	public int dodge;
}
