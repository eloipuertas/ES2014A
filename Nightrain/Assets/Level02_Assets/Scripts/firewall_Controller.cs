using UnityEngine;
using System.Collections;

public class firewall_Controller : MonoBehaviour {
	private bool complete = false;
	private bool activated = false;

	private ParticleSystem particle;

	// Use this for initialization
	void Start () {
		particle = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (complete && activated) {
			this.gameObject.SetActive (false);
			activated = false;
		} else if (!complete && !activated) {
			this.gameObject.SetActive (true);
		}
	}

	public void setCompleted (bool b) {
		complete = b;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (complete) {
				activated = true;
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (complete) {
				activated = true;
			}
		}
	}
}
