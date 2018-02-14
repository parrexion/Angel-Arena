using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelupController : MonoBehaviour {

	public string townName = "Town";

	[Header("Player Stats")]
	public IntVariable playerLevel;
	public IntVariable playerExp;
	public SpellReference spell1;
	public SpellReference spell2;
	public SpellReference spell3;
	public SpellReference spell4;

	[Header("Levelup Mode")]
	public BoolVariable levelupMode;
	public int levelupCostPerLevel = 10;
	public UnityEvent levelupClickedEvent;

	[Header("UI")]
	public Text playerLevelText;
	public Text totalExpText;
	public Text levelupCostText;
	public Button levelupButton;



	void Start() {
		levelupMode.value = false;
		UpdateUI();
	}

	void UpdateUI() {
		playerLevelText.text = "Level: " + playerLevel.value;
		totalExpText.text = "EXP: " + playerExp.value;
		int levelupCost = (levelupCostPerLevel * playerLevel.value);
		levelupCostText.text = "LEVELUP\n" + levelupCost + " EXP";
		levelupButton.interactable = (playerExp.value >= levelupCost);
	}

	/// <summary>
	/// Toggles the levelup mode.
	/// </summary>
	public void ToggleLeveupMode() {
		if (levelupMode.value)
			return;
		levelupMode.value = true;
		levelupClickedEvent.Invoke();
	}

	/// <summary>
	/// Levels up the player.
	/// </summary>
	public void LevelupPlayer() {
		playerExp.value -= (levelupCostPerLevel * playerLevel.value);
		playerLevel.value++;
		levelupMode.value = false;
		levelupClickedEvent.Invoke();

		UpdateUI();
	}

	/// <summary>
	/// Return to the town screen.
	/// </summary>
	public void ReturnToTown() {
		SceneManager.LoadScene(townName);
	}

	/// <summary>
	/// Resets the level and spells and awards some exp for debugging purpose.
	/// </summary>
	public void ResetPlayer() {
		playerLevel.value = 0;
		playerExp.value = 250;
		spell1.level = 0;
		spell2.level = 0;
		spell3.level = 0;
		spell4.level = 0;
		UpdateUI();
		levelupClickedEvent.Invoke();
	}
}
