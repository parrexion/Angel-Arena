using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForgeController : MonoBehaviour {

	public string townName = "Town";

	/// <summary>
	/// Return to the town screen.
	/// </summary>
	public void ReturnButton() {
		SceneManager.LoadScene(townName);
	}


}
