using UnityEngine;
using System.Collections;

public class conductor_prototype : MonoBehaviour {
	public GameObject[] minions;
	// Use this for initialization
	void Start () {
		minions = GameObject.FindGameObjectsWithTag ("goomba");
	}
	
	// Update is called once per frame
	void Update () {
		float[] spectrum = new float[1024];
		audio.GetSpectrumData( spectrum, 0, FFTWindow.Hamming);
		//Debug.Log (spectrum [54]);
		//float c1 = spectrum [3] + spectrum [2] + spectrum [4];

		//Debug.Log ("Samples:" + spectrum[1]);
		/*
		 * frequency of spectrum changing movement speed
		if (spectrum [1] < .0001) {
			for (int k = 0; k < minions.Length ; k++){
				simple_AI op = (simple_AI)minions[k].GetComponent(typeof(simple_AI));
				op.update_x(10);
				Debug.Log ("success");
			}
		}*/

		 // draws a visualizer of the music
		int i = 1;
		while (i < 255) {
			Debug.DrawLine(new Vector3(i - 1, spectrum[i] + 10, 0), new Vector3(i, spectrum[i + 1] + 10, 0), Color.red);
			Debug.DrawLine(new Vector3(i - 1, Mathf.Log(spectrum[i - 1]) + 10, 2), new Vector3(i, Mathf.Log(spectrum[i]) + 10, 2), Color.cyan);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), spectrum[i - 1] - 10, 1), new Vector3(Mathf.Log(i), spectrum[i] - 10, 1), Color.green);
			Debug.DrawLine(new Vector3(Mathf.Log(i - 1), Mathf.Log(spectrum[i - 1]), 3), new Vector3(Mathf.Log(i), Mathf.Log(spectrum[i]), 3), Color.yellow);
			i++;
		}
	}
}
