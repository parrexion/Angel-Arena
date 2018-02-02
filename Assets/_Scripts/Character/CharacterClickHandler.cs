using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharacterClickHandler : MonoBehaviour, IPointerDownHandler {

	public PlayerCharacter character;
	public CharacterReference selectedCharacter;
	public Image icon;
	public Image selected;
	public UnityEvent clickedEvent;


	void Start() {
		icon.sprite = character.icon;
		selected.enabled = false;
	}

	public void Select() {
		selected.enabled = (selectedCharacter.reference == character);
	}

	#region IPointerDownHandler implementation

	public void OnPointerDown(PointerEventData eventData) {
		selectedCharacter.reference = character;
		clickedEvent.Invoke();
	}

	#endregion
}
