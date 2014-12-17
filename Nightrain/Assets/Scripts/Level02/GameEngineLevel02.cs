using UnityEngine;
using System.Collections;

public class GameEngineLevel02 : MonoBehaviour {

	private PauseMenuGUI gui;
	private bool pause = false;

	// Use this for initialization
	void Start () {

		this.gui = new PauseMenuGUI ();
		this.gui.initResources ();
	
	}
	
	// Update is called once per frame
	void Update () {
		this.StateMachine ();
	}
	
	
	void StateMachine(){
		
		/*if (Input.GetKeyDown (KeyCode.Escape) && !this.pause) {
			this.pause = true;
			Time.timeScale = 0;
		} else if (Input.GetKeyDown (KeyCode.Escape) && this.pause) {
			this.pause = false;
			Time.timeScale = 1;
		}*/

	}


	/*void OnGUI(){	
		if (this.pause) 
			this.pause = this.gui.pauseMenu (this.pause);
		
		this.gui.confirmMenu(this.pause);
		this.gui.optionKeyword (this.pause);
		
	}*/
	
}
