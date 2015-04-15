using UnityEngine;
using System.Collections;

public class MeleeWeapon : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        Health health = collider.gameObject.GetComponent<Health>();
        if (health)
        {
            health.Defend(1f);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
