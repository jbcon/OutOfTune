using UnityEngine;
using System.Collections;
using UnityEditor;
public class LevelTransitions : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string levelname = EditorApplication.currentScene;
		Debug.Log (levelname);
	}
}
