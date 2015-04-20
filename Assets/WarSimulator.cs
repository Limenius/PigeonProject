using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;

public class WarSimulator : MonoBehaviour {

	public GameObject HUD;
	public GameObject pigeonStatus;
	public GameObject missilePrefab;
	public GameObject ship;
	public GameObject transition;
	private GameObject pigeonStatusDisplay;
	public int currentPigeon = 0;
	public float transitionTime = 0.5f;
	public float finalTransitionTime = 1.5f;


	// Use this for initialization
	void Start () {
//		Pigeon p1 = new Pigeon ();
//		p1.color = "blue";
//		p1.trained = 99f;
//		ApplicationModel.pigeons.Add (p1);
//		Pigeon p2 = new Pigeon ();
//		p2.color = "red";
//		p2.trained = 99f;
//		ApplicationModel.pigeons.Add (p2);
//		Pigeon p3 = new Pigeon ();
//		p3.color = "red";
//		p3.trained = 99f;
//		ApplicationModel.pigeons.Add (p3);
//		Pigeon p4 = new Pigeon ();
//		p4.color = "yellow";
//		p4.trained = 1f;
//		ApplicationModel.pigeons.Add (p4);
//		Pigeon p5 = new Pigeon ();
//		p5.color = "blue";
//		p5.trained = 99f;
//		ApplicationModel.pigeons.Add (p5);
		SpawnPigeon(currentPigeon);
		transition.SetActive (false);
	}
	
	private void SpawnPigeon(int num) {
		if (pigeonStatusDisplay) {
			GameObject.Destroy(pigeonStatusDisplay);
		}
		GameObject ps = (GameObject)GameObject.Instantiate (pigeonStatus);
		ps.transform.position = new Vector3 (50f, 530f, 0f);
		ps.transform.SetParent (HUD.transform);
		ps.transform.FindChild ("Text").GetComponent<Text> ().text = "Pigeon " + (num + 1);

		pigeonStatusDisplay = ps;

		Pigeon pigeon = ApplicationModel.pigeons [num];
		SetTrainingLevel(pigeon.trained);
		SetTrainingColor (pigeon.color);

		bool success = false;
		if (Random.value * 100f < pigeon.trained) {
			ApplicationModel.pigeons[num].succesful = true;
			success = true;
		} else {
			ApplicationModel.pigeons[num].succesful = false;
			success = false;
		}

		switch (pigeon.color) {
		case "blue":
			ship.GetComponent<Renderer>().material.color = Color.blue;
			break;
		case "red":
			ship.GetComponent<Renderer>().material.color = Color.red;
			break;
		case "yellow":
			ship.GetComponent<Renderer>().material.color = Color.yellow;
			break;
		}
		LaunchMissile (success);
	}

	public void NextPigeon() {
		currentPigeon ++;
		if (currentPigeon == ApplicationModel.pigeons.Count) {
			StartCoroutine("FinalTransitionRoutine");
		} else {
			StartCoroutine("TransitionRoutine");
		}
	}

	IEnumerator TransitionRoutine() {
		transition.SetActive (true);

		float elapsedTime = 0;
		
		while (elapsedTime < transitionTime) {
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		transition.SetActive (false);

		SpawnPigeon (currentPigeon);
		yield return null;
	}

	
	IEnumerator FinalTransitionRoutine() {
		transition.SetActive (true);
		
		float elapsedTime = 0;
		
		while (elapsedTime < finalTransitionTime) {
			yield return null;
			elapsedTime += Time.deltaTime;
		}
		transition.SetActive (false);

		Application.LoadLevel ("recap");

		yield return null;
	}

	void SetTrainingLevel(float trained) {
		pigeonStatusDisplay.transform.FindChild ("Progress").GetComponent<RectTransform> ().sizeDelta = new Vector2 ((trained * 75f) / 100f, 20f);
		pigeonStatusDisplay.transform.FindChild ("Accuracy").GetComponent<Text> ().text = "Accuracy: " + trained + "%";

	}

	void SetTrainingColor(string color) {
		switch (color) {
		case "blue":
			pigeonStatusDisplay.transform.FindChild ("Progress").GetComponent<Image> ().color = Color.blue;
			break;
		case "red":
			pigeonStatusDisplay.transform.FindChild ("Progress").GetComponent<Image> ().color = Color.red;
			break;
		case "yellow":
			pigeonStatusDisplay.transform.FindChild ("Progress").GetComponent<Image> ().color = Color.yellow;
			break;
		}
	}

	void LaunchMissile (bool success) {
		GameObject missile = (GameObject)GameObject.Instantiate (missilePrefab);
		missile.GetComponent<MissileTrajectory>().SetSuccess(success);
	}


	// Update is called once per frame
	void Update () {
	
	}

}
