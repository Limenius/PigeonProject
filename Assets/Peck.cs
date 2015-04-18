using UnityEngine;
using System.Collections;

public class Peck : MonoBehaviour {

	public bool pecking = false;
	public float peckDuration = 0.5f;
	public float peckAngle = 45;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float speedPassed = Time.deltaTime;
	
		if (Input.GetKey(KeyCode.Q) && !pecking) StartCoroutine("PeckRoutine");
	
	}

	IEnumerator PeckRoutine() {
		pecking = true;
		bool peckingPhase = true;
		float time = 0;
		float peckProgress = 0;

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

		pecking = false;

	}

}
