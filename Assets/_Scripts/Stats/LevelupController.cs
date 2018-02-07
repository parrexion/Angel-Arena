using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class LevelupController : MonoBehaviour {

	public string townName = "Town";
	public BoolVariable levelupMode;
	public UnityEvent levelupClickedEvent;
	

	/// <summary>
	/// Toggles the levelup mode.
	/// </summary>
	public void ToggleLeveupMode() {
		if (levelupMode.value)
			return;
		levelupMode.value = true;
		levelupClickedEvent.Invoke();
	}

	/// <summary>
	/// Return to the town screen.
	/// </summary>
	public void ReturnToTown() {
		SceneManager.LoadScene(townName);
	}
}
