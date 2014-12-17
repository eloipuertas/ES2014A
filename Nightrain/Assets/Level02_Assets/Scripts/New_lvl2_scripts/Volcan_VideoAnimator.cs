using UnityEngine;
using System.Collections;

public class Volcan_VideoAnimator : MonoBehaviour {
	public GameObject eff1;
	public GameObject eff2;
	public GameObject eff3;
	public GameObject eff4;
	public GameObject demon;

	private StageController stage;
	private FireDemon_Controller demon_anim;

	private GameObject player;
	private Music_Engine_Script music;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller skill_script;

	private float time = 0.0f;
	private float anim_time = 0.0f;
	private bool first_anim = false;

	private bool fireground = false;
	private bool fireexplosion = false;
	private bool shout = false;

	// Use this for initialization
	void Start () {
		stage = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StageController> ();
		stage.deactive_Stage (3);

		player = GameObject.FindGameObjectWithTag ("Player");
		music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();

		demon_anim = demon.GetComponent<FireDemon_Controller> ();
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller> ();
		
		move_script.enabled = false;
		skill_script.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (first_anim) {
			if(time == 0.0f) time = Time.time;
			anim_time = Time.time - time;

			eff1.SetActive (true);
			if (anim_time >= 1.0f) {
				eff2.SetActive(true);
				if(!fireground) {
					music.play_fireground ();
					fireground = true;
				}
			}
			if (anim_time >= 3.0f) eff3.SetActive (true);
			if (anim_time >= 4.5f) {
				eff4.SetActive (true);
				if(!fireexplosion) {
					music.play_fire_explosion ();
					fireexplosion = true;
				}
				demon.SetActive (true);
				if(!shout) {
					music.play_demon_shout ();
					shout = true;
				}
			}
			if (anim_time >= 4.5f && anim_time < 7.2f) {
				demon_anim.rageAnim ();
			}
			if (anim_time >= 7.2f) {
				move_script.enabled = true;
				skill_script.enabled = true;
				first_anim = false;
				Destroy (eff1);
				Destroy (eff3);
				Destroy (eff2, 2.0f);
				Destroy (eff4);
				demon_anim.set_notAnim();
				gameObject.SetActive(false);
			}
		}
	}

	public void camera_stop() {
		first_anim = true;
	}
}
