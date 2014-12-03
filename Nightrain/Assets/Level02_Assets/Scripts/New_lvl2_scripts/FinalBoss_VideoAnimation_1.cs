using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_1 : MonoBehaviour {
	public GameObject camera2;
	public GameObject position;

	private StageController stage;

	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller_lvl2 skill_script;

	private float time;

	// Use this for initialization
	void Start () {
		stage = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StageController> ();
		stage.deactive_Stage (8);

		player = GameObject.FindGameObjectWithTag ("Player");
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller_lvl2> ();

		move_script.teleport (position.transform.position);

		move_script.enabled = false;
		skill_script.enabled = false;
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time > 2.5f) {
			player.transform.position = position.transform.position;
			camera2.SetActive (true);
			this.gameObject.SetActive (false);
		}
	}
}
