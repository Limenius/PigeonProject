﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;


using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;

public class TrainingArena : MonoBehaviour {

	public GameObject ball;

	public Quaternion initialRotation;

	public GameObject initialTimer;
	public GameObject missionTimer;
	public GameObject initialObjectives;
	public GameObject objectives;
	public GameObject pigeonStatusHolder;
	public GameObject pigeonStatus;
	public GameObject keysHolder;

	private Color[] BallColors =  new Color[] { Color.blue, Color.red, Color.yellow };
	private string[] BallColorNames =  new string[] { "blue", "red", "yellow" };

	private string TargetColorName;

	private float startTime;

	public float summaryTime;
	public float missionTime;

	public GameObject blueship;
	public GameObject redship;
	public GameObject yellowship;

	public bool playing = false;

	private List<string> objectiveList = new List<string> ();

	private List<Pigeon> pigeons = new List<Pigeon> ();
	private Pigeon currentPigeon = new Pigeon();
	private List<GameObject> pigeonsHuds = new List<GameObject> ();

	// Use this for initialization
	void Start () {
		SpawnBalls (10);

		TargetColorName = BallColorNames [Random.Range (0, BallColors.Length)];
		objectives.SetActive (false);
		pigeonStatusHolder.SetActive (false);
		keysHolder.SetActive (false);
		missionTimer.SetActive (false);
		ChooseObjectives ();
		SpawnObjectivesSummary ();
		SpawnObjectives ();
		SpawnPigeons ();

		startTime = Time.time;
		initialRotation = GameObject.FindGameObjectWithTag ("Pigeon").transform.rotation;


	}
	
	// Update is called once per frame
	void Update () {
		float elapsedTime = Time.time - startTime;
		if (elapsedTime < summaryTime) {
			initialTimer.GetComponent<Text> ().text = (summaryTime - elapsedTime).ToString ("n0");
		} else {
			playing = true;
			initialObjectives.SetActive(false);
			objectives.SetActive (true);
			pigeonStatusHolder.SetActive(true);
			keysHolder.SetActive(true);
			missionTimer.SetActive (true);
			missionTimer.GetComponent<Text> ().text = (missionTime - (elapsedTime - summaryTime)).ToString ("n0");
			if (Input.GetKeyUp(KeyCode.N) ) NextPigeon();
			if (Input.GetKeyUp (KeyCode.R)) Application.LoadLevel("trainingarena");
			if (Input.GetKeyUp (KeyCode.F)) FinishTraining();
		}

		if (missionTime - (elapsedTime - summaryTime) <= 0f) {
			FinishTraining ();
		}
	}

	private void FinishTraining() {
		StorePigeon ();
		ApplicationModel.pigeons = pigeons;
		Cursor.lockState = UnityEngine.CursorLockMode.None;
		Cursor.visible = true;
		Application.LoadLevel("Intermission");

	}
	
	private void ChooseObjectives() {
		for (int i = 0; i < 5; i ++) {
			int idxColor = Random.Range (0, BallColors.Length);
			this.objectiveList.Add(BallColorNames[idxColor]);
		}
		this.objectiveList.Sort();
		ApplicationModel.objectiveList = objectiveList;
	}

	private void SpawnObjectivesSummary () {
		int idx = 0;
		GameObject ship = (GameObject)GameObject.Instantiate (blueship);

		foreach (string color in objectiveList) {
			Debug.Log (color);
			switch (color) {
			case "blue":
				ship = (GameObject)GameObject.Instantiate (blueship);
				break;
			case "red":
				ship = (GameObject)GameObject.Instantiate (redship);
				break;
			case "yellow":
				ship = (GameObject)GameObject.Instantiate (yellowship);
				break;
			}
			ship.transform.SetParent(initialObjectives.transform);
			ship.GetComponent<RectTransform>().position = new Vector3(230f + (idx * 100), 385f);
			idx ++;
		}

	}

	private void SpawnObjectives () {
		int idx = 0;
		GameObject ship = (GameObject)GameObject.Instantiate (blueship);
		
		foreach (string color in objectiveList) {
			Debug.Log (color);
			switch (color) {
			case "blue":
				ship = (GameObject)GameObject.Instantiate (blueship);
				break;
			case "red":
				ship = (GameObject)GameObject.Instantiate (redship);
				break;
			case "yellow":
				ship = (GameObject)GameObject.Instantiate (yellowship);
				break;
			}
			ship.transform.SetParent(objectives.transform);
			ship.GetComponent<RectTransform>().position = new Vector3(285f + (idx * 100), 581f);
			idx ++;
		}
		
	}

	private void SpawnPigeons() {
		SpawnPigeon (pigeons.Count);
	}

	private void SpawnPigeon(int num) {
		GameObject ps = (GameObject)GameObject.Instantiate (pigeonStatus);
		ps.transform.position = new Vector3 ((num * 100f) + 50f, 530f, 0f);
		ps.transform.SetParent (pigeonStatusHolder.transform);
		ps.transform.FindChild ("Text").GetComponent<Text> ().text = "Pigeon " + (num + 1);
		pigeonsHuds.Add (ps);
		SetTrainingLevel(0f);
		SetTrainingColor ("blue");
	}

	private void NextPigeon() {
		if (pigeons.Count < 4) {
			StorePigeon();

			SpawnPigeon (pigeons.Count);
			SpawnBalls (5);

			GameObject.FindGameObjectWithTag("Pigeon").transform.position = new Vector3(0f, 0f, 0f);
			GameObject.FindGameObjectWithTag("Pigeon").transform.rotation = initialRotation;
		}
	}

	private void StorePigeon() {
		Pigeon pigeon = new Pigeon ();
		string progressColor = "";
		Color currentColor = pigeonsHuds [pigeonsHuds.Count - 1].transform.FindChild ("Progress").GetComponent<Image> ().color;
		if (currentColor == Color.blue)
			progressColor = "blue";
		if (currentColor == Color.red)
			progressColor = "red";
		if (currentColor == Color.yellow)
			progressColor = "yellow";
		pigeon.color = progressColor;
		pigeon.trained = GetProgress ();
		pigeons.Add (pigeon);
	}
	
	private void SetTrainingLevel(float progress) {
		pigeonsHuds [pigeonsHuds.Count - 1].transform.FindChild ("Progress").GetComponent<RectTransform> ().sizeDelta = new Vector2 ((progress * 75f) / 100f, 20f);
	}

	private void SetTrainingColor(string color) {
		switch (color) {
		case "blue":
			pigeonsHuds [pigeonsHuds.Count - 1].transform.FindChild ("Progress").GetComponent<Image> ().color = Color.blue;
			break;
		case "red":
			pigeonsHuds [pigeonsHuds.Count - 1].transform.FindChild ("Progress").GetComponent<Image> ().color = Color.red;
			break;
		case "yellow":
			pigeonsHuds [pigeonsHuds.Count - 1].transform.FindChild ("Progress").GetComponent<Image> ().color = Color.yellow;
			break;
		}
	}

	private void SpawnBalls(int num) {
		for (int i = 0; i< num; i++) {
			SpawnBall ();
		}
	}

	private void SpawnBall() {
		GameObject newBall;
		newBall =  (GameObject)Instantiate (ball, transform.position + new Vector3 ((Random.value - 0.5f) * 60f, 1f, (Random.value - 0.5f) * 60f), Quaternion.identity);
		Rigidbody rb =  newBall.GetComponent<Rigidbody>();
		Vector3 rot = new Vector3 (0f, Random.Range (-359, 359), 0f);
		newBall.GetComponent<Transform> ().Rotate (rot);

		int idxColor = Random.Range (0, BallColors.Length);
		newBall.GetComponent<Peckable> ().setColor (BallColors [idxColor], BallColorNames [idxColor]);


		rb.velocity = newBall.GetComponent<Transform> ().TransformDirection (Vector3.forward * 10f);
	}

	public bool peckedOn(string color) {
		bool learning = false;
		float progress = GetProgress();
		string progressColor = "";
		Color currentColor = pigeonsHuds [pigeonsHuds.Count - 1].transform.FindChild ("Progress").GetComponent<Image> ().color;
		if (currentColor == Color.blue) progressColor = "blue";
		if (currentColor == Color.red) progressColor = "red";
		if (currentColor == Color.yellow) progressColor = "yellow";
			
		Debug.Log (progressColor);
		if (progress <= 0f || progressColor == color) {
			IncrementProgress();
			learning = true;
			SetTrainingColor (color);
		} else {
			DiminishProgress();
			GetComponent<AudioSource>().Play();
		}
		return learning;
	}

	public float GetProgress() {
		return pigeonsHuds [pigeonsHuds.Count - 1].transform.FindChild ("Progress").GetComponent<RectTransform> ().sizeDelta.x * 100f / 75f;
	}

	public void IncrementProgress() {
		float progress = GetProgress ();
		float delta = 100f - progress;
		SetTrainingLevel (progress + delta / 2);
	}

	public void DiminishProgress() {
		float progress = GetProgress ();
		float delta = 100f - progress;
		if (progress - delta < 0f) {
			SetTrainingLevel (0f);
		} else {
			SetTrainingLevel (progress - delta);
		}

	}

	public bool isTarget(string name) {
		return this.TargetColorName == name;
	}

}
