using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
public class UIManager : MonoBehaviour {
	public Animator settings;
	public Animator Save;
	public bool clicked = false;
	public GameObject player;
	private List<string> buttonnames = new List<string>();
	private List<Image> healthlist;
	private GameObject health;
	private PlayerController playerobj;
	public void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	public void Start(){
		health = GameObject.Find("storysetting");
		//Debug.Log(health.GetComponentsInChildren<Text>());
		healthlist = new List<Image>(health.GetComponentsInChildren<Image>());
		//Debug.Log(healthlist.Count());
		for (int i = 0; i < healthlist.Count(); i++){
			healthlist[i].enabled = false;
		}

	}
	public void StartGame(){
		Application.LoadLevel("Level 1");
		for (int i = 0; i < healthlist.Count(); i++){
			healthlist[i].enabled = true;
		}
		player = GameObject.FindGameObjectWithTag("Player");
		//Debug.Log(player.health);
	}
	public void LoadLevel2(){
		Application.LoadLevel("Level 2");
	}
	public void LoadLevel3(){
		Application.LoadLevel("Level 3");
	}
	void checkinghealth(float playerhealth){
		float temphealth = playerhealth;
		//Debug.Log (temphealth);
		//Debug.Log(healthlist.Count());
		for (int k = 2 ; k < healthlist.Count(); k ++){
			if( temphealth != 0){
				healthlist[k].enabled = true;
				temphealth--;
			}else{
				healthlist[k].enabled = false;
			}
		}
	}
	public void Update(){
		if (Input.GetKeyDown((KeyCode.Escape)))
		{
			clicked = !clicked;
			settings.enabled = true;
			settings.SetBool("escPressed",clicked);
			Debug.Log("display settings");
			Save.enabled = true;
			Save.SetBool("escPressed",clicked);
		}
		player = GameObject.FindGameObjectWithTag("Player");
		if (player != null){
			playerobj = player.GetComponent<PlayerController>();
			checkinghealth(playerobj.health);
		}
	}

}
