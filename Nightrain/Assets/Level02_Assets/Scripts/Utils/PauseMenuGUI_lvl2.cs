using UnityEngine;
using System.Collections;

public class PauseMenuGUI_lvl2 {
	
	private const int reference_width = 1366; 
	private const int reference_height = 598;
	
	// ====== TEXTURES PAUSE MENU ======
	private Texture2D backgroundTexture;
	private Texture2D continueTexture;
	private Texture2D hoverContinueTexture;
	private Texture2D resetTexture;
	private Texture2D hoverResetTexture;
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
	
	
	// SHOW MENU CONFIRM
	private bool confirm = false;
	
	// Buttons sound effects
	private Rect hoveredButton = new Rect();
	private Music_Engine_Script music;
	
	// CONSTRUCTOR
	public PauseMenuGUI_lvl2(){}
	
	// LOAD TEXTURE RESOURCES
	public void initResources () {
		
		// MENU PAUSE
		this.backgroundTexture = Resources.Load<Texture2D>("PauseMenu/background_pause");
		
		this.continueTexture = Resources.Load<Texture2D>("PauseMenu/continue");
		this.hoverContinueTexture = Resources.Load<Texture2D>("PauseMenu/hover_continue");
		
		this.resetTexture = Resources.Load<Texture2D>("PauseMenu/reset");
		this.hoverResetTexture = Resources.Load<Texture2D>("PauseMenu/hover_reset");
		
		this.optionTexture = Resources.Load<Texture2D>("PauseMenu/option");
		this.hoverOptionTexture = Resources.Load<Texture2D>("PauseMenu/hover_option");
		
		this.exitTexture = Resources.Load<Texture2D>("PauseMenu/exit");
		this.hoverExitTexture = Resources.Load<Texture2D>("PauseMenu/hover_exit");
		
		// MENU CONFIRM
		this.confirmTexture = Resources.Load<Texture2D>("PauseMenu/background_confirm");
		
		this.yesTexture = Resources.Load<Texture2D>("PauseMenu/yes");
		this.hoverYesTexture = Resources.Load<Texture2D>("PauseMenu/hover_yes");
		
		this.noTexture = Resources.Load<Texture2D>("PauseMenu/no");
		this.hoverNoTexture = Resources.Load<Texture2D>("PauseMenu/hover_no");
		
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
		
	}
	
	// PAUSE MENU
	public bool pauseMenu(bool pause){
		
		if (!this.confirm) {
			
			// PAUSE INTERFACE
			Rect menu_box = new Rect ((Screen.width / 2) - (this.resizeTextureWidth(this.backgroundTexture) / 2),
			                          (Screen.height / 2) - (this.resizeTextureHeight(this.backgroundTexture) / 2.6f),
			                          this.resizeTextureWidth(this.backgroundTexture),
			                          this.resizeTextureHeight(this.backgroundTexture) / 1.5f);
			Graphics.DrawTexture (menu_box, this.backgroundTexture);
			
			// ============== PAUSE MENU BUTTONS ===================
			
			// BUTTON CONTINUE
			Rect continue_box = new Rect (menu_box.center.x - (this.resizeTextureWidth(this.continueTexture) / 2),
			                              menu_box.center.y - (this.resizeTextureHeight(this.continueTexture) * 1.4f),
			                              this.resizeTextureWidth(this.continueTexture),
			                              this.resizeTextureHeight(this.continueTexture) / 1.25f);
			Graphics.DrawTexture (continue_box, this.continueTexture);
			
			
			// BUTTON RESTART
			Rect reset_box = new Rect (continue_box.position.x,
			                           continue_box.position.y + (this.resizeTextureHeight(this.resetTexture) / 1.1f),
			                           this.resizeTextureWidth(this.resetTexture),
			                           this.resizeTextureHeight(this.resetTexture) / 1.25f);
			Graphics.DrawTexture (reset_box, this.resetTexture);
			
			
			// BUTTON OPTION
			Rect option_box = new Rect (reset_box.position.x,
			                            reset_box.position.y + (this.resizeTextureHeight(this.optionTexture) / 1.1f),
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
					Application.LoadLevel (4);	
				}
			} else {
				Graphics.DrawTexture (reset_box, this.resetTexture);
				if(hoveredButton == reset_box) hoveredButton = new Rect();
			}
			
			// ACTION OPTION BUTTON
			if (option_box.Contains (Event.current.mousePosition)) {
				Graphics.DrawTexture (option_box, this.hoverOptionTexture);
				if (hoveredButton != option_box) {
					music.Play_Button_Hover ();
					hoveredButton = option_box;
				}
				if (Input.GetMouseButtonDown (0)) {
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
			Rect yes_box = new Rect (confirm_box.center.x - (confirm_box.width/6),
			                         confirm_box.center.y,
			                         this.resizeTextureWidth(this.yesTexture),
			                         this.resizeTextureHeight(this.yesTexture));
			Graphics.DrawTexture (yes_box, this.yesTexture);
			
			// BUTTON NO
			Rect no_box = new Rect (confirm_box.center.x + (confirm_box.width/10),
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
		} else
			this.confirm = false;
	}
	
	
	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
	
}