using UnityEngine;
using System.Collections;

public class PeckSim : MonoBehaviour {

	private bool pecking = false;
	public float peckDuration = 0.05f;

	// Use this for initialization
	void Start () {



	}
	
	// Update is called once per frame
	void Update () {
		if (!pecking) {
			StartCoroutine("PeckRoutine");
		}
	}

	IEnumerator PeckRoutine() {
		GameObject targetShip = GameObject.FindWithTag("WarShip");

		Vector3 dir = targetShip.transform.position - transform.position;

		Vector3 targetPoint = transform.localPosition + dir.normalized * 0.1f;

	
		transform.forward = dir;
		pecking = true;
		bool peckingPhase = true;
		float time = 0;
		float peckProgress = 0;

		Vector3 startPoint = transform.localPosition;

		Vector3 initialPosition = transform.localPosition;
		
		while (peckingPhase) {
			peckProgress = time / peckDuration;
			if (peckProgress > 1)
			{
				peckingPhase = false;
				peckProgress = 1;
			}
			Vector3 currentPos = Vector3.Lerp(startPoint, targetPoint, peckProgress);
			transform.localPosition = currentPos;

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
			Vector3 currentPos = Vector3.Lerp(targetPoint, startPoint, peckProgress);
			yield return null;
			time += Time.deltaTime;
		}
		
		transform.localPosition = initialPosition;
		
		pecking = false;
		
	}
}
