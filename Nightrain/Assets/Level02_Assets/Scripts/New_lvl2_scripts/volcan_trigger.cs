using UnityEngine;
using System.Collections;

public class volcan_trigger : MonoBehaviour {
	public GameObject cam;
	private bool activated = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Player" && !activated) {
			cam.SetActive(true);
			activated = true;
		}
	}
}
