using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Handler for the inventory slots which handles when items are placed into the inventory slot.
/// </summary>
public class SlotHandler : MonoBehaviour, IDropHandler, IPointerDownHandler {

	public ItemEntryReference selectedItem;
	public IntVariable selectedItemID;
	public InventoryHandler invContainer;

	private DragHandler dragHandler;
	private InventorySlot slot;


	void Start() {
		slot = GetComponent<InventorySlot>();
	}

	#region IDropHandler implementation

	/// <summary>
	/// Updates the position of the item being dropped into this inventory slot.
	/// </summary>
	/// <param name="eventData"></param>
	public void OnDrop(PointerEventData eventData) {
		if (DragHandler.itemBeingDragged == null)
			return;

		SlotID start_id = DragHandler.itemBeingDragged.GetComponent<DragHandler>().start_id;
		if (slot.slotID.type == ItemEntry.ItemType.DESTROY)
			selectedItem.reference = null;

		invContainer.Swap(start_id,slot.slotID);
	}

    #endregion

	#region IPointerDownHandler implementation

    public void OnPointerDown(PointerEventData eventData) {
		selectedItem.reference = slot.item;
		if (selectedItem.reference != null){
			Debug.Log("Select " + selectedItemID.value);
			Debug.Log("Slot" + slot.slotID.id);
			selectedItemID.value = slot.slotID.id;
		}
	}

    #endregion
}
