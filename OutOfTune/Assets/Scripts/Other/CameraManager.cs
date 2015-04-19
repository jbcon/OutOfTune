using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    public Vector2 targetRange;

    private GameObject player;
    private PlayerController pController;
    private Vector3 target;
    private bool currentDirection;
    private float t = 0, u = 0;

	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        pController = player.GetComponent<PlayerController>();
        transform.position = target + new Vector3(targetRange.x, 0.0f, 0.0f);
	}
	
	// Update is called once per frame
	void Update () {

        //change target based on direction

        if (pController.facingRight != currentDirection)
        {
            t = 0;
            u = 0;
        }

        if (pController.facingRight)
        {
            target = player.transform.position + new Vector3(targetRange.x, 0.0f);
        }
        else
        {
            target = player.transform.position - new Vector3(targetRange.x, 0.0f);
        }
        currentDirection = pController.facingRight;  


        if (transform.position != target)
        {
            Debug.Log("Distance to target: " + (target - transform.position));
            if (transform.position.x != target.x)
                t += Time.deltaTime / 20f;
            else
                t = 0;
            if (transform.position.y != target.y)
                u += Time.deltaTime / 20f;
            else
                u = 0;
            Vector2 newPos = new Vector2(
                Mathf.Lerp(transform.position.x, target.x, t),
                Mathf.Lerp(transform.position.y, target.y, u));
            transform.position = newPos;
        }
        else
        {
            t = 0;
            u = 0;
        }
        Debug.Log("T = " + t + " U = " + u);



        /*
        float clampX;
        float clampY = Mathf.Clamp(transform.position.y, target.y - deadZone.y,
                                   target.y + deadZone.y);
        
        //have it depend on direction
        if (!pController.facingRight)
        {
            clampX = Mathf.Clamp(transform.position.x, target.x + deadZone.x,
                                  target.x - deadZone.x);
        }
        else
        {
            clampX = Mathf.Clamp(transform.position.x, target.x - deadZone.x,
                                  target.x + deadZone.x);
        }
         
        transform.position = new Vector3(clampX, clampY, 0.0f);
         */
	}
}
