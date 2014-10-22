using UnityEngine;
using System.Collections;

public class GameEngineGameOver : MonoBehaviour {

	private Texture2D BacgroundTexture;
	private Texture2D buttonContinue;
	private Texture2D buttonContinue2;
	private Texture2D buttonExit;
	private Texture2D buttonExit2;
	
	// Use this for initialization
	void Start () {

		// ADD TEXTURES
		this.BacgroundTexture = Resources.Load<Texture2D>("GameOver/Misc/fondo");

		this.buttonContinue = Resources.Load<Texture2D>("GameOver/OptionMenu/continuarpausa");
		this.buttonContinue2 = Resources.Load<Texture2D>("GameOver/OptionMenu/continuarpausa2");

		this.buttonExit = Resources.Load<Texture2D>("GameOver/OptionMenu/salirpausa");
		this.buttonExit2 = Resources.Load<Texture2D>("GameOver/OptionMenu/salirpausa2");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){

		int sizeXImage = 256;
		int sizeYImage = 64;

		Rect background_box = new Rect (0, 0, Screen.width, Screen.height);
		Graphics.DrawTexture (background_box, this.BacgroundTexture);

		Rect button1_Continue = new Rect ((Screen.width / 2) - sizeXImage/2, Screen.height / 1.5f, 
                                 sizeXImage, sizeYImage);
		Graphics.DrawTexture (button1_Continue, this.buttonContinue);

		Rect button2_Exit = new Rect ((Screen.width / 2) - sizeXImage/2, Screen.height / 1.2f, 
		                              this.buttonExit.width, this.buttonExit.height);
		Graphics.DrawTexture (button2_Exit, this.buttonExit);


		if (button1_Continue.Contains (Event.current.mousePosition)) {
			Graphics.DrawTexture (button1_Continue, this.buttonContinue2);
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel(2);
			}
		} else if(button2_Exit.Contains (Event.current.mousePosition)){
			Graphics.DrawTexture (button2_Exit, this.buttonExit2);
			if(Input.GetMouseButtonDown(0)){
				Application.LoadLevel(1);
			}
		}
	}

}
