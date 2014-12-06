﻿using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_2 : MonoBehaviour {
	public GameObject boss;

	private Skeleton_boss_controller boss_ctrl;
	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller_lvl2 skill_script;
	private ActionBarScript_lvl2 action_bar;
	
	private Texture2D [] dialogs = new Texture2D[3];
	private int current_dialog = 0;
	private const int reference_width = 650; 
	private const int reference_height = 300;
	private float timer;
	private float camera_timer;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller_lvl2> ();
		action_bar = GameObject.FindGameObjectWithTag ("ActionBar").GetComponent <ActionBarScript_lvl2> ();
		boss_ctrl = boss.GetComponent <Skeleton_boss_controller> ();

		dialogs[0] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_3");
		dialogs[1] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_4");
		dialogs[2] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_5");

		timer = Time.time + 1.5f;
		camera_timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timer > 2.0f && current_dialog >= 3) {
			boss_ctrl.setAgressive (true);
			move_script.enabled = true;
			skill_script.enabled = true;
			action_bar.enabled = true;
			this.gameObject.SetActive (false);
		}

		if ((Time.time - timer > 10.0f || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown  (KeyCode.Return)) && Time.time - camera_timer > 1.0f) {
			current_dialog += 1;
			timer = Time.time;
		}
	}

	void OnGUI () {
		if(Time.time - camera_timer > 1.0f && current_dialog < 3) {
			drawDialog (current_dialog);
		}
	}
	
	void drawDialog (int pos) {
		//if (Screen.height * 1.5f < Screen.width) height_rate = 0.5f; 
		Rect continue_box = new Rect (Screen.width/5.0f, 
		                              Screen.height - (Screen.height/3.6f), 
		                              //this.dialog1.width / 1.0f, 
		                              //this.dialog1.height / 1.0f);
		                              this.resizeTextureWidth(this.dialogs[pos]) / 2.75f, 
		                              this.resizeTextureHeight(this.dialogs[pos]) / 1.5f);
		Graphics.DrawTexture (continue_box, this.dialogs[pos]);
	}
	
	
	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		//return ((Screen.height * texture.height) / (reference_height * 2.0f));
		float factor = Screen.width / Screen.height;
		//if(Screen.width < Screen.height) factor = Screen.height / Screen.width;
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
}
