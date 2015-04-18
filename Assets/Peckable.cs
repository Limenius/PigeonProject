using UnityEngine;
using System.Collections;

public class Peckable : MonoBehaviour {

	private bool isAlive = true;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (!isAlive && !GetComponent<AudioSource> ().isPlaying) {
			Destroy ();
		}
	
	}

	private void Destroy() {

		UnityEngine.Object.Destroy(gameObject);
	}

	void OnTriggerEnter(Collider __collider) {
		if (__collider.gameObject.tag == "Head") {
			//__collider.gameObject.GetComponent<Bullet>().Die();
			GetComponent<AudioSource>().Play ();
			isAlive = false;
			//Destroy ();
			
		}
		
	}
}
