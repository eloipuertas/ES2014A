using UnityEngine;
using System.Collections;

public class IceDemon_VideoAnimation : MonoBehaviour {
	public GameObject demon;
	private FireDemon_Controller demon_anim;
	private float time = 0.0f;
	private float anim_time = 0.0f;
	private bool first_anim = false;
	
	// Use this for initialization
	void Start () {
		demon_anim = demon.GetComponent <FireDemon_Controller> ();
		//demon_anim = GameObject.FindGameObjectWithTag("IceDemon6").GetComponent<FireDemon_Controller> ();
		time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (!first_anim) {
			anim_time = Time.time - time;

			if (anim_time > 3.7f && anim_time < 6.4f) demon_anim.rageAnim ();
				
			if (anim_time >= 7.2f) {
				demon_anim.set_notAnim ();
				first_anim = false;
				this.gameObject.SetActive (false);
			}
		}
	}
}
