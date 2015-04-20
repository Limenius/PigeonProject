using UnityEngine;
using System.Collections;

public class MissileTrajectory : MonoBehaviour {

	private Vector3 dir;
	public bool success;

	public GameObject splash;
	public GameObject explossion;


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

	public void SetSuccess(bool success) {
		transform.FindChild ("Pigeon").GetComponent<PeckSim> ().wellTrained = success;
		this.success = success;
			

	}

	void OnTriggerEnter(Collider __collider) {
		WarSimulator ws = GameObject.FindWithTag("WarSimulator").GetComponent<WarSimulator>();
		if (!success) {
			GameObject.Instantiate (splash);
		} else {
			GameObject.Instantiate (explossion);
		}
		ws.NextPigeon();
		UnityEngine.Object.Destroy(gameObject);
		
	}
}
