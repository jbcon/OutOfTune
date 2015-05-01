using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
public class Story : MonoBehaviour {
	public bool level1start;
	public bool level1preboss;
	public bool level1postboss;
	public bool level2intro;
	public bool level2end;
	public bool level3intro;
	public bool level3preboss;

	private Text story;
	private Image background;
	private List<string> story_book = new List<string>();
	//private List<string> storylines = new List<><string>();
	private GameObject conversation;
	public bool display;
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
		display = false;
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
	void Update(){
		if (display == true){
			if(storyiterator >= sizestory ){
				display= false;
				story.enabled = false;
				background.enabled = false;
				story_book.Clear();
				sizestory = 0;
			}else{
				background.enabled = true;
				story.text = story_book[storyiterator];
				story.enabled = true;
			}

			if (Input.GetKeyDown((KeyCode.Return))){
				storyiterator ++;
			}
		}else{
			story.enabled = false;
			background.enabled = false;
		}
	}
	public void reader(StreamReader datapath, string boolvalue){
		storyiterator = 0;
		if(boolvalue == "level1start"){
			level1start = true;
		}else if (boolvalue == "level1preboss"){
			level1preboss = true;
		}else if (boolvalue == "level1postboss"){
			level1postboss = true;
		}else if (boolvalue == "level2intro"){
			level2intro = true;
		}else if (boolvalue == "level2end"){
			level2end = true;
		}else if (boolvalue == "level3intro"){
			level3intro = true;
		}else if (boolvalue == "level3preboss"){
			level3preboss = true;
		}
		var contents = datapath.ReadToEnd();
		var lines = contents.Split("\n"[0]);
		int temp = 0;
		for ( temp = 0 ; temp < lines.Length ; temp ++){
			story_book.Add(lines[temp]);
			sizestory ++;
		}
		display = true;
	}
}
