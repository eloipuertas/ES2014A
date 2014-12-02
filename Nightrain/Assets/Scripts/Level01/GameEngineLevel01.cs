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
	private string[] nameNPC = {"Enemy","Boss"};
	private GameObject[] npc_enemy;
	private GameObject npc_boss;
	private bool allDead = false;

	private Movement boss;
	private Movement_graveler enemy;

	private bool allIsDead = false;
	
	// --- Camaras ---
	private GameObject camera1;
	private GameObject camera2;

	// --- Delays ---
	public float delay = 2;
	
	// Use this for initialization
	void Awake () {
		
		// --- LOAD RESOURCES TO CHARACTER ---
		this.prefab = Resources.Load<Transform>("Prefabs/MainCharacters/" + PlayerPrefs.GetString("Character"));
		Instantiate (prefab);
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();

		this.npc_boss = GameObject.FindGameObjectWithTag("Boss");
		this.npc_enemy = GameObject.FindGameObjectsWithTag("Enemy");

		this.camera1 = GameObject.FindGameObjectWithTag ("MainCamera");
		this.camera1.SetActive (true);
		
		this.camera2 = GameObject.FindGameObjectWithTag ("CameraGoal");
		this.camera2.SetActive (false);
		
		// --- LOAD RESOURCES TO MENU ---
		gui = new PauseMenuGUI ();
		gui.initResources ();
		
		
		this.c = this.ambientLight.light.color;
		
		//print ("Personaje:" + PlayerPrefs.GetString ("Character") + " Difficulty: " + PlayerPrefs.GetString ("Difficulty"));
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!allIsDead) {
			isAllEnemysDead ();
		} 
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
		if(num <= 0) Application.LoadLevel(6);
	}
	
	//Comprueba si los enemigos de la lista estan muertos
	void isAllEnemysDead(){
		//foreach (GameObject item in npc) {
		//this.npc = GameObject.FindGameObjectWithTag (item);
		//print ("Name: " + this.npc.name);
		if(this.npc_enemy != null && this.npc_boss != null){

			if(this.npc_boss.tag == "Boss"){
				this.boss = this.npc_boss.GetComponent<Movement> ();
				if(boss.getAttributes().getHealth() <= 0.0f){
					//Destroy(npc_boss);
					npc_boss = null;
				}
			}

			if(!allDead){
				for(int i = 0; i < npc_enemy.Length; i++){
					if(this.npc_enemy[i] != null && this.npc_enemy[i].tag == "Enemy"){
						this.enemy = this.npc_enemy[i].GetComponent<Movement_graveler> ();
						if(enemy.getAttributes().getHealth() <= 0.0f){
							//Destroy(npc_enemy[i]);
							npc_enemy[i] = null;
							allDead = true;
						} else {
							allDead = false;
						}
					}
				}

				if(allDead){
					Destroy(GameObject.FindGameObjectWithTag("FireWall_bridge"));
				}
			}

		}
		/*if(npc_boss != null){
			break;
		}*/
		//}
		if (this.npc_boss == null) {

			delay -= 1 * Time.deltaTime;

			if(delay <= 0){
				allIsDead = true;
				this.camera2.SetActive(true);
			}
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
