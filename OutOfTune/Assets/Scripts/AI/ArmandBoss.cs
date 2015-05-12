using UnityEngine;
using System.Collections;

public class ArmandBoss : MonoBehaviour {

    //boss gameobject and normal "cutscene" Armand
    public GameObject boss;
    public GameObject human;

    private Animator bossAnimator;
    private Animator humanAnimator;

	// Use this for initialization
	void Start () {
        bossAnimator = boss.GetComponentInChildren<Animator>();
        humanAnimator = human.GetComponentInChildren<Animator>();
	}

    //runs after the pre-boss dialogue completes
    void Initialize()
    {
        /* TODO:
         * Trigger "human's" jump offscreen
         * Trigger boss's entrance
         * Begin main activity loop
         */


    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
