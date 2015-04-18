using UnityEngine;
using System.Collections;

public class InitialScreen : MonoBehaviour {

	public void StartGame() {
		ApplicationModel.currentLevel = 1;
		Application.LoadLevel("trainingarena");
	}
}
