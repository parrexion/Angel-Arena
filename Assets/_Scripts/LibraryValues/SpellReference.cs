using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName="References/Spell")]
public class SpellReference : ScriptableObject {

	public Spell reference;
	public int level;
}
