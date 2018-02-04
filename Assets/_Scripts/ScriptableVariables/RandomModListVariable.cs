using UnityEngine;

/// <summary>
/// Representative list of inventory items.
/// </summary>
[CreateAssetMenu(menuName="List ScrObj Variables/Random Mod List Variable")]
public class RandomModListVariable : ScriptableObject {

	public ModProbabiltyTuple[] list;

	
	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Takes the index for the position in the kanji list.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public ItemModifier GetRandomItem() {
		int roll = Random.Range(1,101);
		roll -= list[0].probability;
		ItemModifier selectedMod = list[0].modifier;
		int position = 1;

		while (roll > 0) {
			roll -= list[position].probability;
			selectedMod = list[position].modifier;
			position++;
		}
		
		return selectedMod;
	}
}
