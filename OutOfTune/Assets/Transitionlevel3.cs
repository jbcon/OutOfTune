using UnityEngine;
using System.Collections;

public class Transitionlevel3 : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D collision)
	{
		int playerLayer = LayerMask.NameToLayer("Player");
		
		if (collision.gameObject.layer == playerLayer)
		{
			//Debug.Log("HIT!");
			Application.LoadLevel("Level 3");
		}
	}
}
