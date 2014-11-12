using UnityEngine;
using System.Collections;

public class MainMenuGUI {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	// ====== TEXTURES MAIN MENU ======
	private Texture2D backgroundTexture;
	private Texture2D logoTexture;
	private Texture2D newGameTexture;
	private Texture2D hoverNewGameTexture;
	private Texture2D loadGameTexture;
	private Texture2D hoverLoadGameTexture;
	private Texture2D exitGameTexture;
	private Texture2D hoverExitGameTexture;
	
	// ====== TEXTURES CHARACTER SELECTOR ======
	private Texture2D selectorCharacterTexture;
	private Texture2D btnWarriorTexture;
	private Texture2D hoverBtnWarriorTexture;
	private Texture2D btnSageTexture;
	private Texture2D hoverBtnSageTexture;
	private Texture2D btnThiefTexture;
	private Texture2D hoverBtnThiefTexture;
	private Texture2D btnExitTexture;
	private Texture2D hoverBtnExitTexture;
	private Texture2D btnConfirmTexture;
	private Texture2D hoverBtnConfirmTexture;
	private Texture2D warriorTexture;
	private Texture2D warriorAttributesTexture;
	private Texture2D sageTexture;
	private Texture2D sageAttributesTexture;
	private Texture2D thiefTexture;
	private Texture2D thiefAttributesTexture;


	// ====== TEXTURES DIFFICULTY SELECTOR ======

	private Texture2D windowDifficultyTexture;
	private Texture2D squireDifficultyTexture;
	private Texture2D hoverSquireDifficultyTexture;
	private Texture2D warriorDifficultyTexture;
	private Texture2D hoverWarriorDifficultyTexture;
	private Texture2D knightDifficultyTexture;
	private Texture2D hoverKnightDifficultyTexture;
	private Texture2D paladinDifficultyTexture;
	private Texture2D hoverPaladinDifficultyTexture;
	private Texture2D easyTexture;
	private Texture2D normalTexture;
	private Texture2D hardTexture;
	private Texture2D extremeTexture;

	private Texture2D characterTexture;
	private Texture2D attributeTexture;
	private Texture2D difficultyTexture;

	public AudioSource clip;

	// Hover button audio Source
	private GameObject hoverSound;

	// Variable to check current mouse hover button
	private Rect hoveredButton = new Rect();

	// SHOW MAIN MENU
	private bool isMainMenu = true;
	// SHOW CHARACTER SELECTOR
	private bool isCharacterSelector = false;
	// SHOW DIFFICULTY SELECTOR
	private bool isDifficultySelector = false;
	private bool isInsideWindow = false;

	// SAVE CHARACTER SELECTED
	private string character = "hombre";
	

	// CONSTRUCTOR
	public MainMenuGUI(){}
	
	// LOAD TEXTURE RESOURCES
	public void initResources () {
	
		// MAIN MENU
		this.backgroundTexture = Resources.Load<Texture2D>("MainMenu/background_mainmenu");		
		this.logoTexture = Resources.Load<Texture2D>("MainMenu/logo");

		this.newGameTexture = Resources.Load<Texture2D>("MainMenu/play");
		this.hoverNewGameTexture = Resources.Load<Texture2D>("MainMenu/hover_play");

		this.loadGameTexture = Resources.Load<Texture2D>("MainMenu/load");
		this.hoverLoadGameTexture = Resources.Load<Texture2D>("MainMenu/hover_load");
		
		this.exitGameTexture = Resources.Load<Texture2D>("MainMenu/exit");
		this.hoverExitGameTexture = Resources.Load<Texture2D>("MainMenu/hover_exit");

		// CHARACTER SELECTOR
		this.selectorCharacterTexture = Resources.Load<Texture2D>("MainMenu/logo_selector");	
		
		this.btnWarriorTexture = Resources.Load<Texture2D>("MainMenu/cube");
		this.hoverBtnWarriorTexture = Resources.Load<Texture2D>("MainMenu/hover_cube");
		
		this.btnSageTexture = Resources.Load<Texture2D>("MainMenu/sphere");
		this.hoverBtnSageTexture = Resources.Load<Texture2D>("MainMenu/hover_sphere");
		
		this.btnThiefTexture = Resources.Load<Texture2D>("MainMenu/triangle");
		this.hoverBtnThiefTexture = Resources.Load<Texture2D>("MainMenu/hover_triangle");

		this.btnExitTexture = Resources.Load<Texture2D>("MainMenu/exit");
		this.hoverBtnExitTexture = Resources.Load<Texture2D>("MainMenu/hover_exit");
		
		this.btnConfirmTexture = Resources.Load<Texture2D>("MainMenu/confirm");
		this.hoverBtnConfirmTexture = Resources.Load<Texture2D>("MainMenu/hover_confirm");

		this.warriorTexture = Resources.Load<Texture2D>("MainMenu/warrior");
		this.warriorAttributesTexture = Resources.Load<Texture2D>("MainMenu/attributes_warrior");

		this.sageTexture = Resources.Load<Texture2D>("MainMenu/sage");
		this.sageAttributesTexture = Resources.Load<Texture2D>("MainMenu/attributes_sage");

		this.thiefTexture = Resources.Load<Texture2D>("MainMenu/warrior");
		this.thiefAttributesTexture = Resources.Load<Texture2D>("MainMenu/attributes_thief");

		// DIFFICULTY SELECTOR
		this.windowDifficultyTexture = Resources.Load<Texture2D>("MainMenu/window_difficulty");

		this.squireDifficultyTexture = Resources.Load<Texture2D>("MainMenu/escudero");
		this.hoverSquireDifficultyTexture = Resources.Load<Texture2D>("MainMenu/hover_escudero");

		this.warriorDifficultyTexture = Resources.Load<Texture2D>("MainMenu/guerrero");
		this.hoverWarriorDifficultyTexture = Resources.Load<Texture2D>("MainMenu/hover_guerrero");

		this.knightDifficultyTexture = Resources.Load<Texture2D>("MainMenu/caballero");
		this.hoverKnightDifficultyTexture = Resources.Load<Texture2D>("MainMenu/hover_caballero");

		this.paladinDifficultyTexture = Resources.Load<Texture2D>("MainMenu/paladin");
		this.hoverPaladinDifficultyTexture = Resources.Load<Texture2D>("MainMenu/hover_paladin");

		this.easyTexture = Resources.Load<Texture2D>("MainMenu/difficulty_easy");
		this.normalTexture = Resources.Load<Texture2D>("MainMenu/difficulty_normal");
		this.hardTexture = Resources.Load<Texture2D>("MainMenu/difficulty_hard");
		this.extremeTexture = Resources.Load<Texture2D>("MainMenu/difficulty_extreme");

		this.characterTexture = this.warriorTexture;
		this.attributeTexture = this.warriorAttributesTexture;
		this.difficultyTexture = this.normalTexture;

		//Hover button music gameobject
		this.hoverSound = GameObject.FindGameObjectWithTag("music_engine");
	}


	// MAIN MENU
	public void mainMenu(){

		// BACKGROUND MAINMENU
		Rect background_box = new Rect (0, 0, Screen.width, Screen.height);
		Graphics.DrawTexture (background_box, this.backgroundTexture);

		// ============== DRAW MAINMENU ===================

		if (this.isMainMenu) {
			
			// LOGO GAME
			Rect logo_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.logoTexture)/2,
			                          0,
			                          this.resizeTextureWidth(this.logoTexture),
			                          this.resizeTextureHeight(this.logoTexture));
			Graphics.DrawTexture (logo_box, this.logoTexture);

			// NEW GAME
			Rect play_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.newGameTexture)/2,
			                          Screen.height/1.9f,
			                          this.resizeTextureWidth(this.newGameTexture),
			                          this.resizeTextureHeight(this.newGameTexture));
			Graphics.DrawTexture (play_box, this.newGameTexture);

			// LOAD GAME
			Rect load_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.loadGameTexture)/2,
			                          Screen.height/1.6f,
			                          this.resizeTextureWidth(this.loadGameTexture),
			                          this.resizeTextureHeight(this.loadGameTexture));
			Graphics.DrawTexture (load_box, this.loadGameTexture);

			// EXIT GAME
			Rect exit_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.exitGameTexture)/2,
			                          Screen.height/1.38f,
			                          this.resizeTextureWidth(this.exitGameTexture),
			                          this.resizeTextureHeight(this.exitGameTexture));
			Graphics.DrawTexture (exit_box, this.exitGameTexture);


			// ============== ACTION BUTTONS ===================
			
			// ACTION PLAY GAME BUTTON
			if (play_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (play_box, this.hoverNewGameTexture);
				if (hoveredButton != play_box) {
					hoverSound.audio.Play ();
					hoveredButton = play_box;
				}
				if (Input.GetMouseButtonDown (0)) { 
					this.clip.Play();
					this.isMainMenu = false;
					this.isCharacterSelector = true;
				}
			} else {
				Graphics.DrawTexture (play_box, this.newGameTexture);
				if(hoveredButton == play_box) hoveredButton = new Rect();
			}

			// ACTION LOAD GAME BUTTON
			if (load_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (load_box, this.hoverLoadGameTexture);
				if (hoveredButton != load_box) {
					hoverSound.audio.Play ();
					hoveredButton = load_box;
				}
				if (Input.GetMouseButtonDown (0)) { 
					this.clip.Play();
					Debug.Log ("Cargar Datos");
				}
			} else {
				Graphics.DrawTexture (load_box, this.loadGameTexture);
				if(hoveredButton == load_box) hoveredButton = new Rect();
			}
			// ACTION EXIT GAME BUTTON
			if (exit_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (exit_box, this.hoverExitGameTexture);
				if (hoveredButton != exit_box) {
					hoverSound.audio.Play ();
					hoveredButton = exit_box;
				}
				if (Input.GetMouseButtonDown (0)) { 
					this.clip.Play();
					Application.Quit();
				}
			} else {
				Graphics.DrawTexture (exit_box, this.exitGameTexture);
				if(hoveredButton == exit_box) hoveredButton = new Rect();
			}
		}

	}


	// MAIN MENU
	public void characterSelector(){

		if (this.isCharacterSelector) {
		
			// LOGO GAME
			Rect logo_box = new Rect (10,
			                          this.resizeTextureHeight(this.selectorCharacterTexture)/1.75f,
			                          this.resizeTextureWidth(this.selectorCharacterTexture)/2.5f,
			                          this.resizeTextureHeight(this.selectorCharacterTexture)/2.5f);
			Graphics.DrawTexture (logo_box, this.selectorCharacterTexture);
			
			// WARRIOR
			Rect warrior_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.btnWarriorTexture)/1.63f,
			                          Screen.height/1.9f - this.resizeTextureHeight(this.btnWarriorTexture)/4f,
			                          this.resizeTextureWidth(this.btnWarriorTexture)/4,
			                          this.resizeTextureHeight(this.btnWarriorTexture)/4);
			Graphics.DrawTexture (warrior_box, this.btnWarriorTexture);
			
			// SAGE
			Rect sage_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.btnSageTexture)/1.63f,
			                          Screen.height/1.61f - this.resizeTextureHeight(this.btnSageTexture)/4f,
			                          this.resizeTextureWidth(this.btnSageTexture)/4,
			                          this.resizeTextureHeight(this.btnSageTexture)/4);
			Graphics.DrawTexture (sage_box, this.btnSageTexture);
			
			// THIEF
			Rect thief_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.btnThiefTexture)/1.63f,
			                           Screen.height/1.4f - this.resizeTextureHeight(this.btnThiefTexture)/4f,
			                           this.resizeTextureWidth(this.btnThiefTexture)/4,
			                           this.resizeTextureHeight(this.btnThiefTexture)/4);
			Graphics.DrawTexture (thief_box, this.btnThiefTexture);

			// CHARACTER
			Rect character_box = new Rect (Screen.width/1.3f - this.resizeTextureWidth(this.warriorTexture)/2,
			                               Screen.height/1.47f - this.resizeTextureHeight(this.warriorTexture)/2,
			                               this.resizeTextureWidth(this.warriorTexture)/1.5f,
			                               this.resizeTextureHeight(this.warriorTexture)/1.5f);
			Graphics.DrawTexture (character_box, this.characterTexture);

			// ATTRIBUTES
			Rect attributes_box = new Rect (Screen.width/1.27f,
			                                10,
			                                this.resizeTextureWidth(this.warriorAttributesTexture)/3.5f,
			                                this.resizeTextureHeight(this.warriorAttributesTexture)/3.5f);
			Graphics.DrawTexture (attributes_box, this.attributeTexture);

			// CONFIRM BUTTON
			Rect confirm_box = new Rect (Screen.width/1.25f,
		                              	 Screen.height/1.13f,
	                                  	 this.resizeTextureWidth(this.btnConfirmTexture)/1.25f,
			                          	 this.resizeTextureHeight(this.btnConfirmTexture));
			Graphics.DrawTexture (confirm_box, this.btnConfirmTexture);

			// BACK BUTTON
			Rect back_box = new Rect (Screen.width/1.6f,
			                          Screen.height/1.13f,
		                              this.resizeTextureWidth(this.btnExitTexture)/1.25f,
			                          this.resizeTextureHeight(this.btnExitTexture));
			Graphics.DrawTexture (back_box, this.btnExitTexture);


			// ============== ACTION BUTTONS ===================
			
			// ACTION WARRIOR BUTTON
			if (warrior_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (warrior_box, this.hoverBtnWarriorTexture);
				if (hoveredButton != warrior_box) {
					hoverSound.audio.Play ();
					hoveredButton = warrior_box;
				}
				if (Input.GetMouseButtonDown (0)) { 
					this.clip.Play();
					this.character = "hombre";
					this.characterTexture = this.warriorTexture;
					this.attributeTexture = this.warriorAttributesTexture;
					Debug.Log ("Click en Warrior");
				}
			} else {
				Graphics.DrawTexture (warrior_box, this.btnWarriorTexture);
				if(hoveredButton == warrior_box) hoveredButton = new Rect();
			}

			// ACTION SAGE BUTTON
			if (sage_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (sage_box, this.hoverBtnSageTexture);
				if (hoveredButton != sage_box) {
					hoverSound.audio.Play ();
					hoveredButton = sage_box;
				}
				if (Input.GetMouseButtonDown (0)) { 
					this.clip.Play();
					this.character = "mujer";
					this.characterTexture = this.sageTexture;
					this.attributeTexture = this.sageAttributesTexture;
					Debug.Log ("Click en Sage");
				}
			} else {
				Graphics.DrawTexture (sage_box, this.btnSageTexture);
				if(hoveredButton == sage_box) hoveredButton = new Rect();
			}

			// ACTION THIEF BUTTON
			if (thief_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (thief_box, this.hoverBtnThiefTexture);
				if (hoveredButton != thief_box) {
					hoverSound.audio.Play ();
					hoveredButton = thief_box;
				}
				if (Input.GetMouseButtonDown (0)) { 
					this.clip.Play();
					this.character = "joven";
					this.characterTexture = this.thiefTexture;
					this.attributeTexture = this.thiefAttributesTexture;
					Debug.Log ("Click en Thief");
				}
			} else {
				Graphics.DrawTexture (thief_box, this.btnThiefTexture);
				if(hoveredButton == thief_box) hoveredButton = new Rect();
			}

			// ACTION BACK BUTTON
			if (back_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (back_box, this.hoverBtnExitTexture);
				if (hoveredButton != back_box) {
					hoverSound.audio.Play ();
					hoveredButton = back_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					this.isCharacterSelector = false;
					this.isMainMenu = true;
					this.clip.Play();
				}
			} else {
				Graphics.DrawTexture (back_box, this.btnExitTexture);
				if(hoveredButton == back_box) hoveredButton = new Rect();
			}

			// ACTION CONFIRM BUTTON
			if (confirm_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (confirm_box, this.hoverBtnConfirmTexture);
				if (hoveredButton != confirm_box) {
					hoverSound.audio.Play ();
					hoveredButton = confirm_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					this.clip.Play();
					this.isDifficultySelector = true;
					//Application.LoadLevel(2);
				}
			} else {
				Graphics.DrawTexture (confirm_box, this.btnConfirmTexture);
				if(hoveredButton == confirm_box) hoveredButton = new Rect();
			}
		}			

	}


	// DIFFICULTY SELECTOR
	public void difficultySelector(){

		if (this.isDifficultySelector) {

			// DIFFICULTY WINDOW
			Rect window_box = new Rect (Screen.width/2 - this.resizeTextureWidth(this.windowDifficultyTexture)/4,
			                          Screen.height/2 - this.resizeTextureHeight(this.windowDifficultyTexture)/4,
			                            this.resizeTextureWidth(this.windowDifficultyTexture)/2,
			                            this.resizeTextureHeight(this.windowDifficultyTexture)/2);
			Graphics.DrawTexture (window_box, this.windowDifficultyTexture);


			// EASY DIFFICULT
			Rect squire_box = new Rect (window_box.center.x - window_box.center.x/4,
			                            window_box.center.y/1.18f,
			                            this.resizeTextureWidth(this.squireDifficultyTexture)/1.5f,
			                            this.resizeTextureHeight(this.squireDifficultyTexture)/1.5f);
			Graphics.DrawTexture (squire_box, this.squireDifficultyTexture);

			// NORMAL DIFFICULT
			Rect warrior_box = new Rect (window_box.center.x - window_box.center.x/8,
			                            window_box.center.y/1.18f,
			                            this.resizeTextureWidth(this.warriorDifficultyTexture)/1.5f,
			                             this.resizeTextureHeight(this.warriorDifficultyTexture)/1.5f);
			Graphics.DrawTexture (warrior_box, this.warriorDifficultyTexture);

			// HARD DIFFICULT
			Rect knight_box = new Rect (window_box.center.x + this.resizeTextureWidth(this.knightDifficultyTexture)/5.5f,
			                            window_box.center.y/1.18f,
			                            this.resizeTextureWidth(this.knightDifficultyTexture)/1.5f,
			                            this.resizeTextureHeight(this.knightDifficultyTexture)/1.5f);
			Graphics.DrawTexture (knight_box, this.knightDifficultyTexture);

			// EXTREME DIFFICULT
			Rect paladin_box = new Rect (window_box.center.x + window_box.center.x/6.5f,
			                            window_box.center.y/1.18f,
			                            this.resizeTextureWidth(this.paladinDifficultyTexture)/1.5f,
			                             this.resizeTextureHeight(this.paladinDifficultyTexture)/1.5f);
			Graphics.DrawTexture (paladin_box, this.paladinDifficultyTexture);


			// EXTREME DIFFICULT
			Rect text_box = new Rect (window_box.center.x/1.36f,
			                          window_box.center.y + window_box.center.y/6,
			                          this.resizeTextureWidth(this.normalTexture)/1.5f,
			                          this.resizeTextureHeight(this.normalTexture)/1.5f);
			Graphics.DrawTexture (text_box, this.difficultyTexture);


			// ============== ACTION BUTTONS ===================
			
			// ACTION BUTTON
			if (window_box.Contains (Event.current.mousePosition)) {
				//Debug.Log("Estoi dentro de la window");
				this.isInsideWindow = true;
			} else{
				if (Input.GetMouseButtonDown(0) && this.isInsideWindow) { 
					this.isDifficultySelector = false;
					this.isInsideWindow = false;
				}
				//Debug.Log ("Estoi fuera la window");
			}


			// ACTION EASY BUTTON
			if (squire_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (squire_box, this.hoverSquireDifficultyTexture);
				this.difficultyTexture = this.easyTexture;
				if (hoveredButton != squire_box) {
					hoverSound.audio.Play ();
					hoveredButton = squire_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					PlayerPrefs.SetString("Difficulty", "Easy");
					PlayerPrefs.SetString("Character", this.character);
					this.clip.Play();
					Application.LoadLevel(2);
				}
			} else {
				Graphics.DrawTexture (squire_box, this.squireDifficultyTexture);
				if(hoveredButton == squire_box) hoveredButton = new Rect();
			}
			// ACTION NORMAL BUTTON
			if (warrior_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (warrior_box, this.hoverWarriorDifficultyTexture);
				this.difficultyTexture = this.normalTexture;
				if (hoveredButton != warrior_box) {
					hoverSound.audio.Play ();
					hoveredButton = warrior_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					PlayerPrefs.SetString("Difficulty", "Normal");
					PlayerPrefs.SetString("Character", this.character);
					this.clip.Play();
					Application.LoadLevel(2);
				}
			} else {
				Graphics.DrawTexture (warrior_box, this.warriorDifficultyTexture);
				if(hoveredButton == warrior_box) hoveredButton = new Rect();
			}

			// ACTION EASY BUTTON
			if (knight_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (knight_box, this.hoverKnightDifficultyTexture);
				this.difficultyTexture = this.hardTexture;
				if (hoveredButton != knight_box) {
					hoverSound.audio.Play ();
					hoveredButton = knight_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					PlayerPrefs.SetString("Difficulty", "Hard");
					PlayerPrefs.SetString("Character", this.character);
					this.clip.Play();
					Application.LoadLevel(2);
				}
			} else {
				Graphics.DrawTexture (knight_box, this.knightDifficultyTexture);
				if (hoveredButton == knight_box) hoveredButton = new Rect();
			}

			// ACTION EXTREME BUTTON
			if (paladin_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (paladin_box, this.hoverPaladinDifficultyTexture);
				this.difficultyTexture = this.extremeTexture;
				if (hoveredButton != paladin_box) {
					hoverSound.audio.Play ();
					hoveredButton = paladin_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					PlayerPrefs.SetString("Difficulty", "Extreme");
					PlayerPrefs.SetString("Character", this.character);
					this.clip.Play();
					Application.LoadLevel(2);
				}
			} else {
				Graphics.DrawTexture (paladin_box, this.paladinDifficultyTexture);
				if (hoveredButton == paladin_box) hoveredButton = new Rect();
			}
		}
	}

	public void setAudioClick(AudioSource clip){
		this.clip = clip;
	}

	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
}
