using UnityEngine;
using System.Collections;

public class HandleReticle : MonoBehaviour {

	// So it's always rendered in front of terrain
	void Update () {
        transform.position = new Vector3(transform.position.x, transform.position.y, -Mathf.Abs(transform.position.z));
	}
}
