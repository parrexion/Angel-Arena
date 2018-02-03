using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsBars : MonoBehaviour {

	[Header("Health")]
	public Text healthText;
	public Image fillImageHP;
	public IntVariable currentHealth;
	public IntVariable maxHealth;

	[Header("Mana")]
	public Text manaText;
	public Image fillImageMana;
	public IntVariable currentMana;
	public IntVariable maxMana;
	
	[Header("Familiar")]
	public Text f_healthText;
	public Image f_bkgImageHP;
	public Image f_fillImageHP;
	public IntVariable f_currentHealth;
	public IntVariable f_maxHealth;

	
	// Update is called once per frame
	void Update () {
		healthText.text = currentHealth.value + " / " + maxHealth.value;
		fillImageHP.fillAmount = Mathf.Clamp01((float)((float)currentHealth.value / (float)maxHealth.value));

		manaText.text = currentMana.value + " / " + maxMana.value;
		fillImageMana.fillAmount = Mathf.Clamp01((float)((float)currentMana.value / (float)maxMana.value));
		
		if (f_currentHealth.value > 0) {
			f_bkgImageHP.enabled = true;
			f_fillImageHP.enabled = true;
			// f_healthText.text = f_currentHealth.value + " / " + f_maxHealth.value;
			f_fillImageHP.fillAmount = Mathf.Clamp01((float)((float)f_currentHealth.value / (float)f_maxHealth.value));
		}
		else {
			f_bkgImageHP.enabled = false;
			f_fillImageHP.enabled = false;
		}

	}
}
