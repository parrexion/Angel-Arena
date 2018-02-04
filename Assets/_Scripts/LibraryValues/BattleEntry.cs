using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class BattleProbabiltyTuple {
	public int probability;
	public BattleEntry battle;
}

[CreateAssetMenu (menuName = "LibraryEntries/Battle")]
public class BattleEntry : ScrObjLibraryEntry {

	public enum Difficulty { EASY, MEDIUM, HARD }

	public Enemy enemy = null;
	public int noOfEnemiesMin = 1;
	public int noOfEnemiesMax = 2;
	public Difficulty difficulty = Difficulty.EASY;

}
