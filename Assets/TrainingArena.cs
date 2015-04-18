using UnityEngine;
using System.Collections;



public class TrainingArena : MonoBehaviour {

	public GameObject ball;

	// Use this for initialization
	void Start () {
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void SpawnBall() {
		GameObject newBall;
		newBall =  (GameObject)Instantiate (ball, transform.position + new Vector3 ((Random.value - 0.5f) * 60f, 1f, (Random.value - 0.5f) * 60f), Quaternion.identity);
		Rigidbody rb =  newBall.GetComponent<Rigidbody>();
		Vector3 rot = new Vector3 (0f, Random.Range (-359, 359), 0f);
		newBall.GetComponent<Transform> ().Rotate (rot);
		//rb.velocity = newBall.GetComponent<Transform> ().TransformDirection (Vector3.forward * 10f);
	}
}
