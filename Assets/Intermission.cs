using UnityEngine;
using System.Collections;

public class Intermission : MonoBehaviour {

	private float startTime = 0f;
	public float intermissionTime = 5f;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {
		float elapsedTime = Time.time - startTime;
		if (elapsedTime > intermissionTime) {
			Application.LoadLevel("WarSimulator");

		}

	}
}
