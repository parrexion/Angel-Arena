using UnityEngine;

/// <summary>
/// Representative list of inventory items.
/// </summary>
[CreateAssetMenu(menuName="List ScrObj Variables/Random Rarity List Variable")]
public class RandomRarityListVariable : ScriptableObject {

	public RarityProbabiltyTuple[] list;

	
	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Takes the index for the position in the kanji list.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public ItemEntry.Rarity GetRandomItem() {
		int roll = Random.Range(1,101);
		roll -= list[0].probability;
		ItemEntry.Rarity selectedRarity = list[0].rarity;
		int position = 1;

		while (roll > 0) {
			roll -= list[position].probability;
			selectedRarity = list[position].rarity;
			position++;
		}
		
		return selectedRarity;
	}
}
