using UnityEngine;
using System.Collections;

public class ActionBarScript : MonoBehaviour {

	// ACTION BAR
	// --- TEXTURES ---
	private Texture2D fireballTexture;
	private Texture2D useFireballTexture;
	private Texture2D reloadFireballTexture;
	private Texture2D actionBarTexture;
	private Texture2D runIconTexture;
	private Texture2D attackIconTexture;
	private Texture2D potionIconTexture;
	private Texture2D inventoryIconTexture;

	public Rect actionbar_box;
	public Rect skill_box;

	private GUIStyle text_style;
	private GUIStyle guiStyleBack;

	public static InventoryScript inventory;

	private CharacterScript cs;
	private GameObject character;

	private bool takePotion = false;

	public static bool useSkill1 = false;
	public static bool disabledSkill1 = false;

	// Use this for initialization
	void Start () {

		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();

		// ADD TEXTURES
		this.actionBarTexture = Resources.Load<Texture2D>("ActionBar/actionbar_v4");

		this.fireballTexture = Resources.Load<Texture2D>("ActionBar/skill_fireball");
		this.useFireballTexture = Resources.Load<Texture2D>("ActionBar/use_fireball");
		this.reloadFireballTexture = Resources.Load<Texture2D>("ActionBar/reload_fireball");

		this.attackIconTexture = Resources.Load<Texture2D>("ActionBar/icon_attack_v2");
		this.runIconTexture = Resources.Load<Texture2D>("ActionBar/icon_run");
		this.potionIconTexture = Resources.Load<Texture2D>("ActionBar/icon_potion_v2");
		this.inventoryIconTexture = Resources.Load<Texture2D>("ActionBar/icon_inventory_v2");

		//Debug.Log (this.actionBarTexture.name);

		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryScript> ();

		//this.cs = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();

		this.text_style = new GUIStyle ();
		this.text_style.normal.textColor = Color.red;
		this.text_style.fontSize = 15;
		this.text_style.alignment = TextAnchor.UpperCenter ; 
		this.text_style.wordWrap = true; 



	}

	void Update() {

		if (Input.GetMouseButtonDown (1)) {
			//MOUSE BUTTON RIGHT ATTACK
			this.character.animation.CrossFade ("metarig|Atacar", 0.2f);
		} else if (Input.GetKeyDown(KeyCode.Q) && inventory.getPotion() != 0) {
			// ACTION TAKE POTION
				this.cs.setCure(10);
				inventory.setPotion(-1);
		}

		
	}

	void OnGUI(){
		this.drawActionBar ();
	}

	public float x;
	public float x2;
	public float x3;

	void drawActionBar(){

		// ACTION BAR
		GUI.DrawTexture(new Rect (Screen.width * this.actionbar_box.x,
		                          Screen.height * this.actionbar_box.y,
		                          Screen.width * this.actionbar_box.width,
		                          Screen.height * this.actionbar_box.height),
		                this.actionBarTexture);

		//SKILLS
		// FIREBALL ICON

		if(useSkill1 == false && disabledSkill1 == false){
			GUI.DrawTexture( new Rect (Screen.width * 0.3041f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.fireballTexture);
		}else if(disabledSkill1 == true){
			//this.cs.setMagic(-15);
			GUI.DrawTexture( new Rect (Screen.width * 0.3041f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.reloadFireballTexture);
		}

		// MOUSE SKILL

		// RUN ICON
		GUI.DrawTexture( new Rect (Screen.width * 0.4705f,
		                           Screen.height * 0.90075f,
		                           Screen.width * 0.034f,
		                           Screen.height * 0.0685f), 
		                this.runIconTexture);

		// ATTACK ICON
		GUI.DrawTexture( new Rect (Screen.width * 0.5115f,
		                           Screen.height * 0.90075f,
		                           Screen.width * 0.034f,
		                           Screen.height * 0.0685f), 
		                this.attackIconTexture);


		// OPTION SKILL

		// IF IN THE INVENTORY HAS POTION SHOW IT
		if (inventory.getPotion () != 0) {

			GUI.DrawTexture( new Rect (Screen.width * 0.5525f,
			                           Screen.height * 0.90075f,
			                           Screen.width * 0.034f,
			                           Screen.height * 0.0685f), 
			                  this.potionIconTexture);

			GUI.Label (new Rect(Screen.width * 0.305f,
			                    Screen.height * 0.94f,
			                    Screen.width * 0.55f,
			                    Screen.height * 0.25f),
			           inventory.getPotion().ToString(),
			           this.text_style);



		}

		// BAG ICON
		GUI.DrawTexture( new Rect (Screen.width * 0.622f,
		                           Screen.height * 0.90075f,
		                           Screen.width * 0.034f,
		                           Screen.height * 0.0685f), 
		                this.inventoryIconTexture);


	}
}
