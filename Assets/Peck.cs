using UnityEngine;
using System.Collections;

public class Peck : MonoBehaviour {

	private bool pecking = false;
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
		float time = 0;
		float peckProgress = 0;

		while (pecking) {
			peckProgress = time / peckDuration;

			if (peckProgress > 1)
			{
				pecking = false;
				peckProgress = 1;
			}

			transform.RotateAround(transform.parent.transform.position, Vector3.right, (peckAngle / peckDuration) * Time.deltaTime);


			yield return null;

			time += Time.deltaTime;
		}

		pecking = true;
		time = 0;
		peckProgress = 0;
		
		while (pecking) {
			peckProgress = time / peckDuration;
			
			if (peckProgress > 1)
			{
				pecking = false;
				peckProgress = 1;
			}
			
			transform.RotateAround(transform.parent.transform.position, Vector3.left, (peckAngle / peckDuration) * Time.deltaTime);
			
			
			yield return null;
			
			time += Time.deltaTime;
		}

	}

}
