using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour {

	public BattleEntryReference selectedBattle;
	public RandomFightListVariable[] fightLists;

	[Header("Dungeon stats")]
	public IntVariable currentPosition;
	public IntVariable currentDifficulty;

	[Header("Dungeon canvases")]
	public Canvas outsideCanvas;
	public Canvas insideCanvas;
	public Canvas clearedCanvas;
	public Text dungeonName;
	public Text fightNumber;


	void Start() {
		if (currentPosition.value == 0) {
			SetupOutsideDungeon();
		}
		else if (currentPosition.value >= 3) {
			SetupClearedDungeon();
		}
		else {
			SetupInsideDungeon();
		}
	}

	void SetupOutsideDungeon() {
		outsideCanvas.enabled = true;
		insideCanvas.enabled = false;
		clearedCanvas.enabled = false;
	}

	void SetupInsideDungeon() {
		outsideCanvas.enabled = false;
		insideCanvas.enabled = true;
		clearedCanvas.enabled = false;

		BattleEntry.Difficulty diff = (BattleEntry.Difficulty)currentDifficulty.value;
		dungeonName.text = "D: " + diff;
		fightNumber.text = "Fight: " + (currentPosition.value+1);
	}

	void SetupClearedDungeon() {
		outsideCanvas.enabled = false;
		insideCanvas.enabled = false;
		clearedCanvas.enabled = true;
	}

	public void StartFight(int type) {
		selectedBattle.reference = fightLists[type].GetRandomItem();
		currentDifficulty.value = type;
		currentPosition.value = 1;
		SceneManager.LoadScene("Battle");
	}

	public void ContinueFight() {
		selectedBattle.reference = fightLists[currentDifficulty.value].GetRandomItem();
		currentPosition.value++;
		SceneManager.LoadScene("Battle");
	}

	public void ReturnToMap() {
		currentDifficulty.value = 0;
		currentPosition.value = 0;
		SceneManager.LoadScene("Map");
	}

	public void ReturnToTown() {
		currentDifficulty.value = 0;
		currentPosition.value = 0;
		SceneManager.LoadScene("Town");
	}
}
