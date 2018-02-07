using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownController : MonoBehaviour {

	public string adventureName = "Map";
	public string levelupName = "Levelup";
	public string equipmentName = "Inventory";
	public string shopName = "Shop";
	public string forgeName = "Forge";


	public void AdventureButton() {
		SceneManager.LoadScene(adventureName);
	}

	public void LevelupButton() {
		SceneManager.LoadScene(levelupName);
	}

	public void EquipmentButton() {
		SceneManager.LoadScene(equipmentName);
	}

	public void ShopButton() {
		SceneManager.LoadScene(shopName);
	}

	public void ForgeButton() {
		SceneManager.LoadScene(forgeName);
	}


}
