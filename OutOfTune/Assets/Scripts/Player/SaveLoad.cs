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
	public float checkpointlocationx = 0;
	public float checkpointlocationy =0;
	public float checkpointlocationz = 0;
	public PlayerData theplayer = new PlayerData();
	private GameObject player;
	private bool inposition = false;
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
		Debug.Log("loading");
		if(player != null && theplayer.saved == true){
			Vector3 temp = new Vector3(theplayer.checkpointx, theplayer.checkpointy,theplayer.checkpointz);
			player.transform.position = temp;
		}
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
	public void setCheckpoint(){
		//upon player death or checkkpoint reach set it as new checkpoint location
		player.GetComponent<PlayerController>().newcheckpoint();
		theplayer.checkpointx = player.transform.position.x;
		theplayer.checkpointy = player.transform.position.y;
		theplayer.checkpointz = player.transform.position.z;
		theplayer.saved = true;
		Debug.Log("setting checkpoint");
	}
	void Update(){
		player = GameObject.FindGameObjectWithTag("Player");
		if(player != null && inposition == false && theplayer.saved == true){
			inposition = true;
			Vector3 temp = new Vector3(theplayer.checkpointx, theplayer.checkpointy,theplayer.checkpointz);
			player.transform.position = temp;
		}

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
			checkpointlocationx = playervalues.checkpointx;
			theplayer.checkpointx = playervalues.checkpointx;
			theplayer.checkpointy = playervalues.checkpointy;
			theplayer.checkpointz = playervalues.checkpointz;
			theplayer.saved = playervalues.saved;
			Debug.Log("level " +playervalues.level);
			Debug.Log("healh " + playervalues.health);
			stream.Close();
			//loading the necessary files
			switch(level){
			case 1:
				Application.LoadLevel("Level 1");
				break;
			case 2: 
				Application.LoadLevel("Level 2");
				break;
			case 3:
				Application.LoadLevel("Level 3");
				break;
			default:
				break;
			}

		}
	}
	public void Save(){
		PlayerData playervalues = new PlayerData();
		playervalues.health = theplayer.health;
		playervalues.level = theplayer.level;
		setCheckpoint();
		playervalues.checkpointx = theplayer.checkpointx;
		playervalues.checkpointy = theplayer.checkpointy;
		playervalues.checkpointz = theplayer.checkpointz;
		playervalues.saved = theplayer.saved;
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
	public float checkpointx;	//stores the number checkpoint
	public float checkpointy;
	public float checkpointz;
	public bool saved;
	public PlayerData(){
		//seting default values
		this.health = 10;		
		this.level = 1;
		this.checkpointx = 1;
		this.checkpointy = 1;
		this.checkpointz = 1;
		this.saved = false;
		//this.checkpointlocation = new Vector3(0,0,0);
	}
	// deserialization constructor
	public PlayerData(SerializationInfo info, StreamingContext ctxt ){
		health = (float)info.GetValue("health", typeof(float));
		level = (int)info.GetValue("level", typeof(int));
		checkpointx = (float)info.GetValue("checkpointx" , typeof(float));
		checkpointy = (float)info.GetValue("checkpointy" , typeof(float));
		checkpointz = (float)info.GetValue("checkpointz" , typeof(float));
		saved = (bool)info.GetValue("saved",typeof(bool));
		//checkpointlocation = (Vector3)info.GetValue("checkpointlocation", typeof(Vector3));
	}
	//serialization function
	public void GetObjectData(SerializationInfo info, StreamingContext ctxt){
		info.AddValue("health", health);
		info.AddValue("level", level);
		info.AddValue("checkpointx", checkpointx);
		info.AddValue("checkpointy", checkpointy);
		info.AddValue("checkpointz", checkpointz);
		info.AddValue("saved", saved);
		//info.AddValue("checkpointlocation", checkpointlocation);
	}
}