using UnityEngine;
using System.Collections;

public class ConductorV2 : MonoBehaviour {
	//float for precision
	public float beatsPerMinute = 120.0f;
	public int beatsPerMeasure = 4;
	public int beat = 0;
	
	private AudioSource audio;
	
	private float currTime;
	private float beatsPerSecond;
	private float secondsPerBeat;
	
	//get point t between these two to interpolate between current and last beats
	private float nextBeat;
	private float lastBeat;
	private float t;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource>();
		audio.PlayDelayed(1);
		currTime = 0;
		beatsPerSecond = beatsPerMinute / 60.0f;
		secondsPerBeat = 1 / beatsPerSecond;
		lastBeat = 0;
		nextBeat = secondsPerBeat;
	}
	IEnumerator Beats(){
		//every frame
		/*
		 *basically gotta do a beat per second
		 *after calculating it
		 *this function will do a send message all
		 *for enemies to move 
		 *for loop and every 8th beat or something send out a messafe
		 *
		 *
		  */

		yield return null;
	}
	// Update is called once per frame
	void Update () {
		currTime = audio.time;
		//if we have arrived at next beat
		if (currTime > nextBeat+secondsPerBeat)
		{
			lastBeat += secondsPerBeat;
			nextBeat += secondsPerBeat;
			Debug.Log("BEAT " + beat);
			
			if (beat < beatsPerMeasure-1) beat++;
			else beat = 0;
		}
	}
}
