using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_1 : MonoBehaviour {
	public GameObject camera2;

	private float time;

	// Use this for initialization
	void Start () {
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time > 2.5f) {
			camera2.SetActive (true);
			this.gameObject.SetActive (false);
		}
	}
}
