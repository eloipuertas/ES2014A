using UnityEngine;
using System.Collections;

public class rot : MonoBehaviour {

	public GameObject[] spheres;

	public float orbitDistance = 10.0f;
	public float orbitDegreesPerSec = 180.0f;

	// Use this for initialization
	void Start () {
		

	}

	void Orbit(){

		GameObject graveler = this.transform.parent.gameObject;
		Vector3 graveler_position = graveler.transform.position;

		foreach (Transform trans in this.transform) {

			trans.position = graveler_position + (trans.position - graveler_position).normalized * orbitDistance;

			trans.RotateAround(graveler_position, Vector3.up, orbitDegreesPerSec * Time.deltaTime);
			
		}

	}

	void Update () {

	}

	void LateUpdate () {
		
		Orbit();
		
	}

}
