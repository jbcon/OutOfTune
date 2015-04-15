using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;
public class Story : MonoBehaviour {
	private Text story;
	private List<string> story_book = new List<string>();
	private GameObject conversation;
	// Use this for initialization

	void Start () {
		conversation = GameObject.Find("storysetting");
		story = GetComponent<Text>();

		var sr = new StreamReader(Application.dataPath +"/scripts/story_proto.txt");
		var contents = sr.ReadToEnd();
		var lines = contents.Split("\n"[0]);
		int temp = 0;
		for ( temp = 0 ; temp < lines.Length ; temp ++){
			story_book.Add(lines[temp]);
		}
		story.text = story_book[0];
		//conversation.GetComponent<GUIText>().enabled = false;
		story.enabled = false;
	}

}
