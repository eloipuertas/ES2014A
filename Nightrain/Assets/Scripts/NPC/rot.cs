using UnityEngine;
using System.Collections;

public class rot : MonoBehaviour {
	
	public GameObject[] spheres;
	
	public float orbitDistance = 10.0f;
	public float orbitDegreesPerSec = 180.0f;
	
	GameObject graveler;
	Vector3 graveler_position;
	
	// Use this for initialization
	void Start () {
		graveler = this.transform.parent.gameObject;
	}
	
	void LateUpdate () {
		graveler_position = graveler.transform.position;
		foreach (Transform trans in this.transform) {
			trans.position = graveler_position + (trans.position - graveler_position).normalized * orbitDistance;
			trans.RotateAround(graveler_position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);	
		}
	}
}