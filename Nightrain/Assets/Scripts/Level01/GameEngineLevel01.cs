using UnityEngine;
using System.Collections;

public class GameEngineLevel01 : MonoBehaviour {

	private float width = Screen.width;
	private float height = Screen.height;

	private RaycastHit getObjectScene;

	// -- MENU PAUSE ---
	private Texture2D pauseTexture;
	private Texture2D continueTexture;
	private Texture2D continue_hover_Texture;
	private Texture2D resetTexture;
	private Texture2D reset_hover_Texture;
	private Texture2D optionTexture;
	private Texture2D option_hover_Texture;
	private Texture2D exitTexture;
	private Texture2D exit_hover_Texture;
	private bool pause = false;

	// --- CURSOR ---
	public Texture2D[] cursor;
	private CursorMode mode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;

	// --- CHARACTER ---
	private Transform prefab;
	private GameObject character;
	
	// --- LIGHT
	public GameObject ambientLight;
	private CharacterScript cs;
	private Color c;

	
	// Use this for initialization
	void Start () {

		Cursor.SetCursor(cursor[0], hotSpot, mode);

		string str_character = PlayerPrefs.GetString ("Character");
		string dificulty = PlayerPrefs.GetString ("Dificulty");

		// --- LOAD RESOURCES TO MENU ---
		this.pauseTexture = Resources.Load<Texture2D>("PauseMenu/pause");
		this.continueTexture = Resources.Load<Texture2D>("PauseMenu/continue");
		this.continue_hover_Texture = Resources.Load<Texture2D>("PauseMenu/continue_hover");
		this.resetTexture = Resources.Load<Texture2D>("PauseMenu/reset");
		this.reset_hover_Texture = Resources.Load<Texture2D>("PauseMenu/reset_hover");
		this.optionTexture = Resources.Load<Texture2D>("PauseMenu/option");
		this.option_hover_Texture = Resources.Load<Texture2D>("PauseMenu/option_hover");
		this.exitTexture = Resources.Load<Texture2D>("PauseMenu/exit");
		this.exit_hover_Texture = Resources.Load<Texture2D>("PauseMenu/exit_hover");

		// --- LOAD RESOURCES TO CHARACTER ---
		this.prefab = Resources.Load<Transform>("Prefabs/MainCharacters/" + PlayerPrefs.GetString("Character"));

		Instantiate (prefab);
		this.character = GameObject.FindGameObjectWithTag ("Player");

		this.cs = this.character.GetComponent<CharacterScript> ();
		this.c = this.ambientLight.light.color;

		print ("Se ha cargado el personaje " + str_character);
	}
	
	// Update is called once per frame
	void Update () {

	
		this.PauseScreen ();
		this.StateMachine ();

	}


	void StateMachine(){
	
		if (Input.GetMouseButtonDown (0)) {
			Cursor.SetCursor (cursor [1], hotSpot, mode);
		} else if (Input.GetMouseButtonUp (0)) {
			Cursor.SetCursor (cursor [0], hotSpot, mode);
		} else if (Input.GetKeyDown (KeyCode.Escape) && !this.pause) {
			this.pause = true;
			Time.timeScale = 0;
		} else if (Input.GetKeyDown (KeyCode.Escape) && this.pause) {
			this.pause = false;
			Time.timeScale = 1;
		}

		this.isAlive ();
	}

	//Comprueba si el personaje sigue vivo
	void isAlive(){
		int num = this.character.GetComponent<CharacterScript> ().getHeal();
		//If the character is dead we show "game over" scene
		if(num <= 0) Application.LoadLevel(3);
	}


	void PauseScreen(){
		
		if (this.pause && !cs.isCritical ())
			this.ambientLight.light.color = new Color (.2f, .2f, .2f);
		else if (!this.pause && !cs.isCritical ())
			this.ambientLight.light.color = new Color (1.0f, 1.0f, 1.0f);
		else if (this.pause && cs.isCritical ())
			this.ambientLight.light.color = new Color (.5f, .25f, .5f);
		else if (!this.pause && cs.isCritical ())
			this.CautionScreen ();
	}


	// Efecto critico con luz roja
	void CautionScreen(){

		if (cs.isCritical()) {
			this.c.r = 1.0f;
			if(this.c.g >= 0.5f)
				this.c.g -= 0.02f;
			if(this.c.b >= 0.5f)
				this.c.b -= 0.02f;
			this.ambientLight.light.color = this.c;
		} else {
			this.c.r = 1.0f;
			if(this.c.g <= 1.0f)
				this.c.g += 0.02f;
			if(this.c.b <= 1.0f)
				this.c.b += 0.02f;
			this.ambientLight.light.color = this.c;
		}

	}
	

	void PauseMenu(){


				//if (Event.current.type.Equals (EventType.Repaint)) {

				// MENU INTERFACE
				Rect menu_box = new Rect ((this.width / 2) - (this.pauseTexture.width / 2),
			                          (this.height / 2) - (this.pauseTexture.height / 2.6f),
			                          this.pauseTexture.width,
			                          this.pauseTexture.height / 1.3f);
				Graphics.DrawTexture (menu_box, this.pauseTexture);

				Rect continue_box = new Rect (menu_box.center.x - this.continueTexture.width / 2,
			                              menu_box.center.y - this.continueTexture.height * 1.45f,
			                              this.continueTexture.width,
			                              this.continueTexture.height / 1.25f);
				Graphics.DrawTexture (continue_box, this.continueTexture);

				if (continue_box.Contains (Event.current.mousePosition)) {
						Graphics.DrawTexture (continue_box, this.continue_hover_Texture);
						if (Input.GetMouseButtonDown (0)){ 
							this.pause = false;
							Time.timeScale = 1;
						}
				}else
						Graphics.DrawTexture (continue_box, this.continueTexture);

				Rect reset_box = new Rect (continue_box.position.x,
			                           continue_box.position.y + this.resetTexture.height / 1.1f,
                              		   this.resetTexture.width,
			                           this.resetTexture.height / 1.25f);
				Graphics.DrawTexture (reset_box, this.resetTexture);

				if (reset_box.Contains (Event.current.mousePosition)) {
						Graphics.DrawTexture (reset_box, this.reset_hover_Texture);
						if (Input.GetMouseButtonDown (0)){
							Time.timeScale = 1;
							Application.LoadLevel (2);	
						}
				} else
						Graphics.DrawTexture (reset_box, this.resetTexture);

				Rect option_box = new Rect (reset_box.position.x,
			                            reset_box.position.y + this.optionTexture.height / 1.1f,
			                            this.optionTexture.width,
			                            this.optionTexture.height / 1.25f);
				Graphics.DrawTexture (option_box, this.optionTexture);

				if (option_box.Contains (Event.current.mousePosition))
						Graphics.DrawTexture (option_box, this.option_hover_Texture);
				else
						Graphics.DrawTexture (option_box, this.optionTexture);

				Rect exit_box = new Rect (option_box.position.x,
			                          option_box.position.y + this.exitTexture.height / 1.1f,
			                          this.exitTexture.width,
			                          this.exitTexture.height / 1.25f);
				Graphics.DrawTexture (exit_box, this.exitTexture);

				if (exit_box.Contains (Event.current.mousePosition)){
					Graphics.DrawTexture (exit_box, this.exit_hover_Texture);
					if (Input.GetMouseButtonDown (0)){
						Time.timeScale = 1;
						Application.LoadLevel (1);	
					}
				}else
					Graphics.DrawTexture (exit_box, this.exitTexture);

	}


	void OnGUI(){
		
		if(this.pause)
			this.PauseMenu ();
		
	}

}
