using UnityEngine;
using System.Collections;


public enum BossState
{
    Resting,
    Slapping,
    Drumming,
    Clapping,
    BigAimSlapping
}

public class Armandhandboss : MonoBehaviour {
	public GameObject player;

    public Sprite palm;
    public Sprite fist;

    public bool bossActive;

    private GameObject leftHand;
    private GameObject rightHand;
    private Animator animator;

    private BossState bossState;
	private float speed;
	private float counter;
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
        leftHand = GameObject.FindGameObjectWithTag("LeftHand");
        rightHand = GameObject.FindGameObjectWithTag("RightHand");
        animator = GetComponent<Animator>();
		speed = 2;
		counter = 0;
        bossState = BossState.Resting;
        bossActive = false;
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            GameObject camera = GameObject.FindGameObjectWithTag("CameraManager");
            CameraManager cManager = camera.GetComponent<CameraManager>();
            bossActive = true;
        }
    }


	// Update is called once per frame
	void Update () {
        if (bossActive)
        {
            Debug.Log(bossState);
            switch (bossState)
            {
                //no action
                case BossState.Resting:
                    //choose a new action
                    StartCoroutine("Resting");
                    break;

                //rhythmic drumming on stage
                case BossState.Drumming:
                    StartCoroutine("Drumming");
                    break;

                //idk
                case BossState.Slapping:
                    StartCoroutine("Slapping");
                    break;

                //aims for player, then slams the ground
                case BossState.BigAimSlapping:
                    StartCoroutine("BigAimSlapping");
                    break;

                //clapping his hands
                case BossState.Clapping:
                    StartCoroutine("Clapping");
                    break;
            }
        }
	}

    IEnumerator Resting()
    {
        //randomly choose new state every second or few
        yield return new WaitForSeconds(Random.Range(5, 10));
        int attack = Random.Range(0, 4);
        switch (attack)
        {
            case 0:
                bossState = BossState.Drumming;
                break;

            case 1:
                bossState = BossState.Slapping;
                break;

            case 2:
                bossState = BossState.BigAimSlapping;
                break;

            case 3:
                bossState = BossState.Clapping;
                break;
        }
    }

    IEnumerator Drumming()
    {
        //just does the drumming animation
        animator.SetBool("Drumming", true);
        yield return new WaitForSeconds(3.0f);
        bossState = BossState.Resting;
        animator.SetBool("Drumming", false);
    }

    IEnumerator Slapping()
    {
        yield return new WaitForSeconds(5.0f);
        bossState = BossState.Resting;
    }

    IEnumerator BigAimSlapping()
    {
        yield return new WaitForSeconds(5.0f);
        bossState = BossState.Resting;
    }

    IEnumerator Clapping()
    {
        yield return new WaitForSeconds(5.0f);
        bossState = BossState.Resting;
    }

    
	/*void Slap()
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
			}
		}

	}*/
}
