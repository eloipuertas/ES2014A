
using UnityEngine;
using System.Collections;

public class GameEngineGameOver_lvl2 : MonoBehaviour {

	private GameOverGUI_lvl2 gui;
	
	// Use this for initialization
	void Start () {
		this.gui = new GameOverGUI_lvl2 ();
		this.gui.initResources ();
	}


	void OnGUI(){
		this.gui.menuGameOver ();
	}



}
