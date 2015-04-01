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

	//metronome variables
	public int Base;//the beat number
	public int Step;//acts as the beats per measure
	public float BPM;
	public int CurrentStep;
	public int CurrentMeasure;

	private float interval;
	private float nextTime;
	//eight beat work in progress

	private float EightInterval;
	private float EightNexttime;
	public int EightCurrentStep;


	// getting the list of AI that correspond to it
	public GameObject[] BrassyObj;
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
		//StartCoroutine ("Beats",secondsPerBeat);
		//StartCoroutine ("QuarterBeat",Quarter);

		//collect/ fill the brassy ai list wiht the objecets
		BrassyObj = (GameObject[])GameObject.FindGameObjectsWithTag("Brassy");

		//start metronome version
		BPM =120;
		Step = 4;
		Base = 4;
		StartMetronome();
	}
	public void StartMetronome(){
		StopCoroutine("EightBeat");
		StopCoroutine("Beating");
		CurrentStep = 1;	//start first step of new measure
		EightCurrentStep = 1;

		var multiplier = Base/4f; //multiplier based on quarter note with signature Base 4
		var tmpInterval = 60f/BPM; //getting scecond per beat
		interval = tmpInterval/multiplier; //modify interval based on multiplier
		//kind like 16 notes for the bottom
		//EightInterval = (60f/BPM)/4;
		EightInterval = (60f/BPM)/1.8f;
		EightNexttime = Time.time;
		nextTime = Time.time;

		StartCoroutine("Beating");
		StartCoroutine("EightBeat");
	}
	IEnumerator EightBeat(){
		for (;;){
			EightNexttime += EightInterval;
			//Debug.Log ("eight " +EightCurrentStep);

			foreach (GameObject BrassyAiObj in BrassyObj){
				//Debug.Log("asd");
                if (BrassyAiObj != null)
				    BrassyAiObj.gameObject.BroadcastMessage("FireBullet");
			}
			yield return new WaitForSeconds(EightNexttime - Time.time);
			EightCurrentStep++;
			if ( EightCurrentStep > (8)){
				EightCurrentStep = 1;
			}
		}
	}
	IEnumerator Beating(){
		for(;;){
			//this is where Broadcast messag eshould be
			nextTime +=interval;
			yield return new WaitForSeconds(nextTime - Time.time); //asd
			CurrentStep ++;
			//Debug.Log(CurrentStep);
			if ( CurrentStep > Step){
				CurrentStep = 1;
				CurrentMeasure ++;
			}
		}
	}
	IEnumerator Beats(float delta_time){

		//While the game is running for each beat
		//either print out the beat every beat
		while (true) {
			//Debug.Log ("Beat"+ delta_time);
			//BroadcastMessage ("Attack", 2);
			yield return new WaitForSeconds (delta_time);
		}
	}
	IEnumerator QuarterBeat(float delta_time){
		//while game is true
		while (true) {
			//every quarterbeat cann either broadcast message or print stuff
			//Debug.Log ("quaterBeat" + beat);
            beat++;
            if (beat >= beatsPerMeasure) beat = 0;
			//BroadcastMessage ("QuarterAttack", 10);
			yield return new WaitForSeconds (delta_time*4);
		}
	}
	// Update is called once per frame
	void Update () {


	}
}
