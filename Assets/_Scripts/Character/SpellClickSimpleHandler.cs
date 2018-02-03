using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SpellClickSimpleHandler : MonoBehaviour, IPointerDownHandler {

	public SpellReference selectedSpell;
	public CharacterReference selectedCharacter;
	public int spellIndex;
	public UnityEvent clickedEvent;


	#region IPointerDownHandler implementation

	public void OnPointerDown(PointerEventData eventData) {
		selectedSpell.reference = selectedCharacter.reference.spells[spellIndex];
		clickedEvent.Invoke();
	}

	#endregion
}
