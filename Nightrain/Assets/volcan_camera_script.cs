using UnityEngine;
using System.Collections;

public class volcan_camera_script : MonoBehaviour {

	public GameObject engine;
	private Volcan_VideoAnimator anim;


	void Start() {
		anim = engine.GetComponent<Volcan_VideoAnimator> ();
	}

	void cameraStop() {
		anim.play_first_scene();
	}
}
