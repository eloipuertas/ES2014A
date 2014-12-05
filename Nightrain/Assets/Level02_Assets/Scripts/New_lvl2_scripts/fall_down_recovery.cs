using UnityEngine;
using System.Collections;

public class fall_down_recovery : MonoBehaviour {
	public GameObject teleport_point;

	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller_lvl2 skill_script;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter(Collider other) {
		if(other.tag == "Player") {
			player = GameObject.FindGameObjectWithTag ("Player");
			move_script = player.GetComponent <ClickToMove_lvl2> ();
			skill_script = player.GetComponent <Skill_Controller_lvl2> ();
		
			move_script.teleport (teleport_point.transform.position);

			move_script.enabled = false;
			skill_script.enabled = false;

			player.transform.position = teleport_point.transform.position;

			move_script.enabled = true;
			skill_script.enabled = true;
		}
	}

	void OnTriggerStay(Collider other) {
		if(other.tag == "Player") {
			player = GameObject.FindGameObjectWithTag ("Player");
			move_script = player.GetComponent <ClickToMove_lvl2> ();
			skill_script = player.GetComponent <Skill_Controller_lvl2> ();
			
			move_script.teleport (teleport_point.transform.position);
			
			move_script.enabled = false;
			skill_script.enabled = false;
			
			player.transform.position = teleport_point.transform.position;
			
			move_script.enabled = true;
			skill_script.enabled = true;
		}
	}
}
