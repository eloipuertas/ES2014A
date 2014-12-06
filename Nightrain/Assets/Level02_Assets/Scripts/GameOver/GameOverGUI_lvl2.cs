using UnityEngine;
using System.Collections;

public class GameOverGUI_lvl2{

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	// ========== TEXTURES ============
	private Texture2D backgroundTexture;
	private Texture2D continueTexture;
	private Texture2D hoverContinueTexture;
	private Texture2D exitTexture;
	private Texture2D hoverExitTexture;


	// CONSTRUCTOR
	public GameOverGUI_lvl2(){}

	// LOAD TEXTURE RESOURCES
	public void initResources () {
		this.backgroundTexture = Resources.Load<Texture2D>("GameOver/background_gameover");
		
		this.continueTexture = Resources.Load<Texture2D>("GameOver/continue");
		this.hoverContinueTexture = Resources.Load<Texture2D>("GameOver/hover_continue");
		
		this.exitTexture = Resources.Load<Texture2D>("GameOver/exit");
		this.hoverExitTexture = Resources.Load<Texture2D>("GameOver/hover_exit");
	}

	// MENU GAMEOVER
	public void menuGameOver(){

		Rect background_box = new Rect (0, 0, Screen.width, Screen.height);
		Graphics.DrawTexture (background_box, this.backgroundTexture);
		
		// ===================== BOTONERA UNO ENCIMA DE OTRO =============================
		
		Rect continue_box = new Rect ((Screen.width/2) - (this.resizeTextureWidth(this.continueTexture)/2), 
		                              Screen.height - (Screen.height/2.75f), 
		                              this.resizeTextureWidth(this.continueTexture), 
		                              this.resizeTextureHeight(this.continueTexture));
		Graphics.DrawTexture (continue_box, this.continueTexture);
		
		Rect exit_box = new Rect ((Screen.width/2) - (this.resizeTextureWidth(this.exitTexture)/2), 
		                          Screen.height - ((Screen.height/2.75f) - this.resizeTextureHeight(this.continueTexture)*1.25f), 
		                          this.resizeTextureWidth(this.exitTexture), 
		                          this.resizeTextureHeight(this.exitTexture));
		Graphics.DrawTexture (exit_box, this.exitTexture);
		
		// ===============================================================================
		
		
		// ===================== BOTONERA UNO AL LADO DE OTRO =============================
		
		/*Rect continue_box = new Rect (Screen.width/3.5f, 
		                              Screen.height - (Screen.height/2.75f), 
		                              this.resizeTextureWidth(this.continueTexture), 
		                              this.resizeTextureHeight(this.continueTexture));
		Graphics.DrawTexture (continue_box, this.continueTexture);

		Rect exit_box = new Rect (Screen.width/1.9f, 
		                          Screen.height - (Screen.height/2.75f), 
		                          this.resizeTextureWidth(this.exitTexture), 
		                          this.resizeTextureHeight(this.exitTexture));
		Graphics.DrawTexture (exit_box, this.exitTexture);*/
		
		// ===============================================================================
		
		if (continue_box.Contains (Event.current.mousePosition)) {
			Graphics.DrawTexture (continue_box, this.hoverContinueTexture);
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel(4);
			}
		} else if(exit_box.Contains (Event.current.mousePosition)){
			Graphics.DrawTexture (exit_box, this.hoverExitTexture);
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel(1);
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
