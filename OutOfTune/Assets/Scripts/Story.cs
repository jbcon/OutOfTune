﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine.EventSystems;
//using UnityEditor;
public class Story : MonoBehaviour {
	//dialogue to stop reppetition
	public bool level1start;
	public bool level1preboss;
	public bool level1postboss;
	public bool level2intro;
	public bool level2end;
	public bool level3intro;
	public bool level3preboss;
	public bool level3postboss;

	//images for the dailgue
	public Image faceMaestro;
	public Image faceleftRenee;
	public Image facerightRenee;
	public Image facerightPeter;
	public Image faceleftPeter;
	public Image faceArmand;

	//bool to show images for dialogue 
	//bad coding practice but works
	private bool Reneeshow;
	private bool Reneeshow2;
	private bool lvl1postbossshow;
	private bool lvl2into;
	private bool lvl2end;
	private bool lvl3intro;
	private bool lvl3preboss;
	private bool lvl3postboss;

	private Text story;
	private Image background;
	public Image dialogueimg1;
	public Image dialogueimg2;
	private List<string> story_book;
	//private List<string> storylines = new List<><string>();
	private GameObject conversation;
	public bool display;
	private bool continuetorenable;
	private int storyiterator;
	private int sizestory;
	private int sentence_iterator;
	// Use this for initialization
	void Start () {
		level1start = false;
		level1preboss = false;
		level1postboss = false;
		level2intro = false;
		level2end = false;
		level3intro = false;
		level3preboss = false;
		level3postboss = false;
		//sprite show during dialogue
		Reneeshow = false;
		Reneeshow2 = false;
		lvl1postbossshow= false;
		lvl2into= false;
		lvl2end= false;
		lvl3intro= false;
		lvl3preboss= false;
		lvl3postboss= false;
		display = false;
		continuetorenable = false;
		storyiterator = 0;
		conversation = GameObject.FindGameObjectWithTag("Story");
		story = conversation.GetComponentInChildren<Text>();
		background = conversation.GetComponentInChildren<Image>();
		/*
		if (background == null){
			Debug.Log("failed");
		}else{
			Debug.Log("sucess");
		}
		*/
		//var sr = new StreamReader(Application.dataPath +"/scripts/Dialogue.txt");

		//story.text = story_book[0];
		//conversation.GetComponent<GUIText>().enabled = false;
		//story.enabled = false;

		story.enabled = false;
		background.enabled = false;


	}
	void Rightside(Image newface){
		Image textbg = gameObject.GetComponentInChildren<Image>();
		dialogueimg1 = newface;
		dialogueimg1.transform.SetParent(gameObject.transform,false);
		Vector3 prefabpos = new Vector3(textbg.transform.position.x + 410, textbg.transform.position.y- 200,textbg.transform.position.z);
		dialogueimg1.transform.position = prefabpos;
	}
	void Leftside(Image newface){
		Image textbg = gameObject.GetComponentInChildren<Image>();
		dialogueimg2 = newface;
		dialogueimg2.transform.SetParent(gameObject.transform,false);
		Vector3 prefabpos = new Vector3(textbg.transform.position.x - 900, textbg.transform.position.y- 200,textbg.transform.position.z);
		dialogueimg2.transform.position = prefabpos;
	}
	public void delete(){
		//change();
		Destroy(dialogueimg1);
		Destroy(dialogueimg2);
	}
	void Update(){
		GameObject[] enemies;
		enemies =  GameObject.FindGameObjectsWithTag("enemy");
		GameObject player = GameObject.FindGameObjectWithTag("Player");
		if (display == true){
			//disable player movement
			player.GetComponent<PlayerController>().setpause();
			//disable enemy movement
			for (int disable = 0; disable < enemies.Count(); disable ++){
				enemies[disable].GetComponent<Health>().PauseGame();
			}
			gameObject.SetActive(true);
			if(storyiterator >= sizestory ){
                
				display= false;
				story.enabled = false;
                
                //trigger boss battle with Armand if necessary
                if (level3preboss)
                {
                    //after text is over, will trigger boss battle
                    GameObject boss = GameObject.FindGameObjectWithTag("Boss");
                    ArmandBoss a = boss.GetComponent<ArmandBoss>();
                    a.Initialize();
                }
				if(level3postboss){
					GameObject temp = GameObject.FindGameObjectWithTag("uimanager");
					temp.GetComponent<UIManager>().LoadMenu();
				}
				background.enabled = false;
                
				story_book.Clear();
				sizestory = 0;
				Destroy(dialogueimg1);
				Destroy(dialogueimg2);
                
			}else{
				background.enabled = true;
				story.text = story_book[storyiterator];
				story.enabled = true;
			}
			if(level1start == true && Reneeshow == false && sizestory > 0 && storyiterator == 0){
				Image faceofPeter = Instantiate(facerightPeter) as Image;
				Reneeshow = true;
				Rightside(faceofPeter);
			}else if(level1start == true && Reneeshow == true && sizestory > 0&& storyiterator == 2){
				Destroy(dialogueimg1);
				Image faceofRenee = Instantiate (facerightRenee) as Image;
				Reneeshow = false;
				Rightside(faceofRenee);
			}else if(level1preboss == true && Reneeshow2 == false && sizestory > 0 && storyiterator==0){
				Image faceofMaestro = Instantiate (faceMaestro) as Image;
				Reneeshow2 = true;
				Leftside(faceofMaestro);
			}else if(level1preboss == true && Reneeshow2 == true && sizestory > 0 && storyiterator==1){
				Image faceofRenee = Instantiate (facerightRenee) as Image;
				Reneeshow2 = false;
				Rightside(faceofRenee);
			}else if(level1postboss == true && lvl1postbossshow == false && sizestory > 0 && storyiterator==0){
				Image faceofMaestro = Instantiate (faceMaestro) as Image;
				lvl1postbossshow = true;
				Leftside(faceofMaestro);
				Image faceofRenee = Instantiate (facerightRenee) as Image;
				Rightside(faceofRenee);
			}/*else if(level1postboss == true && lvl1postbossshow == true && sizestory > 0 && storyiterator==1){
				Image faceofRenee = Instantiate (facerightRenee) as Image;
				lvl1postbossshow = false;
				Rightside(faceofRenee);
			}*/else if(level2intro == true && lvl2into == false && sizestory > 0 && storyiterator==0){
				Image faceofRenee = Instantiate (facerightRenee) as Image;
				lvl2into = true;
				Rightside(faceofRenee);
			}else if(level2end == true && lvl2end == false && sizestory > 0 && storyiterator==0){
				Image faceofRenee = Instantiate (faceleftRenee) as Image;
				lvl2end = true;
				Leftside(faceofRenee);
			}else if(level3intro == true && lvl3intro == false && sizestory > 0 && storyiterator==2){
				Image faceofRenee = Instantiate (faceleftRenee) as Image;
				lvl3intro = true;
				Rightside(faceofRenee);
			}else if(level3preboss == true && lvl3preboss == false && sizestory > 0 && storyiterator==0){
				Image faceofRenee = Instantiate (faceleftRenee) as Image;
				lvl3preboss = true;
				Leftside(faceofRenee);
			}else if(level3preboss == true && lvl3preboss == true && sizestory > 0 && storyiterator==1){
				Image faceofArmand = Instantiate (faceArmand) as Image;
				lvl3preboss = true;
				Rightside(faceArmand);
			}else if (level3postboss== true && lvl3postboss == false && sizestory >0 && storyiterator ==0){
				Image faceofarmand = Instantiate(faceArmand) as Image;
				lvl3postboss = true;
				Leftside(faceArmand);
			}else if (level3postboss== true && lvl3postboss == true && sizestory >0 && storyiterator == 1){
				Image faceofRenee = Instantiate(facerightRenee) as Image;
				lvl3postboss = false;
				Rightside(faceofRenee);
			}else if (level3postboss== true && lvl3postboss == false && sizestory >0 && storyiterator == 2){
				Destroy(dialogueimg2);
				lvl3postboss = true;
			}else if (level3postboss== true && lvl3postboss == true && sizestory >0 && storyiterator == 10){
				Image faceofpeter = Instantiate(faceleftPeter) as Image;
				lvl3postboss = false;
				Leftside(faceofpeter);
			}
			if (Input.GetKeyDown((KeyCode.Return))){
				storyiterator ++;
			}
		}else{
			//reenable player movement
            
			//delete();
			//reenable enemy movement
			if(continuetorenable == true){
				for (int disable2 = 0; disable2 < enemies.Count(); disable2 ++){
					enemies[disable2].GetComponent<Health>().UnPauseGame();
				}
				if (player)
					player.GetComponent<PlayerController>().unpause();
				continuetorenable = false;
			}
			story.enabled = false;
			background.enabled = false;

		}
	}
	public void change()
	{
		story.enabled = false;
		background.enabled = false;
		level1start = false;
		level1preboss = false;
		level1postboss = false;
		level2intro = false;
		level2end = false;
		level3intro = false;
		level3preboss = false;
		display = false;
		storyiterator = 0;
		sizestory = 0;
		story_book.Clear();
		///gameObject.SetActive(false);
	}
	public void reader(List<string> datapath, string boolvalue){
		storyiterator = 0;
		if(boolvalue == "level1start"){
			level1start = true;
		}else if (boolvalue == "level1preboss"){
			level1preboss = true;
			level1start = false;
		}else if (boolvalue == "level1postboss"){
			level1postboss = true;
			level1preboss = false;
		}else if (boolvalue == "level2intro"){
			level2intro = true;
			level1postboss = false;
		}else if (boolvalue == "level2end"){
			level2end = true;
			level2intro = false;
		}else if (boolvalue == "level3intro"){
			level3intro = true;
			level2end = false;
		}else if (boolvalue == "level3preboss"){
			level3preboss = true;
			level3intro = false;
		}else if(boolvalue == "level3postboss"){
			level3postboss =true;
			level3preboss = false;
		}
		story_book = datapath;
		sizestory = story_book.Count();
		Debug.Log(level1start);
		/*
		for (int i = 0; i < story_book.Count(); i++){
			Debug.Log(story_book[i]);
		}*/
		/*var contents = datapath.ReadToEnd();
		var lines = contents.Split("\n"[0]);
		int temp = 0;
		for ( temp = 0 ; temp < lines.Length ; temp ++){
			story_book.Add(lines[temp]);
			sizestory ++;
		}*/
		display = true;
		continuetorenable = true;
	}
}
