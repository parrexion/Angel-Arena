using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Characters/Enemy")]
public class Enemy : ScriptableObject {

	[Header("Visuals")]
	public Sprite color;

	[Header("Stats")]
	public int health;
	public int mana;
	public int attack;
	public int defense;

	[Header("Loot")]
	public int exp;
	public int money;
}
