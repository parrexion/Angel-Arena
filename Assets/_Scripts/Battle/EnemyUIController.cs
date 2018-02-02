using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUIController : MonoBehaviour {

	public Image[] healthBarBkgs;
	public Image[] healthBars;

	// Use this for initialization
	void Start() {
		for (int i = 0; i < healthBarBkgs.Length; i++) {
			healthBarBkgs[i].enabled = false;
			healthBars[i].enabled = false;
		}
	}


	public void UpdateValue(int index, float percent) {
		healthBarBkgs[index].enabled = true;
		healthBars[index].enabled = true;
		healthBars[index].fillAmount = Mathf.Clamp01(percent);
		if (healthBars[index].fillAmount == 0) {
			healthBarBkgs[index].enabled = false;
			healthBars[index].enabled = false;
		}
	}
}
