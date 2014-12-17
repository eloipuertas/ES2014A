using UnityEngine;
using System.Collections;

public class GameEngineLevel01 : MonoBehaviour {
	
	private PauseMenuGUI gui;
	
	private RaycastHit getObjectScene;
	private static bool pause = false;
	private bool minimap = false;
	private bool inventory = false;
	
	// --- CHARACTER ---
	private Transform prefab;
	private GameObject character;
	
	// --- LIGHT
	public GameObject ambientLight;
	private CharacterScript cs;
	private ClickToMove cm;
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

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;

	// --- Delays ---
	public float delay = 2;
	public float delay_death = 3f;
	private bool anim_death = false;

	// Time Played
	private static float time_play = 0;

	// Music
	private Music_Engine_Script music;

	private InventoryScript invent;
	private miniMapLv1 map;
	
	// Use this for initialization
	void Awake () {
		
		// --- LOAD RESOURCES TO CHARACTER ---
		this.prefab = Resources.Load<Transform>("Prefabs/MainCharacters/" + PlayerPrefs.GetString("Player"));
		Instantiate (prefab);
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.invent = this.character.GetComponentInChildren <InventoryScript> ();
		this.map = GameObject.FindGameObjectWithTag ("Minimap").GetComponent<miniMapLv1> ();
		this.cm = this.character.GetComponent<ClickToMove> ();
		this.cs = this.character.GetComponent<CharacterScript> ();

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData();
		this.load = this.mc.loadData();
		time_play = this.load.loadTimePlayed (); 

		this.npc_boss = GameObject.FindGameObjectWithTag("Boss");
		this.npc_enemy = GameObject.FindGameObjectsWithTag("Enemy");

		this.camera1 = GameObject.FindGameObjectWithTag ("MainCamera");
		this.camera1.SetActive (true);
		
		this.camera2 = GameObject.FindGameObjectWithTag ("CameraGoal");
		this.camera2.SetActive (false);

		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();

		// --- LOAD RESOURCES TO MENU ---
		gui = new PauseMenuGUI ();
		gui.initResources ();
		
		
		this.c = this.ambientLight.light.color;
		pause = false;

	}
	
	// Update is called once per frame
	void Update () {
		if (!allIsDead) {
			isAllEnemysDead ();
		} 
		this.PauseScreen ();
		this.StateMachine ();
		time_play += Time.deltaTime;
	}
	
	
	void StateMachine(){

		if (Input.GetKeyDown (KeyCode.Escape) && !pause) {
			pause = true;
			minimap = map.showMiniMap();
			inventory = invent.showInventory();
			map.setShowMiniMap(false);
			map.setPause(pause);
			invent.setShowInventory(false);
			invent.setPause(pause);
			this.gui.setMapAndInventory(minimap, inventory);
			Time.timeScale = 0;
		} else if (Input.GetKeyDown (KeyCode.Escape) && pause) {
			pause = false;
			this.gui.setConfirm(false);
			this.gui.setKeyword(false);
			this.gui.setOption(false);
			map.setShowMiniMap(minimap);
			map.setPause(pause);
			invent.setShowInventory(inventory);
			invent.setPause(pause);
			Time.timeScale = 1;
		}
		
		this.isAlive ();
	}
	
	//Comprueba si el personaje sigue vivo
	void isAlive(){
		int num = this.character.GetComponent<CharacterScript> ().getHealth();
		//If the character is dead we show "game over" scene
		if (num <= 0) {
			delay_death -= Time.deltaTime;
			if(!anim_death){
				this.save.saveTimePlayed(time_play);
				this.cm.death();
				this.music.play_Player_Die();
				anim_death = true;
			}
			if(delay_death < 0)
				Application.LoadLevel (6);
		}
	}
	
	//Comprueba si los enemigos de la lista estan muertos
	void isAllEnemysDead(){
		
		//Contador para saber el numero de enemigos muertos.
		int cont = 0;
		if(this.npc_enemy != null && this.npc_boss != null && this.allDead == false){
			if(this.npc_boss.tag == "Boss"){
				this.boss = this.npc_boss.GetComponent<Movement> ();
				if(boss.getAttributes().getHealth() <= 0.0f){
					//Destroy(npc_boss);
					npc_boss = null;
				}
			}
			
			///Miramos los NPC
			for(int i = 0; i < npc_enemy.Length; i++){
				if(this.npc_enemy[i] != null && this.npc_enemy[i].tag == "Enemy"){
					this.enemy = this.npc_enemy[i].GetComponent<Movement_graveler> ();
					if(this.enemy != null && enemy.getAttributes().getHealth() <= 0.0f){
						//Destroy(npc_enemy[i]);
						npc_enemy[i] = null;
						cont += 1;
					}
				} else {
					cont += 1;
				}
			}
			
			if(cont == npc_enemy.Length){
				Destroy(GameObject.FindGameObjectWithTag("FireWall_bridge"));
			}
		}
		
		if (this.npc_boss == null) {
			
			delay -= 1 * Time.deltaTime;
			
			if(delay <= 0){
				allIsDead = true;
				this.camera2.SetActive(true);
			}
		}
	}

	
	void PauseScreen(){
		
		if (pause && !this.cs.isCritical ())
			this.ambientLight.light.color = new Color (.2f, .2f, .2f);
		else if (!pause && !this.cs.isCritical ())
			this.ambientLight.light.color = new Color (1.0f, 1.0f, 1.0f);
		else if (pause && this.cs.isCritical ())
			this.ambientLight.light.color = new Color (.5f, .25f, .5f);
		else if (!pause && this.cs.isCritical ())
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

	/*public static bool isPausedGame(){
		return pause;
	}*/
	
	void OnGUI(){	
		if (pause) 
			pause = this.gui.pauseMenu (pause);
		
		this.gui.confirmMenu(pause);
		this.gui.optionKeyword (pause);
		this.gui.showOptionMenu (pause);
	}

	public static float getTimePlay(){
		return time_play;
	}
}
