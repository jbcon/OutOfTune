using UnityEngine;
using System.Collections;

public class ArmandBoss : MonoBehaviour {

    //boss gameobject and normal "cutscene" Armand
    public GameObject boss;
    public GameObject human;

    private Animator bossAnimator;
    private Animator humanAnimator;
    private GameObject manager;
    private GameObject musicPlayer;

	// Use this for initialization
	void Start () {
        bossAnimator = boss.GetComponentInChildren<Animator>();
        humanAnimator = human.GetComponentInChildren<Animator>();
        manager = GameObject.FindGameObjectWithTag("CameraManager");
        musicPlayer = GameObject.FindGameObjectWithTag("MusicPlayer");
        Initialize();
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
        bossAnimator.SetTrigger("StartBattle");
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    IEnumerator waitForFinishedAnimation()
    {
        //wait for the human animation to finish
        //hardcoded because time
        yield return new WaitForSeconds(3.50f);
        StartBattle();
    }
}
