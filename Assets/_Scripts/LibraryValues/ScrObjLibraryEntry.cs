using UnityEngine;

/// <summary>
/// Base class for all the different types of entries which are 
/// added to libraries for later access.
/// </summary>
public class ScrObjLibraryEntry : ScriptableObject {

	public string uuid;
	public string entryName;


	/// <summary>
	/// Resets the values to default.
	/// </summary>
	public virtual void ResetValues() {
		uuid = "";
		entryName = "";
	}

	/// <summary>
	/// Copies the values from another entry.
	/// </summary>
	/// <param name="other"></param>
	public virtual void CopyValues(ScrObjLibraryEntry other) {
		uuid = other.uuid;
		entryName = other.entryName;
	}


	public virtual bool IsEqual(ScrObjLibraryEntry other) {
		return (uuid == other.uuid);
	}

	public static bool CompareEntries(ScrObjLibraryEntry obj1, ScrObjLibraryEntry obj2) {
		if (obj1 == null) {
			if (obj2 == null)
				return true;
			else
				return false;
		}
		if (obj2 == null)
			return false;

		return obj1.IsEqual(obj2);
	}
}
