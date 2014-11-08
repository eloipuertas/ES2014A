using UnityEngine;
using System.Collections;

public class GameEngineLevel01 : MonoBehaviour {
	
	private PauseMenuGUI gui;

	private RaycastHit getObjectScene;
	private bool pause = false;

	// --- CHARACTER ---
	private Transform prefab;
	private GameObject character;
	
	// --- LIGHT
	public GameObject ambientLight;
	private CharacterScript cs;
	private Color c;
	
	// --- NPCs ---
	private string[] nameNPC = {"Enemy"};
	private GameObject npc;
	private Movement ms;
	

	// Use this for initialization
	void Start () {

		// --- LOAD RESOURCES TO CHARACTER ---
		this.prefab = Resources.Load<Transform>("Prefabs/MainCharacters/" + PlayerPrefs.GetString("Character"));
		Instantiate (prefab);
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();

		// --- LOAD RESOURCES TO MENU ---
		gui = new PauseMenuGUI ();
		gui.initResources ();


		this.c = this.ambientLight.light.color;

		print ("Personaje:" + PlayerPrefs.GetString ("Character") + " Difficulty: " + PlayerPrefs.GetString ("Difficulty"));

	}
	
	// Update is called once per frame
	void Update () {
		isAllEnemysDead ();

		this.PauseScreen ();
		this.StateMachine ();
	}


	void StateMachine(){
	
		if (Input.GetKeyDown (KeyCode.Escape) && !this.pause) {
			this.pause = true;
			Time.timeScale = 0;
		} else if (Input.GetKeyDown (KeyCode.Escape) && this.pause) {
			this.pause = false;
			Time.timeScale = 1;
		}

		this.isAlive ();
	}

	//Comprueba si el personaje sigue vivo
	void isAlive(){
		int num = this.character.GetComponent<CharacterScript> ().getHealth();
		//If the character is dead we show "game over" scene
		if(num <= 0) Application.LoadLevel(3);
	}
	
	//Comprueba si los enemigos de la lista estan muertos
	void isAllEnemysDead(){
		foreach (string item in nameNPC) {
			this.npc = GameObject.FindGameObjectWithTag (item);

			if(this.npc != null){
				this.ms = this.npc.GetComponent<Movement> ();

				if(ms.getHealth() <= 0.0f){
					Destroy(npc);
					npc = null;
				}
			}
			if(npc != null){
				print ("No nulo");
				break;
			}
		}
		if (this.npc == null) {
			Destroy(GameObject.FindGameObjectWithTag("FireWall"));
			//Application.LoadLevel(4);
		}
	}
	
	void PauseScreen(){
		
		if (this.pause && !this.cs.isCritical ())
			this.ambientLight.light.color = new Color (.2f, .2f, .2f);
		else if (!this.pause && !this.cs.isCritical ())
			this.ambientLight.light.color = new Color (1.0f, 1.0f, 1.0f);
		else if (this.pause && this.cs.isCritical ())
			this.ambientLight.light.color = new Color (.5f, .25f, .5f);
		else if (!this.pause && this.cs.isCritical ())
			this.CautionScreen ();
	}


	// Efecto critico con luz roja
	void CautionScreen(){

		if (this.cs.isCritical()) {
			this.c.r = 1.0f;
			if(this.c.g >= 0.5f)
				this.c.g -= 0.02f;
			if(this.c.b >= 0.5f)
				this.c.b -= 0.02f;
			this.ambientLight.light.color = this.c;
		} else {
			this.c.r = 1.0f;
			if(this.c.g <= 1.0f)
				this.c.g += 0.02f;
			if(this.c.b <= 1.0f)
				this.c.b += 0.02f;
			this.ambientLight.light.color = this.c;
		}

	}

	void OnGUI(){	
		if (this.pause) 
			this.pause = this.gui.pauseMenu (this.pause);

		this.gui.confirmMenu(this.pause);
	}
}
