using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum BossState
{
    Pushing,
    Drumming,
    Clapping
}

public struct Hand
{
    public GameObject fist;
    public GameObject palm;
}

public class ArmandBoss : MonoBehaviour {

    //boss gameobject and normal "cutscene" Armand
    public GameObject boss;
    public GameObject human;

    //triggers interrupt of the animation if recieves enough damage?
    public float handHealth = 100f;
    public float totalHealth = 300f;

    //time between attacks
    public float interval = 5f;
    public BossState state;

    //do the hands deal damage?
    public bool attacking;

    private Hand left;
    private Hand right;

    private BossState[] possibleStates = { BossState.Clapping, BossState.Drumming, BossState.Pushing };
    private Animator bossAnimator;
    private Animator humanAnimator;
    private GameObject manager;
    private GameObject musicPlayer;

	// Use this for initialization
	void Start () {
        humanAnimator = human.GetComponentInChildren<Animator>();
        manager = GameObject.FindGameObjectWithTag("CameraManager");
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");

        InitHands();
        //for debugging
        if (!GameObject.Find("UIManager"))
        {
            Initialize();
        }
	}

    void InitHands()
    {
        left = new Hand();
        left.palm = GameObject.FindGameObjectWithTag("LeftPalm");
        left.fist = GameObject.FindGameObjectWithTag("LeftFist");

        right = new Hand();
        right.palm = GameObject.FindGameObjectWithTag("RightPalm");
        right.fist = GameObject.FindGameObjectWithTag("RightFist");
    }

    public void Damage(float dmg)
    {
        totalHealth -= dmg;
        handHealth -= dmg;
        if (handHealth <= 0)
        {
            handHealth = 100;
            bossAnimator.SetTrigger("Interrupt");
        }
    }

    //enable colliders on the hands for start and end of attack
    public void EnableHands(bool activate)
    {
        attacking = activate;
    }

    //runs after the pre-boss dialogue completes
    public void Initialize()
    {
        /* TODO:
         * Trigger "human's" jump offscreen
         * Trigger boss's entrance
         * Begin main activity loop
         */
        humanAnimator.SetTrigger("StartBattle");
        musicPlayer.GetComponent<AudioSource>().Play();
        StartCoroutine("waitForFinishedAnimation");
    }

    void StartBattle()
    {
        human.SetActive(false);
        boss.SetActive(true);
        bossAnimator = boss.GetComponentInChildren<Animator>();
        bossAnimator.SetTrigger("StartBattle");
        StartCoroutine("makeDecision");
    }
	
	// Update is called once per frame
	void Update () {
        if (!attacking)
        {
            handHealth = 100;
        }
	}

    //determines action based on current state
    void evaluateState()
    {
        switch (state)
        {
            case BossState.Clapping:
                bossAnimator.SetTrigger("Clap");
                break;
            case BossState.Drumming:
                bossAnimator.SetTrigger("Drum");
                break;
            case BossState.Pushing:
                bossAnimator.SetTrigger("Push");
                break;
            default:
                break;
        }
        StartCoroutine("makeDecision");
    }

    IEnumerator waitForFinishedAnimation()
    {
        //wait for the human animation to finish
        //hardcoded because time
        yield return new WaitForSeconds(3.50f);
        StartBattle();
    }

    IEnumerator makeDecision()
    {
        //needs to wait until attack animation is done
        //likely send a message from attached behavior script on state
        yield return new WaitForSeconds(interval);
        int choice = Random.Range(0, 3);
        state = possibleStates[choice];
        Debug.Log("Decision made: " + state);
        evaluateState();

    }
}
