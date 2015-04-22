using UnityEngine;
using System.Collections;

public class Checkpointloadsave : MonoBehaviour {
	public GameObject player;

	void OnCollisionEnter2D(Collision2D collider)
	{
		Debug.Log("collision" + collider.gameObject.tag);
		if (collider.gameObject.tag == "Player")
		{
			Debug.Log("hitting");
			player = GameObject.FindGameObjectWithTag("Player");
			player.GetComponent<PlayerController>().newcheckpoint();
			GameObject saving = GameObject.FindGameObjectWithTag("save");
			Vector3 temp = player.transform.position;
			saving.GetComponent<SaveLoad>().setCheckpoint(temp);
		}   
	}
}
