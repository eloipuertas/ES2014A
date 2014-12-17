using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_3 : MonoBehaviour {
	public GameObject boss;
	public GameObject playerPos;
	public GameObject fade;
	
	private Skeleton_boss_controller boss_ctrl;
	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller skill_script;
	private ActionBarScript action_bar;
	private FadeOut_lvl2 fade_out;

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
	private float atk_anim = 0.0f;

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;
	
	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller> ();
		action_bar = GameObject.FindGameObjectWithTag ("ActionBar").GetComponent <ActionBarScript> ();
		boss_ctrl = boss.GetComponent <Skeleton_boss_controller> ();
		fade_out = fade.GetComponent<FadeOut_lvl2> ();

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData();
		this.load = this.mc.loadData();

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
		dialogs[1] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_7_"+PlayerPrefs.GetString ("Player"));
		
		timer = Time.time + 1.5f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timer > 2.0f && killed) {
			fade_out.Fading(1);
		}
		if (Time.time - timer > 5.0f && killed) {
			this.save.saveTimePlayed(GameEngineLevel02_new.getTimePlay());
			PlayerPrefs.SetInt ("Final_Credits", 1);
			Application.LoadLevel (8);
			gameObject.SetActive (false);
		}
		if (current_dialog == 1) {
			if (firepunch_actual == null) {
				firepunch_actual = Instantiate (firepunch, player_hand.transform.position, firepunch.transform.rotation) as GameObject;
				firepunch_actual.transform.parent = player_hand.transform;
			}
		} else if (current_dialog == 2) {
			if(!killed) atk_anim = Time.time;
			Vector3 firePos = player.transform.position;
			firePos.x += 2;
			firePos.y += 6;
			if (finalFireball_actual == null) {
				finalFireball_actual = Instantiate (finalFireball, firePos, finalFireball.transform.rotation) as GameObject;
				killed = true;
			}
			if(Time.time - atk_anim < 0.4f) move_script.attackAnim ();
			else move_script.stopAttackAnim();
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
		                              Screen.height - (Screen.height/2.8f), 
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
