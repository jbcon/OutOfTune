using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class SaveLoad : MonoBehaviour {
	public string[] fileNames = new string[]{"player1","player2"} ;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public void Load(){
		PlayerData data = new PlayerData();
		FileStream file = File.Open(Application.persistentDataPath + fileNames[0]+"info.dat", FileMode.Open); 
		BinaryFormatter bf = new BinaryFormatter();
		//bf.Binder = new VersionDeserializationBinder();
		data = (PlayerData)bf.Deserialize(file);
		file.Close();
	}
	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + fileNames[0]+"info.dat" , FileMode.Open);
		PlayerData data = new PlayerData();
		data.health = 10;
		bf.Serialize(file,data);
		file.Close();
	}
}
[Serializable]
class PlayerData{
	public float health;
	public float level;
	public float checkpoint;
}