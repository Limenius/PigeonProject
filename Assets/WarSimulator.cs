using UnityEngine;
using System.Collections;

using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;

public class WarSimulator : MonoBehaviour {

	public GameObject HUD;
	public GameObject pigeonStatus;
	public GameObject pigeonStatusDisplay;

	// Use this for initialization
	void Start () {
		Pigeon p1 = new Pigeon ();
		p1.color = "blue";
		p1.trained = 80f;
		ApplicationModel.pigeons.Add (p1);
		SpawnPigeon(0);
	}
	
	private void SpawnPigeon(int num) {
		GameObject ps = (GameObject)GameObject.Instantiate (pigeonStatus);
		ps.transform.position = new Vector3 (50f, 530f, 0f);
		ps.transform.SetParent (HUD.transform);
		ps.transform.FindChild ("Text").GetComponent<Text> ().text = "Pigeon " + (num + 1);

		pigeonStatusDisplay = ps;

		Debug.Log (ApplicationModel.pigeons.Count);
		Pigeon pigeon = ApplicationModel.pigeons [num];
		SetTrainingLevel(pigeon.trained);
		SetTrainingColor (pigeon.color);
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

	// Update is called once per frame
	void Update () {
	
	}
}
