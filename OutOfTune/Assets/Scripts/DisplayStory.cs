using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;
public class DisplayStory : MonoBehaviour {
	public GameObject player;
	public GameObject storyobj;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		storyobj = GameObject.FindGameObjectWithTag("Story");
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if(distance < 10){
			if(gameObject.name == "Level1intro" &&	storyobj.GetComponent<Story>().level1start == false ){
				var sr = new StreamReader(Application.dataPath +"/scripts/level1intro.txt");
				storyobj.GetComponent<Story>().reader(sr, "level1start");
			}else if(gameObject.name == "level1preboss" &&	storyobj.GetComponent<Story>().level1preboss == false ){
				var sr = new StreamReader(Application.dataPath +"/scripts/level1preboss.txt");
				storyobj.GetComponent<Story>().reader(sr, "level1preboss");
			}else if(gameObject.name == "level1postboss" &&	storyobj.GetComponent<Story>().level1postboss == false ){
				var sr = new StreamReader(Application.dataPath +"/scripts/level1postboss.txt");
				storyobj.GetComponent<Story>().reader(sr, "level1postboss");
			}else if(gameObject.name == "level2intro" &&	storyobj.GetComponent<Story>().level2intro == false ){
				var sr = new StreamReader(Application.dataPath +"/scripts/level2intro.txt");
				storyobj.GetComponent<Story>().reader(sr, "level2intro");
			}else if(gameObject.name == "level2end" &&	storyobj.GetComponent<Story>().level2end == false ){
				var sr = new StreamReader(Application.dataPath +"/scripts/level2end.txt");
				storyobj.GetComponent<Story>().reader(sr, "level2end");
			}else if(gameObject.name == "level3intro" &&	storyobj.GetComponent<Story>().level3intro == false ){
				var sr = new StreamReader(Application.dataPath +"/scripts/level3intro.txt");
				storyobj.GetComponent<Story>().reader(sr, "level3intro");
			}else if(gameObject.name == "level3preboss" &&	storyobj.GetComponent<Story>().level3preboss == false ){
				var sr = new StreamReader(Application.dataPath +"/scripts/level3preboss.txt");
				storyobj.GetComponent<Story>().reader(sr, "level3preboss");
			}

		}
	}
}
