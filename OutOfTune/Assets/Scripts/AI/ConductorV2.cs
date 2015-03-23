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

	//metronome vairabls
	public int Base; // th ebeat number
	public int Step; // acts as the maount of peats per measure
	public float BPM;
	public int CurrentStep;	
	public int CurrentMeasure;

	private float interval;
	private float nextTime;
	//eight beat work in progress
	private float eight_interval;
	private float eight_nextime;
	public int eight_currentStep;

	// Use this for initialization
	void Start () {

		BPM = 120.0f;
		audio = gameObject.GetComponent<AudioSource>();
		audio.PlayDelayed(1);
		currTime = 0;
		beatsPerSecond = beatsPerMinute / 60.0f;
		secondsPerBeat = 1 / beatsPerSecond;
		lastBeat = 0;
		nextBeat = secondsPerBeat;

		// calculate the seconds for each type of beat
		float Quarter = secondsPerBeat / 4;
		Step = 4;
		Base = 4;


		//then load the cooroutine for the beats of the song
		//StartCoroutine ("Beats",secondsPerBeat);
		//StartCoroutine ("QuarterBeat",Quarter);


		//metronome version of doing this
		StartMetronome();
	
	}
	public void StartMetronome(){
		//StopCoroutine("EightBeat");
		StopCoroutine("Beating");
		CurrentStep = 1;			//start first step of new  measure
		//eight_currentStep = 1;

		var multiplier = Base/4f; //multiplier based on quarter notewith signature base 4
		var tmpInterval = 60f / BPM; //getting the second per beat
		interval = tmpInterval/multiplier; //modify interval based on multiplier

		eight_interval = (60f/BPM)/((Base+1) / 4f);
		eight_nextime = Time.time;
		nextTime = Time.time;
		StartCoroutine("Beating");
		//StartCoroutine("EightBeat");
	}
	IEnumerator EightBeat(){
		for(;;){
			eight_nextime += eight_interval;
			yield return new WaitForSeconds(eight_nextime - Time.time); 
			eight_currentStep++;
			Debug.Log("eight" + eight_currentStep);
			if (eight_currentStep > Step ){
				eight_currentStep = 1;
			}
		}
	}
	IEnumerator Beating(){

		for (;;){

			//this is where the broadcast message should be
			nextTime += interval;

			yield return new WaitForSeconds(nextTime - Time.time);// wait interval seconds before next beat
			CurrentStep++;
			Debug.Log(CurrentStep);
			if (CurrentStep > Step){
				CurrentStep = 1;
				CurrentMeasure++;
			}

		}

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
			yield return new WaitForSeconds (.125f);
		}
	}
	// Update is called once per frame
	void Update () {


	}
}
