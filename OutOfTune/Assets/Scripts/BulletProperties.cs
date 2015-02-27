using UnityEngine;
using System.Collections;

public class BulletProperties : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
