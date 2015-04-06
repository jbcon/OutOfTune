using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class SaveLoad: MonoBehaviour{
	public float health = 10;
	public int level = 1;
	public Vector3 checkpointlocation = new Vector3(0,0,0);
	public PlayerData theplayer = new PlayerData();

	public void Load(){
		if(File.Exists("playervariables.gd")) {
			PlayerData playervalues = new PlayerData();
			Stream stream = File.Open("playervariables.gd", FileMode.Open);
			BinaryFormatter bformatter = new BinaryFormatter();          
			UnityEngine.Debug.Log("Loading variables");
			playervalues = (PlayerData)bformatter.Deserialize(stream);
			level = playervalues.level;
			health = playervalues.health;
			Debug.Log(" level " +playervalues.level);
			Debug.Log("healhe " + playervalues.health);
			stream.Close();
		}
	}
	public void Save(){
		PlayerData playervalues = new PlayerData();
		playervalues.health = health;
		playervalues.level = level;
		//playervalues.checkpointlocation = checkpointlocation;
		Stream stream = File.Open("playervariables.gd", FileMode.Create);
		BinaryFormatter bformatter = new BinaryFormatter();            
		Debug.Log("Saving variables");
		bformatter.Serialize(stream, playervalues);
		stream.Close();
	}
	public void increase(){
		health +=10;
	}
	public void printinfo(){
		Debug.Log (health);
	}
}
[System.Serializable]
public class PlayerData{
	public float health;
	public int level;
	public float checkpoint;
	//public Vector3 checkpointlocation;
	public PlayerData(){
		this.health = 10;
		this.level = 1;
		this.checkpoint = 1;
		//this.checkpointlocation = new Vector3(0,0,0);
	}
	// deserialization constructor
	public PlayerData(SerializationInfo info, StreamingContext ctxt ){
		health = (float)info.GetValue("health", typeof(float));
		level = (int)info.GetValue("level", typeof(int));
		//checkpointlocation = (Vector3)info.GetValue("checkpointlocation", typeof(Vector3));
	}
	//serialization function
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt){
		info.AddValue("health", health);
		info.AddValue("level", level);
		//info.AddValue("checkpointlocation", checkpointlocation);
	}
}