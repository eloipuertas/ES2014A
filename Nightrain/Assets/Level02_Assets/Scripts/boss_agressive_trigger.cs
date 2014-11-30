using UnityEngine;
using System.Collections;

public class boss_agressive_trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		GameObject.FindGameObjectWithTag ("Boss").GetComponent<Skeleton_boss_controller> ().setAgressive (true);
	}
}
