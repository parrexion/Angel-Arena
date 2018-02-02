using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellClickHandler : MonoBehaviour, IPointerDownHandler {

	public SpellReference spell;
	public SpellReference selectedItem;
	public Image icon;
	public Image outline;

	public IntVariable playerLevel;
	public BoolVariable levelupMode;
	public UnityEvent levelupModeToggleEvent;


	void Start() {
		icon.sprite = spell.reference.icon;
		icon.color = (spell.level > 0) ? Color.white : new Color(0.5f,0.5f,0.5f,0.65f);
		outline.enabled = false;
	}

	public void CalculateLevelups() {
		outline.enabled = levelupMode.value && spell.reference.CanLevelup(playerLevel.value, spell.level);
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown(PointerEventData eventData) {
		if (selectedItem.reference == spell.reference && outline.enabled) {
			Debug.Log("Leveled up the spell");
			levelupMode.value = false;
			spell.level++;
			selectedItem.level = spell.level;
			levelupModeToggleEvent.Invoke();
		}
		else {
			selectedItem.reference = spell.reference;
			selectedItem.level = spell.level;
		}
	}

	#endregion
}
