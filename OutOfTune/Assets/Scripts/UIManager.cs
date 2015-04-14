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
	private List<Image> weapons = new List<Image>();
	private GameObject health;
	private PlayerController playerobj;
	private Image weaponimg;
	public void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	public void Start(){
		health = GameObject.Find("storysetting");
		//Debug.Log(health.GetComponentsInChildren<Text>());
		healthlist = new List<Image>(health.GetComponentsInChildren<Image>());
		List<int> temp = new List<int>();
		for (int i = 0; i < healthlist.Count(); i++){
			//Debug.Log(healthlist[i].sprite.name+ "asdfdasf"+ healthlist[i] + i);
			if (healthlist[i].sprite.name != "Health1"){
				temp.Add(i);
				if (healthlist[i].sprite.name == "tuba" || healthlist[i].sprite.name == "bigtrumpet" ){
					weaponimg = healthlist[i];
					weaponimg.GetComponent<Outline>().enabled = false;
					weapons.Add(weaponimg);
				}
			}
			healthlist[i].enabled = false;
		}
		int tempsub = 1;
		for (int t = 0; t < temp.Count(); t ++){
			//Debug.Log(temp[t] + healthlist[t].sprite.name + t);
			healthlist.RemoveAt(temp[t]);
			//have to minus 1 more than usual becuase positions have shifted
			//Debug.Log(temp[t+1]);
			if (t+1 < temp.Count()){
				temp[t+1] -=tempsub;
				tempsub +=1;
			}

		}


	}
	public void StartGame(){
		Application.LoadLevel("Level 1");
		for (int i = 0; i < healthlist.Count(); i++){
			healthlist[i].enabled = true;
		}
		for (int k = 0; k < weapons.Count(); k++){
			weapons[k].enabled = true;
		}
		player = GameObject.FindGameObjectWithTag("Player");
	}
	public void LoadLevel2(){
		Application.LoadLevel("Level 2");
	}
	public void LoadLevel3(){
		Application.LoadLevel("Level 3");
	}
	void checkinghealth(float playerhealth){
		float temphealth = playerhealth;
		//as loop forward disable the last health 
		//image then the temp health = in casea there is more than one
		//loop and subtract the temp health reaches 0 that means disable the heart after that
		for (int k = 0 ; k < healthlist.Count(); k ++){
			if( temphealth != 0){
				healthlist[k].enabled = true;
				temphealth--;
			}else{
				healthlist[k].enabled = false;
			}
		}
	}
	void highlightweapon(string weaponname){
		for (int counter = 0; counter < weapons.Count(); counter ++){
			if (weaponname == weapons[counter].sprite.name){
				weapons[counter].GetComponent<Outline>().enabled = true;
				continue;
			}else{
				weapons[counter].GetComponent<Outline>().enabled = false;
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
			highlightweapon(playerobj.weaponManager.weaponname);
			Debug.Log(playerobj.weaponManager.weaponname);
		}
	}

}
