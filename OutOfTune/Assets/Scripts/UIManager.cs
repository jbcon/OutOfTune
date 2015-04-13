using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour {
	public Animator settings;
	public Animator Save;
	public bool clicked = false;
	private List<string> buttonnames = new List<string>();
	public void Awake(){
		DontDestroyOnLoad(gameObject);
	}
	public void Start(){
	}
	public void StartGame(){
		Application.LoadLevel("Level 1");
	}
	public void LoadLevel2(){
		Application.LoadLevel("Level 2");
	}
	public void LoadLevel3(){
		Application.LoadLevel("Level 3");
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
	}

}
