using UnityEngine;
using System.Collections;

public class Dontdestroy : MonoBehaviour {

	void Awake(){
		DontDestroyOnLoad(gameObject);
	}
}
