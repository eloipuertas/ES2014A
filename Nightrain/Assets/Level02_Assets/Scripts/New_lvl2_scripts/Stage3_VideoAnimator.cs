using UnityEngine;
using System.Collections;

public class Stage3_VideoAnimator : MonoBehaviour {
	public GameObject firetrap;

	private GameObject player;
	private ClickToMove_lvl2 player_move;
	private Skill_Controller skill_ctrl;
	private GameObject action_bar;

	private Texture2D dialog1;
	private const int reference_width = 650; 
	private const int reference_height = 300;

	private float timer;
	private float time;
	private bool active_dialog = false;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player");
		player_move = player.GetComponent<ClickToMove_lvl2> ();
		skill_ctrl = player.GetComponent<Skill_Controller> ();
		action_bar = GameObject.FindGameObjectWithTag ("ActionBar");
		player_move.enabled = false;
		skill_ctrl.enabled = false;
		action_bar.SetActive (false);
	
		this.dialog1 = Resources.Load<Texture2D>("Lvl2/Dialogs/caution_fire_dialog");

		timer = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		time = Time.time - timer;

		if (Input.GetKeyDown (KeyCode.KeypadEnter) || Input.GetKeyDown  (KeyCode.Return)) {
			if(active_dialog) timer = Time.time - 5.5f;
		}

		if (time > 5.5f) active_dialog = false;

		if (time > 6.50f && time < 9.0f) {
			firetrap.gameObject.SetActive (true);
		}

		if (time > 10.0f) {
			firetrap.tag = "FireTrap";
			player_move.enabled = true;
			skill_ctrl.enabled = true;
			action_bar.SetActive (true);
			this.gameObject.SetActive(false);
		}
	}

	void OnGUI () {
		if(time > 1.0f && time < 5.5f) {
			drawDialog ();
			if (!active_dialog) active_dialog = true;
		}
	}

	void drawDialog () {
		float height_rate = 1.0f;
		//if (Screen.height * 1.5f < Screen.width) height_rate = 0.5f; 
		Rect continue_box = new Rect (Screen.width/5.0f, 
		                              Screen.height - (Screen.height/2.8f), 
		                              //this.dialog1.width / 1.0f, 
		                              //this.dialog1.height / 1.0f);
		                              this.resizeTextureWidth(this.dialog1) / 2.75f, 
		                              this.resizeTextureHeight(this.dialog1) / 1.5f);
		Graphics.DrawTexture (continue_box, this.dialog1);
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
