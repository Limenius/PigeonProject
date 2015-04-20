using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;

public class WarSimulator : MonoBehaviour {

	public GameObject HUD;
	public GameObject pigeonStatus;
	public GameObject missilePrefab;
	private GameObject pigeonStatusDisplay;
	public int currentPigeon = 0;

	// Use this for initialization
	void Start () {
		Pigeon p1 = new Pigeon ();
		p1.color = "blue";
		p1.trained = 99f;
		ApplicationModel.pigeons.Add (p1);
		Pigeon p2 = new Pigeon ();
		p2.color = "red";
		p2.trained = 99f;
		ApplicationModel.pigeons.Add (p2);
		Pigeon p3 = new Pigeon ();
		p3.color = "red";
		p3.trained = 99f;
		ApplicationModel.pigeons.Add (p3);
		Pigeon p4 = new Pigeon ();
		p4.color = "yellow";
		p4.trained = 1f;
		ApplicationModel.pigeons.Add (p4);
		Pigeon p5 = new Pigeon ();
		p5.color = "blue";
		p5.trained = 99f;
		ApplicationModel.pigeons.Add (p5);
		SpawnPigeon(currentPigeon);
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
		LaunchMissile (success);
	}

	public void NextPigeon() {
		currentPigeon ++;
		if (currentPigeon == ApplicationModel.pigeons.Count) {
			Application.LoadLevel ("recap");
		} else {
			SpawnPigeon (currentPigeon);
		}

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
