using UnityEngine;
using System.Collections;

public class anim_finalStage_trigger : MonoBehaviour {
	public GameObject camera;
	private bool activated = false;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if(!activated) camera.SetActive (true);
		activated = true;
	}
}
