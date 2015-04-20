using UnityEngine;
using System.Collections;

public class Peck : MonoBehaviour {

	public bool pecking = false;
	public float peckDuration = 0.1f;
	public float peckAngle = 35;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	
		if (Input.GetKey(KeyCode.Mouse0) && !pecking) StartCoroutine("PeckRoutine");
	
	}

	IEnumerator PeckRoutine() {
		TrainingArena ta = GameObject.FindWithTag("TrainingArena").GetComponent<TrainingArena>();
		float initProgress = ta.GetProgress ();

		pecking = true;
		bool peckingPhase = true;
		float time = 0;
		float peckProgress = 0;
		Vector3 initialPosition = transform.localPosition;

		while (peckingPhase) {
			peckProgress = time / peckDuration;
			if (peckProgress > 1)
			{
				peckingPhase = false;
				peckProgress = 1;
			}

			transform.RotateAround(transform.parent.transform.position, transform.parent.transform.right, (peckAngle / peckDuration) * Time.deltaTime);
			yield return null;
			time += Time.deltaTime;
		}

		peckingPhase = true;
		time = 0;
		peckProgress = 0;
		
		while (peckingPhase) {
			peckProgress = time / peckDuration;
			if (peckProgress > 1)
			{
				peckingPhase = false;
				peckProgress = 1;
			}
			transform.RotateAround(transform.parent.transform.position, -transform.parent.transform.right, (peckAngle / peckDuration) * Time.deltaTime);
			yield return null;
			time += Time.deltaTime;
		}

		transform.localPosition = initialPosition;

		pecking = false;

		if (initProgress == ta.GetProgress ()) {
			ta.DiminishProgress();
			GetComponent<AudioSource>().Play();
		}
	}



}
