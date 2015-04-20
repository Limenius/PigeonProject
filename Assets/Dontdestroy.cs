using UnityEngine;
using System.Collections;

public class Dontdestroy : MonoBehaviour {


	private static Dontdestroy instance = null;
	public static Dontdestroy Instance {
		get { return instance; }
	}
	void Awake() {
		if (instance != null && instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}
}
