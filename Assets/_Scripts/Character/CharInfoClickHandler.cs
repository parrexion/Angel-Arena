using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharInfoClickHandler : MonoBehaviour, IPointerDownHandler {

	public SpellReference selectedSpell;
	public UnityEvent clickedEvent;


	#region IPointerDownHandler implementation

	public void OnPointerDown(PointerEventData eventData) {
		selectedSpell.reference = null;
		clickedEvent.Invoke();
	}

	#endregion
}
