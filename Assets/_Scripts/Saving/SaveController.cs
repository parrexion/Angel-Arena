using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class SaveData {
	public string characterID;
	public int[] playerGains;
	public int[] playerStats;
	public int[] spellLevels;

	public SavedItem[] equippedItems;
	public SavedItem[] bagItems;

	public int playTime;
}

public class SaveController : MonoBehaviour {

#region Singleton
	private static SaveController instance = null;
	void Awake() {
		if (instance != null) {
			Destroy(gameObject);
		}
		else {
			DontDestroyOnLoad(gameObject);
			instance = this;
		}
	}
#endregion

	public string townStr = "Town";

	[Header("Libraries")]
	public ScrObjLibraryVariable characterLibrary;
	public ScrObjLibraryVariable spriteLibrary;

	[Header("Player Stats")]
	public CharacterReference selectedCharacter;
	public IntVariable[] playerGains;
	public IntVariable[] playerStats;
	public SpellReference[] spellRefs;

	[Header("Inventory")]
	public InvListVariable equipItems;
	public InvListVariable bagItems;

	[Header("Progress")]
	public IntVariable playtime;
	public IntVariable[] dungeonProgress;

	[Header("Battle")]
	public BattleEntryReference currentBattle;

	private string filePath = "";


	void SetPath() {
		filePath = Application.persistentDataPath+"/settingsData.xml";
	}

	public void SaveGame() {
		Debug.Log("###SAVED GAME FUNCTION CALLED###");
		SaveData data = new SaveData();
		data.characterID = selectedCharacter.reference.uuid;
		data.playerGains = new int[playerGains.Length];
		for (int i = 0; i < playerGains.Length; i++) {
			data.playerGains[i] = playerGains[i].value;
		}
		data.playerStats = new int[playerStats.Length];
		for (int i = 0; i < playerStats.Length; i++) {
			data.playerStats[i] = playerStats[i].value;
		}
		data.spellLevels = new int[spellRefs.Length];
		for (int i = 0; i < spellRefs.Length; i++) {
			data.spellLevels[i] = spellRefs[i].level;
		}

		data.equippedItems = new SavedItem[equipItems.values.Length];
		for (int i = 0; i < equipItems.values.Length; i++) {
			if (equipItems.values[i] != null) {
				data.equippedItems[i] = equipItems.values[i].Save();
				data.equippedItems[i].isItem = true;
			}
		}
		data.bagItems = new SavedItem[bagItems.values.Length];
		for (int i = 0; i < bagItems.values.Length; i++) {
			if (bagItems.values[i] != null) {
				data.bagItems[i] = bagItems.values[i].Save();
				data.bagItems[i].isItem = true;
			}
		}

		data.playTime = playtime.value;

		WriteFile(data);
	}

	public void LoadGame() {
		Debug.Log("###LOAD GAME FUNCTION CALLED###");
		characterLibrary.GenerateDictionary();
		spriteLibrary.GenerateDictionary();

		SetPath();
		SaveData data = ReadFile();
		Debug.Log(JsonUtility.ToJson(data));

		//Character
		PlayerCharacter character = (PlayerCharacter)characterLibrary.GetEntry(data.characterID);
		selectedCharacter.reference = character;
		for (int i = 0; i < character.spells.Length; i++) {
			spellRefs[i].reference = character.spells[i];
		}

		// Player stats
		for (int i = 0; i < playerGains.Length; i++) {
			playerGains[i].value = data.playerGains[i];
		}
		for (int i = 0; i < playerStats.Length; i++) {
			playerStats[i].value = data.playerStats[i];
		}
		//Spells
		for (int i = 0; i < spellRefs.Length; i++) {
			spellRefs[i].level = data.spellLevels[i];
		}
		Debug.Log("Finished loading stats");
		//Inventory
		ItemEntry item;
		equipItems.values = new ItemEntry[data.equippedItems.Length];
		for (int i = 0; i < data.equippedItems.Length; i++) {
			if (data.equippedItems[i] == null || !data.equippedItems[i].isItem)
				continue;

			item = (ItemEntry)ScriptableObject.CreateInstance("ItemEntry");
			item.icon = (SpriteEntry)spriteLibrary.GetEntry(data.equippedItems[i].iconID);
			item.Load(data.equippedItems[i]);
			equipItems.values[i] = item;
		}
		bagItems.values = new ItemEntry[data.bagItems.Length];
		for (int i = 0; i < data.bagItems.Length; i++) {
			if (data.bagItems[i] == null || !data.bagItems[i].isItem)
				continue;

			item = (ItemEntry)ScriptableObject.CreateInstance("ItemEntry");
			item.icon = (SpriteEntry)spriteLibrary.GetEntry(data.bagItems[i].iconID);
			item.Load(data.bagItems[i]);
			bagItems.values[i] = item;
		}
		//Other
		playtime.value = data.playTime;
		Debug.Log("Finished loading");
		SceneManager.LoadScene(townStr);
	}

	public void WriteFile(SaveData data) {
		XmlWriterSettings xmlWriterSettings = new XmlWriterSettings() { Indent = true };
		XmlSerializer serializer;

		serializer = new XmlSerializer(typeof(SaveData));
		SetPath();

		using (XmlWriter xmlWriter = XmlWriter.Create(filePath, xmlWriterSettings)) {
			serializer.Serialize(xmlWriter, data);
		}
		Debug.Log("Successfully saved the settings!");
	}

	SaveData ReadFile() {
		if (File.Exists(filePath)){
			XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
			FileStream file = File.Open(filePath,FileMode.Open);
			SaveData data = serializer.Deserialize(file) as SaveData;
			file.Close();
			Debug.Log("Successfully loaded the settings!");
			return data;
		}
		else {
			Debug.LogWarning("Could not open the file: " + filePath);
			return null;
		}
	}
}
