using UnityEngine;
using System.Collections;

public class bossExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Destroy(gameObject, 10.0f);
	}
}
