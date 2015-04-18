using UnityEngine;
using System.Collections;

public class Peckable : MonoBehaviour {

	private bool isAlive = true;

	private Color color;
	private string colorName;

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
			TrainingArena ta = GameObject.FindWithTag("TrainingArena").GetComponent<TrainingArena>();
			if (ta.isTarget(this.colorName)) {
				Debug.Log ("GOOD");
			}
			//__collider.gameObject.GetComponent<Bullet>().Die();
			GetComponent<AudioSource>().Play ();
			isAlive = false;
			//Destroy ();
			
		}
		
	}

	public void setColor(Color color, string colorName) {
		this.color = color;
		this.colorName = colorName;
		this.GetComponent<Renderer>().material.color = color;

	}
}
