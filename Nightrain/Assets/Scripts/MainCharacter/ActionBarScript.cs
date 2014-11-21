using UnityEngine;
using System.Collections;

public class ActionBarScript : MonoBehaviour {

	// ACTION BAR
	// --- TEXTURES ---
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

	// Use this for initialization
	void Start () {

		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();

		// ADD TEXTURES
		this.actionBarTexture = Resources.Load<Texture2D>("ActionBar/actionbar_v3");

		this.attackIconTexture = Resources.Load<Texture2D>("ActionBar/icon_attack");
		this.runIconTexture = Resources.Load<Texture2D>("ActionBar/icon_run");
		this.potionIconTexture = Resources.Load<Texture2D>("ActionBar/icon_potion");
		this.inventoryIconTexture = Resources.Load<Texture2D>("ActionBar/icon_inventory");

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

	void drawActionBar(){

		// ACTION BAR
		GUI.DrawTexture(new Rect (Screen.width * this.actionbar_box.x,
		                          Screen.height * this.actionbar_box.y,
		                          Screen.width * this.actionbar_box.width,
		                          Screen.height * this.actionbar_box.height),
		                this.actionBarTexture);

		// RUN ICON
		GUI.DrawTexture( new Rect (Screen.width * 0.47f,
		                           Screen.height * 0.925f,
		                           Screen.width * 0.03f,
		                           Screen.height * 0.05f), 
		                this.runIconTexture);

		// ATTACK ICON
		GUI.DrawTexture( new Rect (Screen.width * 0.508f,
		                           Screen.height * 0.927f,
		                           Screen.width * 0.03f,
		                           Screen.height * 0.05f), 
		                this.attackIconTexture);

		// IF IN THE INVENTORY HAS POTION SHOW IT
		if (inventory.getPotion () != 0) {

			GUI.DrawTexture( new Rect (Screen.width * 0.535f,
			                            Screen.height * 0.9275f,
			                            Screen.width * 0.05f,
			                            Screen.height * 0.05f), 
			                  this.potionIconTexture);

			GUI.Label (new Rect(Screen.width * 0.29f,
			                    Screen.height * 0.95f,
			                    Screen.width * 0.55f,
			                    Screen.height * 0.25f),
			           inventory.getPotion().ToString(),
			           this.text_style);



		}

		// BAG ICON
		GUI.DrawTexture( new Rect (Screen.width * 0.6f,
		                           Screen.height * 0.9275f,
		                           Screen.width * 0.05f,
		                           Screen.height * 0.05f), 
		                this.inventoryIconTexture);


	}
}
