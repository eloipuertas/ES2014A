using UnityEngine;
using System.Collections;

public class IceDemon_VideoAnimation : MonoBehaviour {
	public GameObject demon;

	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller skill_script;

	private StageController stage;

	private FireDemon_Controller demon_anim;
	private float time = 0.0f;
	private float anim_time = 0.0f;
	private bool first_anim = false;

	private Music_Engine_Script music;
	private bool shout = false;
	
	// Use this for initialization
	void Start () {
		stage = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StageController> ();
		stage.deactive_Stage (5);
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();

		player = GameObject.FindGameObjectWithTag ("Player");
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller> ();

		demon_anim = demon.GetComponent<FireDemon_Controller> ();

		move_script.enabled = false;
		skill_script.enabled = false;
		//demon_anim = GameObject.FindGameObjectWithTag("IceDemon6").GetComponent<FireDemon_Controller> ();
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!first_anim) {
			anim_time = Time.time - time;

			if (anim_time > 3.7f && anim_time < 6.4f) {
				if (!shout) {
					shout = true;
					music.play_demon_shout ();
				}
				demon_anim.rageAnim ();
			}
			if (anim_time >= 7.2f) {
				move_script.enabled = true;
				skill_script.enabled = true;
				demon_anim.set_notAnim ();
				first_anim = false;
				this.gameObject.SetActive (false);
			}
		}
	}
}
