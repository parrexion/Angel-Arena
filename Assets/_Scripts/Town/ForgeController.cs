using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgeController : MonoBehaviour {

	public string townName = "Town";
	public int recipePrice = 50;
	public ItemEntryReference selectedItem;
	public IntVariable selectedItemID;
	public ItemGenerator itemGenerator;
	public InventoryHandler invHandler;

	[Header("UI Elements")]
	public Button forgeButton;
	public Text forgePriceText;
	public Button buyButton;
	public Button sellButton;
	public Text sellPriceText;
	public Text currentMoneyText;

	[Header("Player Stats")]
	public IntVariable playerLevel;
	public IntVariable totalMoney;

	public UnityEvent inventoryChanged;


	void Start() {
		BuyAvailable();
	}

	void Update() {
		ItemSelected();
		currentMoneyText.text = totalMoney.value.ToString();
	}

	public void ItemSelected() {
		ItemEntry item = selectedItem.reference;
		if (item != null) {
			sellButton.interactable = true;
			sellPriceText.text = "SELL\n" + ((item.bought) ? 0 : selectedItem.reference.moneyValue / 2) + " gold";
			if (item.isRecipe) {
				forgeButton.interactable = (totalMoney.value >= item.moneyValue);
				forgePriceText.text = "FORGE\n" + item.moneyValue + " gold";
			}
			else {
				forgeButton.interactable = false;
				forgePriceText.text = "FORGE";
			}
		}
		else {
			sellButton.interactable = false;
			sellPriceText.text = "SELL";
			forgeButton.interactable = false;
			forgePriceText.text = "FORGE";
		}
	}

	public void ForgeButton() {
		int value = selectedItem.reference.moneyValue;
		totalMoney.value -= value;
		itemGenerator.CreateFromRecipe();
		ItemSelected();
		inventoryChanged.Invoke();
	}

	public void BuyButton() {
		totalMoney.value -= recipePrice;
		BuyAvailable();
		ItemEntry item = itemGenerator.GenerateLevel(playerLevel.value);
		invHandler.AddBag(item);
	}

	public void SellButton() {
		ItemEntry item = selectedItem.reference;
		if (item == null)
			return;

		totalMoney.value += (item.bought) ? 0 : selectedItem.reference.moneyValue / 2;
		invHandler.Remove(selectedItemID.value);
		selectedItem.reference = null;
		BuyAvailable();
	}

	/// <summary>
	/// Activates the buy button if it's possible for the player to buy recipes.
	/// </summary>
	void BuyAvailable() {
		buyButton.interactable = (totalMoney.value >= recipePrice) && invHandler.FreeBagSlot();
	}

	/// <summary>
	/// Return to the town screen.
	/// </summary>
	public void ReturnButton() {
		SceneManager.LoadScene(townName);
	}


}
