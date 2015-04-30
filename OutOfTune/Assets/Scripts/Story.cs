using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
public class Story : MonoBehaviour {
	private Text story;
	private Image background;
	private List<string> story_book = new List<string>();
	//private List<string> storylines = new List<><string>();
	private GameObject conversation;
	public bool display;
	private int storyiterator;
	private int sentence_iterator;
	// Use this for initialization

	void Start () {
		display = false;
		storyiterator = 1;
		sentence_iterator = 0;
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
		var sr = new StreamReader(Application.dataPath +"/scripts/Dialogue.txt");
		var contents = sr.ReadToEnd();
		var lines = contents.Split("\n"[0]);
		int temp = 0;
		for ( temp = 0 ; temp < lines.Length ; temp ++){
			story_book.Add(lines[temp]);
		}
		//story.text = story_book[0];
		//conversation.GetComponent<GUIText>().enabled = false;
		//story.enabled = false;

		story.enabled = false;
		background.enabled = false;


	}
	void Update(){
		if (display == true){
			background.enabled = true;
			var storylines = story_book[storyiterator].Split(" "[0]);
			if(storylines[0] == "Armand:" ||  storylines[0] == "Narrator:" ||
			   storylines[0] == "Renee:" || storylines[0] == "Peter:" ||
			   storylines[0] == "Maestro:"
			   )
			{
				story.text = story_book[storyiterator];
			}else{
				storyiterator++;
				//be at end so
				display = false;
			}
			story.enabled = true;
			if (Input.GetKeyDown((KeyCode.Return))){
				storyiterator ++;
			}
		}else{
			story.enabled = false;
			background.enabled = false;
		}
	}
}
