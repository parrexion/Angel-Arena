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
	

	
	// Update is called once per frame
	void Update () {
		healthText.text = currentHealth.value + " / " + maxHealth.value;
		fillImageHP.fillAmount = Mathf.Clamp01((float)((float)currentHealth.value / (float)maxHealth.value));
		manaText.text = currentMana.value + " / " + maxMana.value;
		fillImageMana.fillAmount = Mathf.Clamp01((float)((float)currentMana.value / (float)maxMana.value));
	}
}
