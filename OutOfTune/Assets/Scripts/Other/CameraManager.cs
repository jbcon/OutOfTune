using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public Vector2 deadZone;

    private GameObject player;
    private PlayerController pController;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pController = player.GetComponent<PlayerController>();
        transform.position = player.transform.position + new Vector3(deadZone.x, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

        float clampX;
        float clampY = Mathf.Clamp(transform.position.y, player.transform.position.y - deadZone.y,
                                   player.transform.position.y + deadZone.y);

        //have it depend on direction
        if (!pController.facingRight)
        {
            clampX = Mathf.Clamp(transform.position.x, player.transform.position.x + deadZone.x,
                                  player.transform.position.x - deadZone.x);
        }
        else
        {
            clampX = Mathf.Clamp(transform.position.x, player.transform.position.x - deadZone.x,
                                  player.transform.position.x + deadZone.x);
        }
        transform.position = new Vector3(clampX, clampY, 0.0f);
	}
}
