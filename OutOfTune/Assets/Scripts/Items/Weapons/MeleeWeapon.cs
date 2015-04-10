using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        collider.gameObject.GetComponent<Health>().Defend(1f);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
