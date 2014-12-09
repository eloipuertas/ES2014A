using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_3 : MonoBehaviour {
	public GameObject boss;
	public GameObject playerPos;
	public GameObject end_camera;

	private Skeleton_boss_controller boss_ctrl;
	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller_lvl2 skill_script;
	private ActionBarScript_lvl2 action_bar;

	private GameObject player_hand;
	private GameObject firepunch;
	private GameObject finalFireball;

	private GameObject firepunch_actual = null;
	private GameObject finalFireball_actual = null;
	
	private Texture2D [] dialogs = new Texture2D[2];
	private int current_dialog = 0;
	private const int reference_width = 650; 
	private const int reference_height = 300;
	private float timer;
	private float camera_timer;

	private bool killed = false;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller_lvl2> ();
		action_bar = GameObject.FindGameObjectWithTag ("ActionBar").GetComponent <ActionBarScript_lvl2> ();
		boss_ctrl = boss.GetComponent <Skeleton_boss_controller> ();

		boss_ctrl.teleportToRespawn ();
		boss_ctrl.rotateToPlayer (playerPos.transform.position);

		move_script.teleport (playerPos.transform.position);
		player.transform.position = playerPos.transform.position;
		move_script.rotateToPos (boss.transform.position);

		move_script.enabled = false;
		skill_script.enabled = false;
		action_bar.enabled = false;

		player_hand = GameObject.FindGameObjectWithTag ("PlayerHand");
		firepunch = Resources.Load <GameObject> ("Lvl2/prefabs/Fire_punch");
		finalFireball = Resources.Load <GameObject> ("Lvl2/prefabs/Final_Fireball");
		
		dialogs[0] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_6");
		dialogs[1] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_7");
		
		timer = Time.time + 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timer > 2.0f && killed) {
			end_camera.SetActive (true);
			gameObject.SetActive (false);
		}

		if (current_dialog == 1) {
			if (firepunch_actual == null) {
				firepunch_actual = Instantiate (firepunch, player_hand.transform.position, firepunch.transform.rotation) as GameObject;
			}
		} else if (current_dialog == 2) {
			Vector3 firePos = player.transform.position;
			firePos.x += 6;
			if (finalFireball_actual == null) {
				move_script.attackAnim ();
				finalFireball_actual = Instantiate (finalFireball, firePos, finalFireball.transform.rotation) as GameObject;
				killed = true;
			}
		}

		if (Time.time - timer > 10.0f) {
			current_dialog += 1;
			timer = Time.time;
		}

		if ((Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown  (KeyCode.Return)) && Time.time - timer > 4.0f) {
			current_dialog += 1;
			timer = Time.time;
		}
	}
	
	void OnGUI () {
		if(current_dialog < 2) {
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
