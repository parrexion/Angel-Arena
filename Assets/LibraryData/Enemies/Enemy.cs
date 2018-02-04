using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Characters/Enemy")]
public class Enemy : ScriptableObject {

	[Header("Visuals")]
	public string enemyName;
	public Sprite color;

	[Header("Stats")]
	public int health;
	public int mana;
	public int damage;
	public int armor;
	public int magicResist;
	public int dodge;
	public int crit;

	[Header("Spell")]
	public Spell spell;

	[Header("Loot")]
	public int exp;
	public int money;

}
