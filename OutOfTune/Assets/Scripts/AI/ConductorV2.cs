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

		// calculate the seconds for each type of beat
		float Quarter = secondsPerBeat / 4;

		//then load the cooroutine for the beats of the song
		StartCoroutine ("Beats",secondsPerBeat);
		StartCoroutine ("QuarterBeat",Quarter);
	}
	IEnumerator Beats(float delta_time){

		//While the game is running for each beat
		//either print out the beat every beat
		while (true) {
			Debug.Log ("Beat"+ delta_time);
			//BroadcastMessage ("Attack", 2);
			yield return new WaitForSeconds (delta_time);
		}
	}
	IEnumerator QuarterBeat(float delta_time){
		//while game is true
		while (true) {
			//every quarterbeat cann either broadcast message or print stuff
			Debug.Log ("quaterBeat" + delta_time);
			//BroadcastMessage ("QuarterAttack", 10);
			yield return new WaitForSeconds (delta_time);
		}
	}
	// Update is called once per frame
	void Update () {


	}
}
