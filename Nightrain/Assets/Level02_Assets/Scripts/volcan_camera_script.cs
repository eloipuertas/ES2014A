using UnityEngine;
using System.Collections;

public class volcan_camera_script : MonoBehaviour {
	private Volcan_VideoAnimator anim;


	void Start() {
		anim = gameObject.GetComponent<Volcan_VideoAnimator> ();
	}

	void cameraStop() {
		anim.camera_stop ();
	}

}
