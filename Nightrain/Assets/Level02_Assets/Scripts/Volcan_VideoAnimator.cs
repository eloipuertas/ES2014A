using UnityEngine;
using System.Collections;

public class Volcan_VideoAnimator : MonoBehaviour {

	public GameObject eff1;
	public GameObject eff2;
	public GameObject eff3;
	public GameObject eff4;
	public GameObject demon;
	public GameObject cam;
	private FireDemon_Controller demon_anim;

	private float time = 0.0f;
	private float anim_time = 0.0f;
	private bool first_anim = false;

	// Use this for initialization
	void Start () {
		demon_anim = demon.GetComponent<FireDemon_Controller> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (first_anim) {
			if(time == 0.0f) time = Time.time;
			anim_time = Time.time - time;

			eff1.SetActive (true);
			if (anim_time >= 1.0f) eff2.SetActive(true);
			if (anim_time >= 3.0f) eff3.SetActive (true);
			if (anim_time >= 4.5f) {
				eff4.SetActive (true);
				demon.SetActive (true);
			}
			if (anim_time >= 4.5f && anim_time < 7.2f) {
				demon_anim.rageAnim ();
			}
			if (anim_time >= 7.2f) {
				first_anim = false;
				Destroy (eff1);
				Destroy (eff3);
				Destroy (eff2, 2.0f);
				Destroy (eff4);
				cam.SetActive(false);
				demon_anim.set_notAnim();
			}
		}
	}

	public void play_first_scene() {
		first_anim = true;		
	}
}
