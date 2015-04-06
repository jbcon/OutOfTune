using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class UIManager : MonoBehaviour {
	public Animator settings;
	public bool clicked = false;
	public void StartGame(){
		Application.LoadLevel("Level 1");
	}
	public void Update(){
		if (Input.GetKeyDown((KeyCode.Escape)))
		{
			clicked = !clicked;
			settings.enabled = true;
			settings.SetBool("escPressed",clicked);
			Debug.Log("display settings");
		}
	}

}
