using UnityEngine;
using System.Collections;

public class volcan_trigger : MonoBehaviour {
	public GameObject cam;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player") {
			cam.SetActive(true);
		}
	}
}
