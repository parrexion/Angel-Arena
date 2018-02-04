using UnityEngine;

/// <summary>
/// Representative list of inventory items.
/// </summary>
[CreateAssetMenu(menuName="List ScrObj Variables/Random Type List Variable")]
public class RandomTypeListVariable : ScriptableObject {

	public BaseProbabiltyTuple[] list;

	
	/// <summary>
	/// Returns the kanji representation with activations, projectiles and values etc...
	/// Takes the index for the position in the kanji list.
	/// </summary>
	/// <param name="index"></param>
	/// <returns></returns>
	public ItemEntry.ItemType GetRandomItem() {
		int roll = Random.Range(1,101);
		roll -= list[0].probability;
		ItemEntry.ItemType selectedType = list[0].baseType;
		int position = 1;

		while (roll > 0) {
			roll -= list[position].probability;
			selectedType = list[position].baseType;
			position++;
		}
		
		return selectedType;
	}
}
