using UnityEngine;
using System.Collections;

public class GameEngineResultsMenu : MonoBehaviour {

	private ResultsMenuGUI gui;

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;
	
	// Use this for initialization
	void Start () {

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData ();
		this.load = this.mc.loadData ();

		this.save.saveTimePlayed (GameEngineLevel01.getTimePlay());
		this.save.saveLevel (4);
		this.gui = new ResultsMenuGUI ();
		this.gui.initResources ();
	}
	
	
	void OnGUI(){
		this.gui.menuResults ();
	}
}
