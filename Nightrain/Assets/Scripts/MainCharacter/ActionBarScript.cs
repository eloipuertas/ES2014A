using UnityEngine;
using System.Collections;

public class ActionBarScript : MonoBehaviour {

	// ACTION BAR
	// --- TEXTURES ---
	private Texture2D fireballTexture;
	private Texture2D reloadFireballTexture;
	private Texture2D PowerKnifeTexture;
	private Texture2D reloadPowerKnifeTexture;
	private Texture2D DarkAuraTexture;
	private Texture2D reloadDarkAuraTexture;
	private Texture2D actionBarTexture;
	private Texture2D runIconTexture;
	private Texture2D attackIconTexture;
	private Texture2D potionIconTexture;
	private Texture2D inventoryIconTexture;
	private Texture2D mapIconTexture;

	public Rect actionbar_box;
	public Rect skill_box;

	private GUIStyle text_style;
	private GUIStyle guiStyleBack;

	public static InventoryScript inventory;

	private CharacterScript cs;
	private ClickToMove cm;
	//private ClickToMove_lvl2 cm2;
	private GameObject character;

	private bool takePotion = false;

	public static bool disabledSkill1 = false;
	public static bool disabledSkill2 = false;
	public static bool disabledSkill3 = false;

	// Use this for initialization
	void Start () {

		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();
		this.cm = this.character.GetComponent<ClickToMove> ();
		//this.cm2 = this.character.GetComponent<ClickToMove_lvl2> ();

		// ADD TEXTURES
		this.actionBarTexture = Resources.Load<Texture2D>("ActionBar/actionbar");

		this.fireballTexture = Resources.Load<Texture2D>("ActionBar/skill_fireball");
		this.reloadFireballTexture = Resources.Load<Texture2D>("ActionBar/reload_fireball");
		this.PowerKnifeTexture = Resources.Load<Texture2D>("ActionBar/skill_powerKnife");
		this.reloadPowerKnifeTexture = Resources.Load<Texture2D>("ActionBar/reload_powerKnife");
		this.DarkAuraTexture = Resources.Load<Texture2D>("ActionBar/skill_aura");
		this.reloadDarkAuraTexture = Resources.Load<Texture2D>("ActionBar/reload_aura");

		this.attackIconTexture = Resources.Load<Texture2D>("ActionBar/icon_attack_v2");
		this.runIconTexture = Resources.Load<Texture2D>("ActionBar/icon_run");
		this.potionIconTexture = Resources.Load<Texture2D>("ActionBar/icon_elixir_v2");
		this.inventoryIconTexture = Resources.Load<Texture2D>("ActionBar/icon_inventory_v2");
		this.mapIconTexture = Resources.Load<Texture2D>("ActionBar/icon_map");

		//Debug.Log (this.actionBarTexture.name);

		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryScript> ();

		//this.cs = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();

		this.text_style = new GUIStyle ();
		this.text_style.normal.textColor = Color.red;
		this.text_style.fontSize = 15;
		this.text_style.alignment = TextAnchor.UpperCenter ; 
		this.text_style.wordWrap = true; 

		disabledSkill1 = false;
		disabledSkill2 = false;
		disabledSkill3 = false;


	}

	void Update() {

		if (Input.GetKeyDown(KeyCode.Q) && inventory.getPotion() != 0) {
			// ACTION TAKE POTION
			this.cs.setCure(999);
			this.cs.setRecoverMagic(999);	
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
		if(disabledSkill1 == false){
			GUI.DrawTexture( new Rect (Screen.width * 0.3041f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.fireballTexture);
		}else if(disabledSkill1 == true){
			//this.cs.setMagic(-15);
			GUI.DrawTexture( new Rect (Screen.width * 0.3041f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.reloadFireballTexture);
		}

		// Knife ICON
		if(disabledSkill2 == false){
			GUI.DrawTexture( new Rect (Screen.width * 0.3445f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.PowerKnifeTexture);
		}else if(disabledSkill2 == true){
			//this.cs.setMagic(-15);
			GUI.DrawTexture( new Rect (Screen.width * 0.3445f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.reloadPowerKnifeTexture);
		}

		// Aura ICON
		if(disabledSkill3 == false){
			GUI.DrawTexture( new Rect (Screen.width * 0.386f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.DarkAuraTexture);
		}else if(disabledSkill3 == true){
			//this.cs.setMagic(-15);
			GUI.DrawTexture( new Rect (Screen.width * 0.386f, Screen.height * 0.90075f, Screen.width * 0.034f, Screen.height * 0.0685f), 
			                this.reloadDarkAuraTexture);
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

		// MAP ICON
		GUI.DrawTexture( new Rect (Screen.width * 0.665f,
		                           Screen.height * 0.90075f,
		                           Screen.width * 0.034f,
		                           Screen.height * 0.0685f), 
		                this.mapIconTexture);


	}
}
