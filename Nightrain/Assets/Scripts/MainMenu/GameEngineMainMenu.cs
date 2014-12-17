using UnityEngine;
using System.Collections;

public class GameEngineMainMenu : MonoBehaviour {

	private MainMenuGUI gui;

	// LOAD ALL THE RESOURCE TO START
	void Start () {

		this.gui = new MainMenuGUI ();
		this.gui.setAudioClick (this.audio);
		this.gui.initResources ();
	
	}
	
	// PAINT THE MAINMENU SCENE
	void OnGUI(){
		this.gui.mainMenu ();
		this.gui.characterSelector ();
		this.gui.difficultySelector ();
		this.gui.loadSaveGame ();
		this.gui.showControls ();
	}
}
