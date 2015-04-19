using UnityEngine;
using System.Collections;

public class MissileTrajectory : MonoBehaviour {

	private Vector3 dir;

	// Use this for initialization
	void Start () {


		GameObject target = GameObject.FindWithTag("WarShip");
		dir = target.transform.position - transform.position;
		transform.forward = dir;
		Rigidbody rb = this.GetComponent<Rigidbody> ();
		rb.velocity = this.transform.TransformDirection (Vector3.forward * 10f);
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(transform.position, transform.up, (Random.value - 0.5f) * 1f);
		transform.RotateAround(transform.position, transform.right, (Random.value - 0.5f) * 1f);
		Rigidbody rb = this.GetComponent<Rigidbody> ();

		rb.velocity = this.transform.TransformDirection (Vector3.forward * 10f);

	}
}
