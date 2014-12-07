using UnityEngine;
using System.Collections;

public class target_selection : MonoBehaviour {
	public GameObject selection;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnMouseOver() {
		selection.SetActive (true);
	}

	void OnMouseExit() {
		selection.SetActive (false);
	}
}
