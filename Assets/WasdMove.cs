using UnityEngine;
using System.Collections;

public class WasdMove : MonoBehaviour {

	private const int PIGEON_SPEED = 50;

	private Vector3 moveDirection = Vector3.zero;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		float speedPassed = Time.deltaTime;
		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))    transform.Translate(Vector3.forward *PIGEON_SPEED * speedPassed);
		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))  transform.Translate(Vector3.back * PIGEON_SPEED * speedPassed);
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))  transform.Translate(Vector3.left * PIGEON_SPEED * speedPassed);
		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) transform.Translate(Vector3.right * PIGEON_SPEED * speedPassed);


	
	}
}
