using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SaveTrigger : MonoBehaviour {

	public UnityEvent saveEvent;
	public UnityEvent loadEvent;


	public void ChangeScene(string scene) {
		SceneManager.LoadScene(scene);
	}

	public void TriggerSave() {
		saveEvent.Invoke();
	}

	public void TriggerLoad() {
		loadEvent.Invoke();
	}
}
