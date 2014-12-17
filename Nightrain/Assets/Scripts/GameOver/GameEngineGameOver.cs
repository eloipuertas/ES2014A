
using UnityEngine;
using System.Collections;

public class GameEngineGameOver : MonoBehaviour {

	private GameOverGUI gui;
	
	// Use this for initialization
	void Start () {
		this.gui = new GameOverGUI ();
		this.gui.setAudioClick (this.audio);
		this.gui.initResources ();
	}


	void OnGUI(){
		this.gui.menuGameOver ();
	}

}
