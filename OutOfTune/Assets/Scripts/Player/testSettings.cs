using UnityEngine;
using System.Collections;

public class testSettings : MonoBehaviour {

	public void Awake(){
		DontDestroyOnLoad(gameObject);
	}
}
