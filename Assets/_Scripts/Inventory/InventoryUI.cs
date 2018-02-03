﻿using UnityEngine;

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


	// Use this for initialization
	void Start () {

		//Slot initialization
		equipSlots = equipItemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < equipSlots.Length; i++) {
			equipSlots[i].SetID(ItemEntry.ItemType.ARMOR,-(i+1));
		}
		bagSlots = bagItemsParent.GetComponentsInChildren<InventorySlot>();
		for (int i = 0; i < bagSlots.Length; i++) {
			bagSlots[i].SetID(ItemEntry.ItemType.WEAPON,i);
		}

		Debug.Log("Initiated the slot ids");
		UpdateUI();
	}

	/// <summary>
	/// Update function for the UI. Uses the inventory to update the images of all the inventory slots.
	/// </summary>
	public void UpdateUI() {

		//Update the equipment
		for (int i = 0; i < equipSlots.Length; i++) {
			if (invItemEquip.values[i] != null) {
				equipSlots[i].AddItem(invItemEquip.values[i]);
			}
			else {
				equipSlots[i].ClearSlot();
			}
		}

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
