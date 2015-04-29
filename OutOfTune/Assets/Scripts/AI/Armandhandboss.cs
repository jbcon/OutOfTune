using UnityEngine;
using System.Collections;

public class Armandhandboss : MonoBehaviour {
	public GameObject player;
	private bool fight;				//initialize the fight
    private bool attack;
	private float speed;
	private float counter;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		fight = false;
		speed = 2;
		counter = 0;
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if (distance < 10){
			fight = true;
		}
		if (fight == true){
			int method = Random.Range(0,3);		//randomly choose one of three ai behaviors
			//StartCoroutine("Slap");
		}else{
			Debug.Log ("stop the coroutines");
		}
	}
	void Slap()
	{
		while(counter < 10){
			//going to follow player on top of him then wait then smash
			Vector3 point = player.gameObject.transform.InverseTransformPoint (gameObject.transform.position);
			if (point.x > 0) {
				//facing right
				gameObject.transform.Translate (Vector3.left * speed * Time.deltaTime);
			} else if (point.x < 0) {
				//facing left
				gameObject.transform.Translate (Vector3.right * speed * Time.deltaTime);
			}else {
				//righht on top of it
				attack = true;
			}
			fight = false;
		}

	}
}
