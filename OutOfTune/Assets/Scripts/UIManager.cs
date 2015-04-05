using UnityEngine;
using System.Collections;

public class UIManager : MonoBehaviour {
	public int testcounter =0;
	public void StartGame(){
		Application.LoadLevel("Level 1");
	}
	public void increment(){
		testcounter++;
	}
	public void getcounter(){
		Debug.Log(testcounter);
	}
}
