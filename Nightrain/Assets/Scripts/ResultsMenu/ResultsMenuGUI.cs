using UnityEngine;
using System.Collections;

public class ResultsMenuGUI : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;
	
	// ========== TEXTURES ============
	private Texture2D backgroundTexture;
	private Texture2D mainMenuTexture;
	private Texture2D hoverMainMenuTexture;
	private Texture2D nextLevelTexture;
	private Texture2D hoverNextLevelTexture;
	
	
	// CONSTRUCTOR
	public ResultsMenuGUI(){}
	
	// LOAD TEXTURE RESOURCES
	public void initResources () {
		this.backgroundTexture = Resources.Load<Texture2D>("ResultsStage/background_results_" + PlayerPrefs.GetString("Player"));
		
		this.mainMenuTexture = Resources.Load<Texture2D>("ResultsStage/backmenu_button");
		this.hoverMainMenuTexture = Resources.Load<Texture2D>("ResultsStage/backmenu_button_hover");
		
		this.nextLevelTexture = Resources.Load<Texture2D>("ResultsStage/nextlvl_button");
		this.hoverNextLevelTexture = Resources.Load<Texture2D>("ResultsStage/nextlvl_button_hover");
	}
	
	// MENU RESULTS
	public void menuResults(){
		
		Rect background_box = new Rect (0, 0, Screen.width, Screen.height);
		Graphics.DrawTexture (background_box, this.backgroundTexture);
		
		// ===================== BOTONERA UNO ENCIMA DE OTRO =============================
		
		/*Rect continue_box = new Rect ((Screen.width/2) - (this.resizeTextureWidth(this.mainMenuTexture)/2), 
		                              Screen.height - (Screen.height/2.75f), 
		                              this.resizeTextureWidth(this.mainMenuTexture), 
		                              this.resizeTextureHeight(this.mainMenuTexture));
		Graphics.DrawTexture (continue_box, this.mainMenuTexture);
		
		Rect exit_box = new Rect ((Screen.width/2) - (this.resizeTextureWidth(this.nextLevelTexture)/2), 
		                          Screen.height - ((Screen.height/2.75f) - this.resizeTextureHeight(this.mainMenuTexture)*1.25f), 
		                          this.resizeTextureWidth(this.nextLevelTexture), 
		                          this.resizeTextureHeight(this.nextLevelTexture));
		Graphics.DrawTexture (exit_box, this.nextLevelTexture);*/
		
		// ===============================================================================
		
		
		// ===================== BOTONERA UNO AL LADO DE OTRO =============================
		
		Rect continue_box = new Rect (Screen.width/3.5f, 
		                              Screen.height - (Screen.height/4.75f), 
		                              this.resizeTextureWidth(this.mainMenuTexture) / 2.75f, 
		                              this.resizeTextureHeight(this.mainMenuTexture) / 1.5f);
		Graphics.DrawTexture (continue_box, this.mainMenuTexture);

		Rect exit_box = new Rect (Screen.width/1.9f, 
		                          Screen.height - (Screen.height/4.75f), 
		                          this.resizeTextureWidth(this.nextLevelTexture) / 2.75f, 
		                          this.resizeTextureHeight(this.nextLevelTexture) / 1.5f);
		Graphics.DrawTexture (exit_box, this.nextLevelTexture);
		
		// ===============================================================================
		
		if (continue_box.Contains (Event.current.mousePosition)) {
			Graphics.DrawTexture (continue_box, this.hoverMainMenuTexture);
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel(1);
			}
		} else if(exit_box.Contains (Event.current.mousePosition)){
			Graphics.DrawTexture (exit_box, this.hoverNextLevelTexture);
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel(4);
			}
		}
		
	}
	
	
	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
}
