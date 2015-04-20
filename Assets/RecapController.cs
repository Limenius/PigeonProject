using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;

public class RecapController : MonoBehaviour {

	public GameObject mFailed;
	public GameObject mSuccess;
	public GameObject canvas;

	public GameObject sBluePrefab;
	public GameObject sRedPrefab;
	public GameObject sYellowPrefab;

	public GameObject stampPrefab;

	public GameObject targetMissed;
	public GameObject targetEliminated;

	public GameObject playagainButton;

	private float startTime;
	public float resultTime = 0.5f;
	public float stepTime = 0.5f;

	private int step = 0;
	
	public int missionsAccomplished = 0;
	public bool finished = false;

	public List<Pigeon> pigeons = new List<Pigeon>();
	public List<string> objectiveList = new List<string>();

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		mFailed.SetActive (false);
		mSuccess.SetActive (false);
		playagainButton.SetActive(false);

		pigeons = ApplicationModel.pigeons;
		objectiveList = ApplicationModel.objectiveList;

//		Pigeon p1 = new Pigeon ();
//		p1.color = "blue";
//		p1.succesful = true;
//		pigeons.Add (p1);
//
//		Pigeon p2 = new Pigeon ();
//		p2.color = "blue";
//		p2.succesful = false;
//		pigeons.Add (p2);
//
//		Pigeon p3 = new Pigeon ();
//		p3.color = "yellow";
//		p3.succesful = true;
//		pigeons.Add (p3);
//
//		objectiveList.Add ("red");
//		objectiveList.Add ("red");
//		objectiveList.Add ("blue");
//		objectiveList.Add ("blue");
//		objectiveList.Add ("yellow");
	}
	
	// Update is called once per frame
	void Update () {
		if (finished) {
			return;
		}

		float elapsedTime = Time.time - startTime;
		if (elapsedTime > step * stepTime) {
			string color = objectiveList[step];
			bool found = false;
			int i = 0;
			for (i = 0; i < pigeons.Count; i ++) {
				if (pigeons[i].color == color && pigeons[i].succesful) {
					found = true;
					missionsAccomplished ++;
					break;
				}
			}
			if (found) {
				pigeons.RemoveAt (i);
			}

			GameObject ship = null;
			switch (objectiveList[step]) {
			case "blue":
				ship = (GameObject)GameObject.Instantiate(sBluePrefab);
				break;
			case "red":
				ship = (GameObject)GameObject.Instantiate(sRedPrefab);
				break;
			case "yellow":
				ship = (GameObject)GameObject.Instantiate(sYellowPrefab);
				break;
			}

			ship.transform.SetParent(canvas.transform);
			ship.GetComponent<RectTransform> ().position = new Vector3(330f, 420f - (step * 60f), 0f);
			ship.GetComponent<RectTransform> ().sizeDelta = new Vector2(135f, 30f);

			StartCoroutine(CheckRoutine(found, step));
			step++;
			if (step == objectiveList.Count) {
				StartCoroutine(StampRoutine(missionsAccomplished == 5));
				finished = true;
			}
		}
	}

	public void PlayAgain() {
		ApplicationModel.pigeons = new List<Pigeon> ();
		ApplicationModel.objectiveList = new List<string> ();
		GameObject.Destroy(GameObject.FindWithTag("DestroyRestart"));

		Application.LoadLevel("trainingarena");

	}

	IEnumerator CheckRoutine(bool result, int step) {

		float elapsedTime = 0;

		while (elapsedTime < stepTime/2) {
			yield return null;
			elapsedTime += Time.deltaTime;
		}

		GameObject tick = null;
		if (result) {
			tick = (GameObject)GameObject.Instantiate(targetEliminated);
		} else {
			tick = (GameObject)GameObject.Instantiate(targetMissed);
		}

		tick.transform.SetParent (canvas.transform);
		tick.GetComponent<RectTransform> ().position = new Vector3((result ? 30f : 0f) + 550f, 415f - (step * 60f), 0f);

	}

	IEnumerator StampRoutine(bool result) {
		
		float elapsedTime = 0;
		
		while (elapsedTime < resultTime) {
			yield return null;
			elapsedTime += Time.deltaTime;
		}

		GameObject.Instantiate (stampPrefab);

		mSuccess.SetActive (result);
		mFailed.SetActive (!result);
		playagainButton.SetActive(true);


	}
}
