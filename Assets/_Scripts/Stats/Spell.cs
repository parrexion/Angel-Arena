using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="Spell")]
public class Spell : ScriptableObject {

	public bool useable;
	public Sprite icon;

	public bool ultimate;
	public int damage;
	public int cooldown;
	public int manaCost;
}
