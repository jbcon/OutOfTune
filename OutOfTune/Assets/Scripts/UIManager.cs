using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;
public class UIManager : MonoBehaviour {
	//the animations for each button are set
	public Animator settings;
	public Animator Save;
	public Animator newgamebutton;
	public Animator loadbutton;
	public Animator level2button;
	public Animator level3button;
	public bool clicked = false;
	public GameObject player;
	private List<string> buttonnames = new List<string>(); //possible use using keyboard input to select htings
	private List<Image> healthlist;						//list of the health images- can be changed if creating them would be more cost efficent
	private List<Image> weapons = new List<Image>();	//list of weapons
	private GameObject health;							//Going to be the canvas GUI object that will stay throughout the levels
	private PlayerController playerobj;					//Grabing the player object to access variables off of it
	private Image weaponimg;							//temp variable for the weapon image
	public void Awake(){
		DontDestroyOnLoad(gameObject);					//making sure this gameobject doesn't get destroyed for each new level
	}
	public void Start(){
		settings.enabled = true;
		Save.enabled = true;
		buttonnames.Add("newgame");
		buttonnames.Add("LoadButton");
		buttonnames.Add("Level2");
		buttonnames.Add("Level 3");
		health = GameObject.Find("storysetting");
		//Debug.Log(health.GetComponentsInChildren<Text>());
		healthlist = new List<Image>(health.GetComponentsInChildren<Image>());	//grabs all the images on the canvas for the GUI
		List<int> temp = new List<int>();										//stores the indexes of the images that aren;t part of health
		for (int i = 0; i < healthlist.Count(); i++){
			//Debug.Log(healthlist[i].sprite.name+ "asdfdasf"+ healthlist[i] + i);
			if (healthlist[i].sprite.name != "Health1"){						//if the image isn't health store index
				temp.Add(i);
				if (healthlist[i].sprite.name == "tuba" || healthlist[i].sprite.name == "bigtrumpet" ||healthlist[i].sprite.name == "flute"
				    || healthlist[i].sprite.name == "trombone" || healthlist[i].sprite.name == "violin"

				    ){// if it happens to be a weapon
					weaponimg = healthlist[i];															// store inside weapon list 
					weaponimg.GetComponent<Outline>().enabled = false;									//disable outline
					weapons.Add(weaponimg);																//add it to weapon list
				}else if (healthlist[i].sprite.name == "button_level2" ||healthlist[i].sprite.name =="button_newGame" ||
				          healthlist[i].sprite.name == "button_settings" ||healthlist[i].sprite.name =="button_level3" ||
				          healthlist[i].sprite.name =="button_load" || healthlist[i].sprite.name =="button_save" 
				          ){										//ignore the setting buttons
					//Debug.Log ("here");
					continue;
				}

			}
			healthlist[i].enabled = false;																//disable all the images for the main menu
		}
		int tempsub = 1; 			// variable to store the new modified list after changing it
		for (int t = 0; t < temp.Count(); t ++){
			//Debug.Log(temp[t] + healthlist[t].sprite.name + t);
			//remove from the health list all unnecessary images that aren't health through the indexes
			healthlist.RemoveAt(temp[t]);
			//have to minus 1 more than usual becuase positions have shifted
			//Debug.Log(temp[t+1]);
			//make sure the tempsub is updated after each modifed list changes position of the images in the list
			if (t+1 < temp.Count()){
				temp[t+1] -=tempsub;
				tempsub +=1;
			}

		}
	}
	void loadUI(){
		//Enable the hearts to be seen
		for (int i = 0; i < healthlist.Count(); i++){
			healthlist[i].enabled = true;
		}
		//enable the weapon selcetor object to be seen
		for (int k = 0; k < weapons.Count(); k++){
			weapons[k].enabled = true;
		}
	}
	public void StartGame(){
		Application.LoadLevel("Level 1_alpha");
		loadUI ();
	}
	public void LoadLevel2(){
		Application.LoadLevel("Level 2_alpha");
		loadUI ();
	}
	public void LoadLevel3(){
		Application.LoadLevel("Level 3_alpha");
		loadUI ();
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
		//go through all the weapons and if the weapon isn;t currently selected
		//turn off the outline
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
		GameObject testbutton = GameObject.Find("Save");
		if (Input.GetKeyDown((KeyCode.Escape)))
		{
			//bringing down the setting menu
			clicked = !clicked;
			settings.SetBool("escPressed",clicked);
			//Debug.Log("display settings" + clicked);

			//when opening settings run the animations for setting
			//and move the menu buttons out of the ways
			Save.SetBool("escPressed",clicked);
			newgamebutton.SetBool("escPressed",clicked);
			loadbutton.SetBool("escPressed",clicked);
			level2button.SetBool("escPressed",clicked);
			level3button.SetBool("escPressed",clicked);

			//list all the enemies
			GameObject[] enemies;
			enemies = GameObject.FindGameObjectsWithTag("enemy");

			if (clicked){
				//settings menu set selected menu to the the save button
				EventSystem tempevent = EventSystem.current;
				EventSystem.current.SetSelectedGameObject(testbutton, new BaseEventData(tempevent));

				//disabling all enemies
				for (int disable = 0; disable < enemies.Count(); disable ++){
					enemies[disable].GetComponent<GeneralAI>().pausegame();
				}


			}else{
				//when go back to main menu set the focus back onto the newgame
				GameObject testbutton2 = GameObject.Find("newgame");
				EventSystem tempevent = EventSystem.current;
				EventSystem.current.SetSelectedGameObject(testbutton2, new BaseEventData(tempevent));

				//reenabling all the enemies
				for (int disable2 = 0; disable2 < enemies.Count(); disable2 ++){
					enemies[disable2].GetComponent<GeneralAI>().unpause();
				}
			}
		}
		//during setting menus make sure that the user can't select anything else other than save
		if(clicked == true && EventSystem.current.gameObject != testbutton){
			EventSystem.current.SetSelectedGameObject(testbutton, new BaseEventData(EventSystem.current));
		}
		//grab the player object
		player = GameObject.FindGameObjectWithTag("Player");
		if (player != null){
			//if player exist in scene then grab the script and access its variables
			playerobj = player.GetComponent<PlayerController>();
			//call the functions to check the current player status
			checkinghealth(playerobj.health);
			highlightweapon(playerobj.weaponManager.weaponname);
			//Debug.Log(playerobj.weaponManager.weaponname);
		}
	}

}
