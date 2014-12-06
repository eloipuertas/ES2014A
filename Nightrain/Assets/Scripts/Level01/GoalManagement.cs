using UnityEngine;
using System.Collections;

public class GoalManagement : MonoBehaviour {

	private bool success = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (success) {
			Application.LoadLevel(4);
		}
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag ("Player")) {
			success = true;
		}
	}
}
