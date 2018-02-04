using UnityEngine;

/// <summary>
/// Representative list of inventory items.
/// </summary>
[CreateAssetMenu(menuName="List ScrObj Variables/Random Fight List Variable")]
public class RandomFightListVariable : ScriptableObject {

	public BattleProbabiltyTuple[] list;

	
	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Takes the index for the position in the kanji list.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public BattleEntry GetRandomItem() {
		int roll = Random.Range(1,101);
		roll -= list[0].probability;
		BattleEntry selectedBattle = list[0].battle;
		int position = 1;

		while (roll > 0) {
			roll -= list[position].probability;
			selectedBattle = list[position].battle;
			position++;
		}
		
		return selectedBattle;
	}
}
