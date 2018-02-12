using UnityEngine;

/// <summary>
/// The UI component of the inventory screen containing all the images of the inventory.
/// </summary>
public class InventoryUI : MonoBehaviour {

	public Transform equipItemsParent;
	public Transform bagItemsParent;

	InventorySlot[] equipSlots;
	InventorySlot[] bagSlots;

	public InvListVariable invItemEquip;
	public InvListVariable invItemBag;

	bool useEquip;
	bool useBags;


	// Use this for initialization
	void Start () {

		useEquip = equipItemsParent != null && invItemEquip != null;
		useBags = bagItemsParent != null && invItemBag != null;

		//Slot initialization
		if (useEquip) {
			equipSlots = equipItemsParent.GetComponentsInChildren<InventorySlot>();
			equipSlots[0].SetID(ItemEntry.ItemType.WEAPON,-1,false);
			equipSlots[1].SetID(ItemEntry.ItemType.ARMOR,-2,false);
			equipSlots[2].SetID(ItemEntry.ItemType.HELM,-3,false);
			equipSlots[3].SetID(ItemEntry.ItemType.AMULET,-4,false);
		}

		if (useBags) {
			bagSlots = bagItemsParent.GetComponentsInChildren<InventorySlot>();
			for (int i = 0; i < bagSlots.Length; i++) {
				bagSlots[i].SetID(ItemEntry.ItemType.WEAPON,i,true);
			}
		}

		Debug.Log("Initiated the slot ids");
		UpdateUI();
	}

	/// <summary>
	/// Update function for the UI. Uses the inventory to update the images of all the inventory slots.
	/// </summary>
	public void UpdateUI() {

		//Update the equipment
		if (useEquip) {
			for (int i = 0; i < equipSlots.Length; i++) {
				if (invItemEquip.values[i] != null) {
					equipSlots[i].AddItem(invItemEquip.values[i]);
				}
				else {
					equipSlots[i].ClearSlot();
				}
			}
		}

		if (useBags) {
			for (int i = 0; i < bagSlots.Length; i++) {
				if (invItemBag.values[i] != null) {
					bagSlots[i].AddItem(invItemBag.values[i]);
				}
				else {
					bagSlots[i].ClearSlot();
				}
			}
		}
	}

}
