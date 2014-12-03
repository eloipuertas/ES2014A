using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_2 : MonoBehaviour {
	public GameObject boss;

	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller_lvl2 skill_script;

	private Skeleton_boss_controller boss_ctrl;
	private float time;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller_lvl2> ();

		boss_ctrl = boss.GetComponent <Skeleton_boss_controller> ();
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - time > 2.0f) {
			boss_ctrl.setAgressive (true);
			move_script.enabled = true;
			skill_script.enabled = true;
			this.gameObject.SetActive (false);
		}
	}
}
