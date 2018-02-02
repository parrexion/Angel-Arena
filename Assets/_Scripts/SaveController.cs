using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveController : MonoBehaviour {

#region Singleton
	private static SaveController instance = null;
	void OnAwake() {
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
	public IntVariable[] intVars;
	public SpellReference[] spellRefs;



	public void SaveGame() {
		Debug.Log("###SAVED GAME FUNCTION CALLED###");
	}
}
