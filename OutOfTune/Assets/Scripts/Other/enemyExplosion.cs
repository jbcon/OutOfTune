using UnityEngine;
using System.Collections;

public class enemyExplosion : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GameObject.Destroy(gameObject, 5.0f);
	}
}
