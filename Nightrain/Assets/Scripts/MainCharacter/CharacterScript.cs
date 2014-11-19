using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	// ATRIBUTES CHARACTER

	// === HEALTH ===
	private int bar_health;		// Value of initial HEALTH
	public float max_health;	// Value of max HEALTH on the GAME. This value basically is to resize the health bar.

	// === MAGIC ===
	private int bar_magic;		// Value of initial MAGIC
	public float max_magic;		// Value of max MAGIC on the GAME. This value basically is to resize the magic bar.


	// === ATTRIBUTES ===
	private int strength;
	private int defense;
	private int speed;


	// ADD MORE ATTRIBUTES OF CHARACTER
	private bool critical = false;

	// =========================
	private GameObject NPCs;

	// MusicEngine GameObject that plays audio effects
	private Music_Engine_Script music;

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;


	// Use this for initialization
	void Awake () {
		
		// ADD COMPONENT
		// Buscamos al personaje principal
		this.NPCs = GameObject.FindGameObjectWithTag("Enemy");

		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData();
		this.load = this.mc.loadData();

		// LOAD ATTRIBUTES
		this.bar_health = this.load.loadVIT ();
		this.bar_magic = this.load.loadPM ();
		this.strength = this.load.loadSTR ();
		this.defense = this.load.loadDEF ();
		this.speed = this.load.loadSPD ();

		Debug.Log ("Attributes: VIT:" + this.bar_health + " PM:" + this.bar_magic + " STR:" + this.strength + " DEF:" + this.defense + " SPD:" + this.speed);

	}
	
	// Update is called once per frame.
	void Update () {
	
	}


	// ========================= COLISION CON NPC ==================================
	
	void OnTriggerEnter (Collider other){
		int damage = Random.Range (6, this.strength+1);
		if(other.gameObject == this.NPCs){
			this.NPCs.GetComponent<Movement>().setDamage(damage);
			print("Daño: " + damage); 
		}	
	}
	
	// ==============================================================================


	// === METHODS GET/SET ATTRIBUTES ===

	// Get the actual value of health.
	public int getHealth(){
		return this.bar_health;
	}
	
	// Set actual health value.
	public void setHealth(int health){
		this.max_health = health;
	}
	
	// Get the max value of health in the game.
	public float getMaxHealth(){
		return this.max_health;
	}
	
	// Set the max health value.
	public void setMaxHealth(){
		this.max_health = this.bar_health;
	}
	
	// Get the actual value of health.
	public int getMagic(){
		return this.bar_magic;
	}
	
	// Set the magic value.
	public void setMagic(int magic){
		this.max_magic = magic;
	}
	
	// Get the max value of health in the game.
	public float getMaxMagic(){
		return this.max_magic;
	}
	
	// Set the max magic value.
	public void setMaxMagic(){
		this.max_magic = this.bar_magic;
	}

	// Get the actual value of strength.
	public int getFRZ(){
		return this.strength;
	}
	
	// Set actual strength value.
	public void setFRZ(int FRZ){
		this.strength = FRZ;
	}

	// Get the actual value of defense.
	public int getDEF(){
		return this.defense;
	}
	
	// Set actual defense value.
	public void setDEF(int DEF){
		this.defense = DEF;
	}

	// Get the actual value of speed.
	public int getSPD(){
		return this.speed;
	}
	
	// Set actual speed value.
	public void setSPD(int SPD){
		this.speed = SPD;
	}


	// ==============================================================================

	// === METHODS ===
	
	// Get is the 'Character' has few health.
	public bool isCritical(){
		return this.critical;
	}
	
	// Set the critical state of 'Character'.
	public void setCritical(bool critical){
		this.critical = critical;
	}
	
	// Method to Cure the 'Character'.
	public void setCure(int heal){
		if (this.bar_health < this.max_health) {
			this.bar_health += heal;
			if(this.bar_health > this.max_health)
				this.bar_health = Mathf.FloorToInt(this.max_health);
		}
	}
	
	// Method to Damage the 'Character'
	public void setDamage(int damage){
		this.bar_health -= damage;
		// Reproducimos un sonido de dolor del personaje al recibir el golpe
		if (music != null) music.play_Player_Hurt ();
	}



}
