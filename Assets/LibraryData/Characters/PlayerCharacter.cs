using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerCharacter : ScriptableObject {

	public string characterName;
	public Sprite icon;
	public Color color = Color.black;
	public string description;

	[Header("Spells")]
	public Spell[] spells = new Spell[4];


}
