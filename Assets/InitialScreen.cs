using UnityEngine;
using System.Collections;
using Image = UnityEngine.UI.Image;
using Text = UnityEngine.UI.Text;


public class InitialScreen : MonoBehaviour {

	public GameObject imageObject1;
	public GameObject imageObject2;
	public GameObject imageObject3;
	public GameObject imageObject4;

	public GameObject buttontext;

	private int step = 0;

	void Start() {
		imageObject1.SetActive (true);
		imageObject2.SetActive (true);
		imageObject3.SetActive (true);
		imageObject4.SetActive (true);


		//imageObject3.GetComponent<CanvasRenderer>().enabled = false;
		//imageObject4.GetComponent<CanvasRenderer>().enabled = false;
		
	}

	public void Next() {
		switch (this.step) {
		case 0:
			imageObject1.SetActive (false);
			this.step ++;
			break;
		case 1:
			imageObject2.SetActive (false);
			this.step ++;
			break;
		case 2:
			imageObject3.SetActive (false);
			this.step ++;
			buttontext.GetComponent<Text>().text = "Start Game"; 
			break;
		case 3:
			StartGame();
			break;

		}
	}

	public void StartGame() {
		ApplicationModel.currentLevel = 1;
		Application.LoadLevel("trainingarena");
	}
}
