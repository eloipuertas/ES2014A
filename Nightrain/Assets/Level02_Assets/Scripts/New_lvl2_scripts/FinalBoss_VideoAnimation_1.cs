using UnityEngine;
using System.Collections;

public class FinalBoss_VideoAnimation_1 : MonoBehaviour {
	public GameObject camera2;
	public GameObject position;

	private StageController stage;

	private GameObject boss;
	private GameObject player;
	private ClickToMove_lvl2 move_script;
	private Skill_Controller skill_script;
	private ActionBarScript action_bar;
	
	private Texture2D [] dialogs = new Texture2D[2];
	private int current_dialog = 0;
	private const int reference_width = 650; 
	private const int reference_height = 300;
	private float timer;
	private float camera_timer;

	// Use this for initialization
	void Start () {
		stage = GameObject.FindGameObjectWithTag ("GameController").GetComponent<StageController> ();
		stage.deactive_Stage (8);

		boss = GameObject.FindGameObjectWithTag ("Boss");
		player = GameObject.FindGameObjectWithTag ("Player");
		move_script = player.GetComponent <ClickToMove_lvl2> ();
		skill_script = player.GetComponent <Skill_Controller> ();
		action_bar = GameObject.FindGameObjectWithTag ("ActionBar").GetComponent <ActionBarScript> ();

		move_script.teleport (position.transform.position);
		player.transform.position = position.transform.position;
		move_script.rotateToPos (boss.transform.position);


		move_script.enabled = false;
		skill_script.enabled = false;
		action_bar.enabled = false;
		
		dialogs[0] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_1");
		dialogs[1] = Resources.Load<Texture2D>("Lvl2/Dialogs/boss_dialog_2_"+PlayerPrefs.GetString ("Player"));
		timer = Time.time + 3.5f;
		camera_timer = Time.time;

	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - timer > 1.0f && current_dialog >= 2) {
			camera2.SetActive (true);
			this.gameObject.SetActive (false);
		}

		if ((Time.time - timer > 10.0f || Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown  (KeyCode.Return)) && Time.time - camera_timer > 3.5f) {
			current_dialog += 1;
			timer = Time.time;
		}
	}

	void OnGUI () {
		if(Time.time - camera_timer > 3.5f && current_dialog < 2) {
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
