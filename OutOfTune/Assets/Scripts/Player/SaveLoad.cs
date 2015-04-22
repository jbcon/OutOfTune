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

	public void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	public void OnLevelWasLoaded(int level_loaded){
		//loading the checkpoint
		/*the way this would work is to previously store  locations of eveyr check point 
			 * or get the locations of it and store it preivously like getting component
			 * reposition the player according to the player
			 * 
			 */
		switch(level_loaded){
		case 1:
			theplayer.level = 1;
			break;
		case 2:
			theplayer.level = 2;
			break;
		case 3:
			theplayer.level = 3;
			break;
		}
	}
	public void setCheckpoint(Vector3 newlocation){
		//upon player death or checkkpoint reach set it as new checkpoint location
		checkpointlocation = newlocation;
	}
	public void Load(){
		if(File.Exists("playervariables.gd")) {
			PlayerData playervalues = new PlayerData();
			Stream stream = File.Open("playervariables.gd", FileMode.Open);
			BinaryFormatter bformatter = new BinaryFormatter();          
			UnityEngine.Debug.Log("Loading variables");
			playervalues = (PlayerData)bformatter.Deserialize(stream);
			level = playervalues.level;
			health = playervalues.health;
			theplayer.health = playervalues.health;
			theplayer.level = playervalues.level;
			Debug.Log("level " +playervalues.level);
			Debug.Log("healh " + playervalues.health);
			stream.Close();
			//loading the necessary files
			switch(level){
			case 1:
				Application.LoadLevel("Level 1_alpha");
				break;
			case 2: 
				Application.LoadLevel("Level 2_alpha");
				break;
			case 3:
				Application.LoadLevel("Level 3_alpha");
				break;
			default:
				break;
			}

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
	//debuggin / testing functions
	public void increase(){
		level +=1;
	}
	public void printinfo(){
		Debug.Log (level);
	}
}
[System.Serializable]
public class PlayerData{
	public float health;		//stores the health of the object
	public int level;			//Stores the level the player was on
	public float checkpoint;	//stores the number checkpoint
	//public Vector3 checkpointlocation;
	public PlayerData(){
		//seting default values
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