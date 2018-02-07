using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {

#region Singleton
	private static SaveController instance = null;
	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}
#endregion

	[Header("Player Stats")]
	public CharacterReference selectedCharacter;
	public IntVariable[] intVars;
	public SpellReference[] spellRefs;

	[Header("Inventory")]
	public InvListVariable equipItems;
	public InvListVariable bagItems;

	[Header("Progress")]
	public IntVariable[] dungeonProgress;

	[Header("Battle")]
	public BattleEntryReference currentBattle;


	public void SaveGame() {
		Debug.Log("###SAVED GAME FUNCTION CALLED###");
	}
}
