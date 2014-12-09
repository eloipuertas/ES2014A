using UnityEngine;
using System.Collections;




public class rot : MonoBehaviour {

	GameObject go;
	
	Transform target;

	// Use this for initialization
	void Start () {
		go = GameObject.FindGameObjectWithTag("tag");
		target = go.transform;
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Rotate (0.0F, 1000.0F*Time.deltaTime, 0.0F);
		transform.RotateAround(transform.parent.position, new Vector3(0, 1, 0), 100.0F * Time.deltaTime);
	

	}
}
