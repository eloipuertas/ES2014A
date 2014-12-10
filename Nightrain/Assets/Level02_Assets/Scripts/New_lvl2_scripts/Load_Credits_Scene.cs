using UnityEngine;
using System.Collections;

public class Load_Credits_Scene : MonoBehaviour {
	private float timer;
	// Use this for initialization
	void Start () {
		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timer > 0.1f) {
			PlayerPrefs.SetInt ("Final_Credits", 1);
			Application.LoadLevel (8);
		}
	}
}
