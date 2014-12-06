using UnityEngine;
using System.Collections;

public class firewall_Controller : MonoBehaviour {
	public int firewall_id = 1;

	private bool complete = false;
	private bool activated = false;
	private bool reactivated = false;

	private StageController stg_ctrl;
	private ParticleSystem particle;

	// Use this for initialization
	void Start () {
		stg_ctrl = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StageController> ();
		particle = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (complete && activated && !reactivated) {
			this.gameObject.SetActive (false);
			activated = false;
		} else if (!complete && !activated && !reactivated) {
			this.gameObject.SetActive (true);
		}
	}

	public void setCompleted (bool b) {
		complete = b;
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (complete && !reactivated) {
				activated = true;
				stg_ctrl.deactive_Stage (firewall_id-1);
			}
		}
	}

	void OnTriggerStay(Collider other) {
		if (other.gameObject.tag == "Player") {
			if (complete && !reactivated) {
				activated = true;
				stg_ctrl.deactive_Stage (firewall_id-1);
			}
		}
	}

	public void setBackBlockingWall () {
		reactivated = true;
	}
}
