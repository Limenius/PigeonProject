using UnityEngine;
using System.Collections;



public class TrainingArena : MonoBehaviour {

	public GameObject ball;

	private Color[] BallColors =  new Color[] { Color.blue, Color.red, Color.yellow };
	private string[] BallColorNames =  new string[] { "blue", "red", "yellow" };

	private string TargetColorName;

	// Use this for initialization
	void Start () {
		Debug.Log (ApplicationModel.currentLevel);
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
		SpawnBall ();
		TargetColorName = BallColorNames [Random.Range (0, BallColors.Length)];
		Debug.Log ("Target is " + TargetColorName);
	
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

		int idxColor = Random.Range (0, BallColors.Length);
		newBall.GetComponent<Peckable> ().setColor (BallColors [idxColor], BallColorNames [idxColor]);


		//rb.velocity = newBall.GetComponent<Transform> ().TransformDirection (Vector3.forward * 10f);
	}

	public bool isTarget(string name) {
		return this.TargetColorName == name;
	}

}