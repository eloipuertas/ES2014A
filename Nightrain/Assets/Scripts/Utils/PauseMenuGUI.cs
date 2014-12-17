using UnityEngine;
using System.Collections;

public class PauseMenuGUI {
	
	private const int reference_width = 1366; 
	private const int reference_height = 598;

	private int level;

	// ====== TEXTURES PAUSE MENU ======
	private Texture2D backgroundTexture;
	private Texture2D continueTexture;
	private Texture2D hoverContinueTexture;
	private Texture2D resetTexture;
	private Texture2D hoverResetTexture;
	private Texture2D controlTexture;
	private Texture2D hoverControlTexture;
	private Texture2D optionTexture;
	private Texture2D hoverOptionTexture;
	private Texture2D exitTexture;
	private Texture2D hoverExitTexture;
	
	// ====== TEXTURES CONFIRM MENU ======
	private Texture2D confirmTexture;
	private Texture2D yesTexture;
	private Texture2D hoverYesTexture;
	private Texture2D noTexture;
	private Texture2D hoverNoTexture;

	// ====== TEXTURES OPTION MENU ======
	private Texture2D backgroundOptionTexture;
	private Texture2D checkTexture;
	private Texture2D unCheckTexture;

	// ====== TEXTURES KEYWORD OPTION ======
	private Texture2D keywordTexture;

	// SHOW MENU CONFIRM
	private bool confirm = false;

	// SHOW MENU KEYWORD
	private bool keyword = false;

	// SHOW MENU OPTION
	private bool option = false;
	private bool sound = true;
	private bool light = true;
	
	// Buttons sound effects
	private Rect hoveredButton = new Rect();
	private Music_Engine_Script music;

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;

	private InventoryScript invent;
	private miniMapLv1 map;
	private miniMapLv2 map2;

	private bool minimap;
	private bool inventory;

	private int delay = 0;

	private GameObject shadow;
	private GameObject audio;

	// CONSTRUCTOR
	public PauseMenuGUI(){}
	
	// LOAD TEXTURE RESOURCES
	public void initResources () {

		this.level = PlayerPrefs.GetInt ("Level");
		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData ();

		this.invent = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren <InventoryScript> ();
		this.map = GameObject.FindGameObjectWithTag ("Minimap").GetComponent<miniMapLv1> ();
		this.map2 = GameObject.FindGameObjectWithTag ("Minimap").GetComponent<miniMapLv2> ();

		this.shadow = GameObject.FindGameObjectWithTag("Light");
		this.audio = GameObject.FindGameObjectWithTag("Audio");

		// MENU PAUSE
		this.backgroundTexture = Resources.Load<Texture2D>("PauseMenu/background_pause");
		
		this.continueTexture = Resources.Load<Texture2D>("PauseMenu/continue");
		this.hoverContinueTexture = Resources.Load<Texture2D>("PauseMenu/hover_continue");
		
		this.resetTexture = Resources.Load<Texture2D>("PauseMenu/reset");
		this.hoverResetTexture = Resources.Load<Texture2D>("PauseMenu/hover_reset");

		this.controlTexture = Resources.Load<Texture2D>("PauseMenu/controls");
		this.hoverControlTexture = Resources.Load<Texture2D>("PauseMenu/hover_controls");

		this.optionTexture = Resources.Load<Texture2D>("PauseMenu/option");
		this.hoverOptionTexture = Resources.Load<Texture2D>("PauseMenu/hover_option");
		
		this.exitTexture = Resources.Load<Texture2D>("PauseMenu/exit");
		this.hoverExitTexture = Resources.Load<Texture2D>("PauseMenu/hover_exit");

		// MENU KEYWORD
		this.keywordTexture = Resources.Load<Texture2D>("PauseMenu/keyword");

		// MENU CONFIRM
		this.confirmTexture = Resources.Load<Texture2D>("PauseMenu/background_confirm");

		// MENU OPTION
		this.backgroundOptionTexture = Resources.Load<Texture2D>("PauseMenu/background_opciones");
		this.checkTexture = Resources.Load<Texture2D>("PauseMenu/checked_checkbox");
		this.unCheckTexture = Resources.Load<Texture2D>("PauseMenu/unchecked_checkbox");

		this.yesTexture = Resources.Load<Texture2D>("PauseMenu/yes");
		this.hoverYesTexture = Resources.Load<Texture2D>("PauseMenu/hover_yes");
		
		this.noTexture = Resources.Load<Texture2D>("PauseMenu/no");
		this.hoverNoTexture = Resources.Load<Texture2D>("PauseMenu/hover_no");
		
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
		
	}

	
	// PAUSE MENU
	public bool pauseMenu(bool pause){
		
		if (!this.confirm && !this.keyword && !option) {
			
			// PAUSE INTERFACE
			Rect menu_box = new Rect ((Screen.width / 2) - (this.resizeTextureWidth(this.backgroundTexture) / 2),
			                          (Screen.height / 2) - (this.resizeTextureHeight(this.backgroundTexture) / 2.6f),
			                          this.resizeTextureWidth(this.backgroundTexture),
			                          this.resizeTextureHeight(this.backgroundTexture) / 1.5f);
			Graphics.DrawTexture (menu_box, this.backgroundTexture);
			
			// ============== PAUSE MENU BUTTONS ===================
			
			// BUTTON CONTINUE
			Rect continue_box = new Rect (menu_box.center.x - (this.resizeTextureWidth(this.continueTexture) / 2),
			                              menu_box.center.y - (this.resizeTextureHeight(this.continueTexture) * 1.75f),
			                              this.resizeTextureWidth(this.continueTexture),
			                              this.resizeTextureHeight(this.continueTexture) / 1.25f);
			Graphics.DrawTexture (continue_box, this.continueTexture);
			
			
			// BUTTON RESTART
			Rect reset_box = new Rect (continue_box.position.x,
			                           continue_box.position.y + (this.resizeTextureHeight(this.resetTexture) / 1.1f),
			                           this.resizeTextureWidth(this.resetTexture),
			                           this.resizeTextureHeight(this.resetTexture) / 1.25f);
			Graphics.DrawTexture (reset_box, this.resetTexture);
			

			// BUTTON CONTROLS
			Rect control_box = new Rect (reset_box.position.x,
			                            reset_box.position.y + (this.resizeTextureHeight(this.controlTexture) / 1.1f),
			                             this.resizeTextureWidth(this.controlTexture),
			                             this.resizeTextureHeight(this.controlTexture) / 1.25f);
			Graphics.DrawTexture (control_box, this.controlTexture);


			// BUTTON OPTION
			Rect option_box = new Rect (control_box.position.x,
			                            control_box.position.y + (this.resizeTextureHeight(this.optionTexture) / 1.1f),
			                            this.resizeTextureWidth(this.optionTexture),
			                            this.resizeTextureHeight(this.optionTexture) / 1.25f);
			Graphics.DrawTexture (option_box, this.optionTexture);
			
			
			// BUTTON EXIT
			Rect exit_box = new Rect (option_box.position.x,
			                          option_box.position.y + (this.resizeTextureHeight(this.exitTexture) / 1.1f),
			                          this.resizeTextureWidth(this.exitTexture),
			                          this.resizeTextureHeight(this.exitTexture) / 1.25f);
			Graphics.DrawTexture (exit_box, this.exitTexture);
			
			
			// ============== ACTION BUTTONS ===================
			
			// ACTION CONTINUE BUTTON
			if (continue_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (continue_box, this.hoverContinueTexture);
				if (hoveredButton != continue_box) {
					music.Play_Button_Hover ();
					hoveredButton = continue_box;
				}
				if (Input.GetMouseButtonDown (0)) { 
					pause = false;
					this.activateMapAndInventory();
					Time.timeScale = 1;
					music.Play_Button_Click();
				}
			} else {
				Graphics.DrawTexture (continue_box, this.continueTexture);
				if(hoveredButton == continue_box) hoveredButton = new Rect();
			}
			
			// ACTION RESTART BUTTON
			if (reset_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (reset_box, this.hoverResetTexture);
				if (hoveredButton != reset_box) {
					music.Play_Button_Hover ();
					hoveredButton = reset_box;
				}	
				if (Input.GetMouseButtonDown (0)) {
					Time.timeScale = 1;
					music.Play_Button_Click();

					if(level == 3)
						this.save.saveTimePlayed(GameEngineLevel01.getTimePlay());
					else if(level == 4)
						this.save.saveTimePlayed(GameEngineLevel02_new.getTimePlay());

					Application.LoadLevel (PlayerPrefs.GetInt("Level"));	
				}
			} else {
				Graphics.DrawTexture (reset_box, this.resetTexture);
				if(hoveredButton == reset_box) hoveredButton = new Rect();
			}

			// ACTION OPTION BUTTON
			if (control_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (control_box, this.hoverControlTexture);
				if (hoveredButton != control_box) {
					music.Play_Button_Hover ();
					hoveredButton = control_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					this.keyword = true;
					music.Play_Button_Click();
				}
			}
			else {
				Graphics.DrawTexture (control_box, this.controlTexture);
				if(hoveredButton == control_box) hoveredButton = new Rect();
			}

			// ACTION OPTION BUTTON
			if (option_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (option_box, this.hoverOptionTexture);
				if (hoveredButton != option_box) {
					music.Play_Button_Hover ();
					hoveredButton = option_box;
				}
				if (Input.GetMouseButtonDown (0)) {
					this.option = true;
					music.Play_Button_Click();
				}
			}
			else {
				Graphics.DrawTexture (option_box, this.optionTexture);
				if(hoveredButton == option_box) hoveredButton = new Rect();
			}
			
			// ACTION EXIT BUTTON
			if (exit_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (exit_box, this.hoverExitTexture);
				if (hoveredButton != exit_box) {
					music.Play_Button_Hover ();
					hoveredButton = exit_box;
				}	
				if (Input.GetMouseButtonDown (0)) {
					confirm = true;
					music.Play_Button_Click();
					//this.confirmMenu(pause);
					//Time.timeScale = 1;
					//Application.LoadLevel (1);	
				}
			} else {
				Graphics.DrawTexture (exit_box, this.exitTexture);
				if(hoveredButton == exit_box) hoveredButton = new Rect();
			}
		}
		return pause;
	}
	
	
	// CONFIRM MENU
	public void confirmMenu(bool pause){
		
		//Debug.Log ("Pause: " + pause + " Confirm: " + confirm);
		if (pause && this.confirm) {
			// CONFIRM INTERFACE
			Rect confirm_box = new Rect ((Screen.width / 2) - (this.resizeTextureWidth (this.confirmTexture)*1.25f / 2),
			                             (Screen.height / 2) - (this.resizeTextureHeight (this.confirmTexture)*1.25f / 2),
			                             this.resizeTextureWidth (this.confirmTexture) * 1.25f,
			                             this.resizeTextureHeight (this.confirmTexture) * 1.25f);
			Graphics.DrawTexture (confirm_box, this.confirmTexture);
			
			// ============== CONFIRM MENU BUTTONS ===================
			
			// BUTTON YES
			Rect yes_box = new Rect (confirm_box.center.x - (confirm_box.width/3),
			                         confirm_box.center.y,
			                         this.resizeTextureWidth(this.yesTexture),
			                         this.resizeTextureHeight(this.yesTexture));
			Graphics.DrawTexture (yes_box, this.yesTexture);
			
			// BUTTON NO
			Rect no_box = new Rect (confirm_box.center.x + (confirm_box.width/4),
			                        confirm_box.center.y,
			                        this.resizeTextureWidth(this.noTexture),
			                        this.resizeTextureHeight(this.noTexture));
			Graphics.DrawTexture (no_box, this.noTexture);
			
			
			// ============== ACTION BUTTONS ===================
			
			// ACTION YES BUTTON
			if (yes_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (yes_box, this.hoverYesTexture);
				if (hoveredButton != yes_box) {
					music.Play_Button_Hover ();
					hoveredButton = yes_box;
				}	
				if (Input.GetMouseButtonDown (0)) { 
					Time.timeScale = 1;
					music.Play_Button_Click();
					if(level == 3)
						this.save.saveTimePlayed(GameEngineLevel01.getTimePlay());
					else if(level == 4)
						this.save.saveTimePlayed(GameEngineLevel02_new.getTimePlay());
					Application.LoadLevel (1);	
				}
			} else {
				Graphics.DrawTexture (yes_box, this.yesTexture);
				if(hoveredButton == yes_box) hoveredButton = new Rect();
			}
			
			// ACTION NO BUTTON
			if (no_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (no_box, this.hoverNoTexture);
				if (hoveredButton != no_box) {
					music.Play_Button_Hover ();
					hoveredButton = no_box;
				}	
				if (Input.GetMouseButtonDown (0)) { 
					this.confirm = false;
					music.Play_Button_Click();
				}
			} else {
				Graphics.DrawTexture (no_box, this.noTexture);
				if(hoveredButton == no_box) hoveredButton = new Rect();
			}
		}/* else
			this.confirm = false;*/
	}

	// CONFIRM MENU
	public void optionKeyword(bool pause){
					
		if (pause && this.keyword) {
			// KEYWORD INTERFACE
			Rect keyword_box = new Rect ((Screen.width / 2) - this.resizeTextureWidth (this.keywordTexture)*.375f,
			                             (Screen.height / 2.25f) - this.resizeTextureHeight (this.keywordTexture)*.375f,
			                             this.resizeTextureWidth (this.keywordTexture)*.75f,
			                             this.resizeTextureHeight (this.keywordTexture)*.75f);


			Graphics.DrawTexture (keyword_box, this.keywordTexture);

			// ACTION EXIT
			if (Input.GetMouseButtonDown (0)) { 
				if (!keyword_box.Contains (Event.current.mousePosition)) {
					this.keyword = false;
				}
			}
		}
	}

	public float x1 = 1f;
	public float y1 = 1f;
	public float x2 = 1f;
	public float y2 = 1f;

	// Option MENU
	public void showOptionMenu(bool pause){
		
		if (pause && this.option) {

			delay--;

			if(delay <= 0)
				delay = 0;

			// KEYWORD INTERFACE
			Rect option_box = new Rect ((Screen.width / 2) - (this.resizeTextureWidth (this.confirmTexture)*1.25f / 2),
			                             (Screen.height / 2) - (this.resizeTextureHeight (this.confirmTexture)*1.25f / 2),
			                             this.resizeTextureWidth (this.confirmTexture) * 1.25f,
			                             this.resizeTextureHeight (this.confirmTexture) * 1.25f);
			
			
			Graphics.DrawTexture (option_box, this.backgroundOptionTexture);


			Rect sound_box = new Rect (option_box.x * 2.15f,
			                           option_box.y * 1.42f,
			                           this.resizeTextureWidth (this.unCheckTexture)/ 10f,
			                           this.resizeTextureHeight (this.unCheckTexture)/ 12f);

			if(!sound)
				Graphics.DrawTexture (sound_box, this.unCheckTexture);
			else if(sound)
				Graphics.DrawTexture (sound_box, this.checkTexture);


			Rect light_box = new Rect (option_box.x * 2.15f,
			                           option_box.y * 1.75f,
			                           this.resizeTextureWidth (this.unCheckTexture)/ 10f,
			                           this.resizeTextureHeight (this.unCheckTexture)/ 12f);


			if(!light)
				Graphics.DrawTexture (light_box, this.unCheckTexture);
			else if(light)
				Graphics.DrawTexture (light_box, this.checkTexture);

			// ACTION EXIT
			if (Input.GetMouseButtonDown (0)) { 
				if (!option_box.Contains (Event.current.mousePosition)) {
					this.option = false;
				}

				if (sound_box.Contains (Event.current.mousePosition) && delay <= 0) {

					if(!this.sound)
						this.sound = true;
					else if(this.sound)
						this.sound = false;

					if(audio != null)
						this.audio.SetActive(sound);
					//AudioListener audio = GameObject.FindObjectOfType<AudioListener>();
					//audio.enabled = sound;
					delay = 5;
				}

				if (light_box.Contains (Event.current.mousePosition) && delay <= 0) {

					if(!this.light)
						this.light = true;
					else if(this.light)
						this.light = false;

					if(shadow != null)
						this.shadow.SetActive(light);

					delay = 5;
				}
			}
		}
	}

	public void setMapAndInventory(bool minimap, bool inventory){
			
		this.minimap = minimap;
		this.inventory = inventory;

		if(map != null)
			map.setShowMiniMap(false);

		if(map2 != null)
			map2.setShowMiniMap(false);

		invent.setShowInventory(false);

	}

	public void activateMapAndInventory(){

		this.confirm = false;
		this.keyword = false;
		this.option = false;

		if(map != null){
			map.setShowMiniMap(minimap);
			map.setPause(false);
		}
		if(map2 != null){
			map2.setShowMiniMap(minimap);
			map2.setPause(false);
		}

		invent.setShowInventory(inventory);
		invent.setPause(false);

	}

	public void setConfirm(bool confirm){
		this.confirm = confirm;
	}

	public void setKeyword(bool keyword){
		this.keyword = keyword;
	}

	public void setOption(bool option){
		this.option = option;
	}
	
	
	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
	
}