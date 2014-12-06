using UnityEngine;
using System.Collections;

public class GameEngineResultsMenu : MonoBehaviour {

	private ResultsMenuGUI gui;
	
	// Use this for initialization
	void Start () {
		this.gui = new ResultsMenuGUI ();
		this.gui.initResources ();
	}
	
	
	void OnGUI(){
		this.gui.menuGameOver ();
	}
}
