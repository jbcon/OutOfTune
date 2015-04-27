using UnityEngine;
using System.Collections;

public class Checkpointloadsave : MonoBehaviour {
	public GameObject player;
	private bool saved;
	void Start(){
		saved = false;
		player = GameObject.FindGameObjectWithTag("Player");
	}
	/*
	void OnCollisionEnter2D(Collision2D collider)
	{
		Debug.Log("collision" + collider.gameObject.tag);
		if (collider.gameObject.tag == "Player")
		{
			Debug.Log("hitting");
			player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerController>().newcheckpoint();
			GameObject saving = GameObject.FindGameObjectWithTag("save");
			saving.GetComponent<SaveLoad>().setCheckpoint();
		}   
	}*/
	void Update(){
		float distance = Vector3.Distance(player.transform.position, gameObject.transform.position);
		if (distance <= 20 && saved == false){
			saved = true;
			player.GetComponent<PlayerController>().newcheckpoint();
			GameObject saving = GameObject.FindGameObjectWithTag("save");
			saving.GetComponent<SaveLoad>().setCheckpoint();
			Debug.Log ("saving");
		}
	}
}
