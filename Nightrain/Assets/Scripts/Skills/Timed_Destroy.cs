using UnityEngine;
using System.Collections;

public class Timed_Destroy : MonoBehaviour {
	public float destroyTime = 1.0f;
	private float initTime;
	private float actualTime; 

	// Use this for initialization
	void Start () {
		initTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		actualTime = Time.time;
		if (actualTime - initTime > destroyTime) {
			Destroy (gameObject);
		}
	}
}
