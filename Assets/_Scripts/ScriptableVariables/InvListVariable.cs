using UnityEngine;

/// <summary>
/// Representative list of inventory items.
/// </summary>
[CreateAssetMenu(menuName="List ScrObj Variables/Inventory List Variable")]
public class InvListVariable : ScriptableObject {

	public ItemEntry[] values;

}
