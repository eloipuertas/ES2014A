using UnityEngine;
using System.Collections;

public class Credits_Camera_Movement : MonoBehaviour {
	private float start_time;
	private float timer;
	private bool normal_credits = true;
	private bool press_esc = false;

	// Use this for initialization
	void Start () {
		start_time = Time.time;
		if (PlayerPrefs.GetInt ("Final_Credits") == 1) {
			PlayerPrefs.SetInt ("Final_Credits",0);
			normal_credits = false;
		} else {
			this.transform.position += new Vector3 (0.0f,0.0f,-6.1f);
			press_esc = true;
			normal_credits = true;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		timer = Time.time - start_time;
		if (timer > 16.0f && this.transform.position.z > -15.01f && !normal_credits) {
			this.transform.position += new Vector3(0.0f,0.0f,-0.012f);
		} 
		else if (timer > 2.0f && this.transform.position.z > -15.01f && normal_credits) {
			this.transform.position += new Vector3(0.0f,0.0f,-0.012f);
		}
		//print (this.transform.position);
		if (timer > 62.5f)
			this.gameObject.GetComponent <Credits_FadeOut> ().fade_out ();
		if (timer > 66.5f) Application.LoadLevel(1);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape) && press_esc) {
			Application.LoadLevel(1);
		}
	}

}
