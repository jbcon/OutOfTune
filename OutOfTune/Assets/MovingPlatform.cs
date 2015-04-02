using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour {

	public int speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.position.y < -298 || transform.position.y > -222) {
			speed *= -1;
		}
		transform.Translate(Vector3.up * speed);
	}
}
