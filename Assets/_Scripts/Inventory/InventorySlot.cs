using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ID used to identify which inventory the inventory slot belongs to.
/// </summary>
[System.Serializable]
public class SlotID {
	public bool allowAnyType = true;
	public ItemEntry.ItemType type;
	public int id;
}

/// <summary>
/// Class containing the information for the inventory slots.
/// </summary>
public class InventorySlot : MonoBehaviour {

	public bool moveable = true;
	public SlotID slotID;
	public Image icon;
	public ItemEntry item;


	/// <summary>
	/// Sets the SlotID for the inventory slot.
	/// </summary>
	/// <param name="type"></param>
	/// <param name="id"></param>
	public void SetID(ItemEntry.ItemType type, int id, bool allowAnyType) {
		slotID = new SlotID();
		slotID.type = type;
		slotID.id = id;
		slotID.allowAnyType = allowAnyType;
	}

	/// <summary>
	/// Adds an item to the slot, overwriting the previous item.
	/// </summary>
	/// <param name="newItem"></param>
	public void AddItem(ItemEntry newItem) {
		item = newItem;

		icon.sprite = item.icon.value;
		icon.color = item.tintColor;
		icon.enabled = true;
	}
	
	/// <summary>
	/// Empties the inventory slot.
	/// </summary>
	public void ClearSlot() {

		item = null;
		icon.sprite = null;
		icon.enabled = false;
	}

}
