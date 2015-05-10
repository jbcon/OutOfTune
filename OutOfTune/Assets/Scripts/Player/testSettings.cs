using UnityEngine;
using System.Collections;

public class testSettings : MonoBehaviour {
	public static testSettings Instance;
	void Awake(){
		if(Instance){
			DestroyImmediate(gameObject);
		}else{
			DontDestroyOnLoad(gameObject);
			Instance = this;
		}
	}
	/*
	void Awake(){
		DontDestroyOnLoad(gameObject);
	}*/
}
