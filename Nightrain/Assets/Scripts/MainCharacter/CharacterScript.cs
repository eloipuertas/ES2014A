using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	// ATRIBUTES CHARACTER

	private int level;			// Value of level main character.

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


	// === EXPERIENCE ===
	private int experience;
	private int next;

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

	private GUIStyle text_style;
	private GUIStyle guiStyleBack;


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


	}

	void Start(){
		
		this.text_style = new GUIStyle ();
		this.text_style.normal.textColor = Color.black;
		this.text_style.fontSize = 15;
		//this.text_style.alignment = TextAnchor.UpperCenter ; 
		this.text_style.wordWrap = true;

		// LOAD ATTRIBUTES
		this.loadAttributes ();
		this.printAttributes ();
		this.calculateEXP ();
	}
	
	// Update is called once per frame.
	void Update () {

		if(this.level < 100)
			levelUP ();
	}


	// ========================= COLISION CON NPC ==================================
	
	void OnTriggerEnter (Collider other){
		int damage = Random.Range ((this.strength + 1) - (int)(4 + this.strength * 0.25f), this.strength+1);
		if(other.gameObject == this.NPCs){
			this.NPCs.GetComponent<Movement>().setDamage(damage);
			//print("Daño: " + damage); 
		}	
	}
	
	// ==============================================================================


	// === METHODS GET/SET ATTRIBUTES ===


	// Get the actual value of level.
	public int getLVL(){
		return this.level;
	}
	
	// Set actual level value.
	public void setLevel(int level){
		this.level += level;
		this.save.saveStatus("LVL", this.level);
		// CALL A PARAMETER UPDATE
	}

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

		this.strength += FRZ;

		if(this.strength > 255)
			this.strength = 255;
		else if(this.strength <= 0)
			this.strength = this.load.loadSTR();

		this.printAttributes ();
	}

	// Get the actual value of defense.
	public int getDEF(){
		return this.defense;
	}
	
	// Set actual defense value.
	public void setDEF(int DEF){

		this.defense += DEF;

		if(this.defense > 255)
			this.defense = 255;
		else if(this.defense <= 0)
			this.defense = this.load.loadDEF();

		this.printAttributes ();
	}

	// Get the actual value of speed.
	public int getSPD(){
		return this.speed;
	}
	
	// Set actual speed value.
	public void setSPD(int SPD){
		this.speed = SPD;
	}

	// Get the actual value of level.
	public int getEXP(){
		return this.experience;
	}
	
	// Set actual level value.
	public void setEXP(int experience){
		this.experience += experience;
		this.save.saveStatus("EXP", this.experience);
		this.levelUP ();

	}

	void printAttributes(){
		/*Debug.Log ("VIT:" + this.bar_health 
		           + " PM:" + this.bar_magic 
		           + " STR:" + this.strength 
		           + " DEF:" + this.defense 
		           + " SPD:" + this.speed);*/
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

	// Method to Spell magic the 'Character'
	public void setSpell(int spell){
		this.bar_magic -= spell;
		// Reproducimos un sonido de dolor del personaje al recibir el golpe
		//if (music != null) music.play_Player_Hurt ();
	}

	void levelUP(){

		int experience = getEXP ();
		int nextLevel = this.next;

		if (experience > nextLevel) {
			//UPDATE STATS
			this.setLevel(1);
			this.calculateEXP();
			this.updateCharacterAttributes();
		}


	}


	void updateCharacterAttributes(){

		// SAVE ATTRIBUTES
		this.save.saveLevel (this.level);

		int VIT = 0, PM = 0, STR = 0, DEF = 0, SPD = 0;

		if(this.load.loadPlayer() == "hombre"){

			VIT = Random.Range (6, 10);
			PM = Random.Range (1, 3);
			STR = Random.Range (3, 5);
			DEF = Random.Range (2, 5);
			SPD = Random.Range (1, 3);

			this.save.saveStatus("VIT", (this.load.loadVIT() +  VIT));
			this.save.saveStatus("PM", (this.load.loadPM() + PM));
			this.save.saveStatus("STR", (this.load.loadSTR() + STR));
			this.save.saveStatus("DEF", (this.load.loadDEF() + DEF));
			this.save.saveStatus("SPD", (this.load.loadSPD() + SPD));

		}else if(this.load.loadPlayer() == "mujer"){

			VIT = Random.Range (2, 6);
			PM = Random.Range (5, 10);
			STR = Random.Range (1, 3);
			DEF = Random.Range (3, 5);
			SPD = Random.Range (2, 4);

			this.save.saveStatus("VIT", this.load.loadVIT() +  VIT);
			this.save.saveStatus("PM", this.load.loadPM() + PM);
			this.save.saveStatus("STR", this.load.loadSTR() + STR);
			this.save.saveStatus("DEF", this.load.loadDEF() + DEF);
			this.save.saveStatus("SPD", this.load.loadSPD() + SPD);

		}else if(this.load.loadPlayer() == "joven"){

			VIT = Random.Range (4, 8);
			PM = Random.Range (3, 7);
			STR = Random.Range (2, 5);
			DEF = Random.Range (2, 4);
			SPD = Random.Range (3, 5);

			this.save.saveStatus("VIT", this.load.loadVIT() +  VIT);
			this.save.saveStatus("PM", this.load.loadPM() + PM);
			this.save.saveStatus("STR", this.load.loadSTR() + STR);
			this.save.saveStatus("DEF", this.load.loadDEF() + DEF);
			this.save.saveStatus("SPD", this.load.loadSPD() + SPD);
		}

		this.updateAttributes (VIT, PM, STR, DEF, SPD);
		
	}

	void updateAttributes(int VIT, int PM, int STR, int DEF, int SPD){

		this.level = this.load.loadLVL ();
		if(this.bar_health + VIT < 510) this.bar_health += VIT; else this.bar_health = 510;
		if(this.bar_magic + PM < 510) this.bar_magic += PM; else this.bar_magic = 510;
		if(this.strength + STR < 255) this.strength += STR; else this.strength = 255;
		if(this.defense + DEF < 255) this.defense += DEF; else this.defense = 255;
		if(this.speed + SPD < 255) this.speed += SPD; else this.speed = 255;
		this.experience = this.load.loadEXP ();

	}

	void loadAttributes(){

		this.level = this.load.loadLVL ();
		this.bar_health = this.load.loadVIT ();
		this.bar_magic = this.load.loadPM ();
		this.strength = this.load.loadSTR ();
		this.defense = this.load.loadDEF ();
		this.speed = this.load.loadSPD ();
		this.experience = this.load.loadEXP ();
	}

	void calculateEXP(){

		int minLevel = 1;					// Level Min. 1
		int currentLevel = getLVL();		// Current Level.
		int maxLevel = 99;					// Level Max. 100.

		int xp_for_first_level = 50;		// Initial experiencie to level 1.
		int xp_for_last_level = 65536;		// Final experiencie to level 100. 
		int nextLevel = 0;					// The amount of experience to level up.
		
		double B = Mathf.Log(((float)xp_for_last_level / xp_for_first_level)) / (maxLevel - 1);
		double A = (float)xp_for_first_level / (Mathf.Exp((float)B) - 1.0);

		// Calculate de amount of experience that you need to Level Up.
		for (int i = minLevel; i <= currentLevel && currentLevel <= maxLevel; i++){	

			int old_xp = (int)Mathf.Round((float)(A * Mathf.Exp((float)B * (i - 1))));
			int new_xp = (int)Mathf.Round((float)(A * Mathf.Exp((float)B * i)));

			nextLevel += (new_xp - old_xp);
		}

		this.next = nextLevel;

	}

	public MemoryCard getMemoryCard(){
		return this.mc;
	}

	void OnGUI(){

		GUI.Label (new Rect (25 , Screen.height / 3, 150, 300),
		           "Attribute Debug:\nLVL: " + this.getLVL() 
		           + "\nVIT: " + this.getHealth() 
		           + "\nPM: " + this.getMagic()
		           + "\nFRZ: " + this.getFRZ()
		           + "\nDEF: " + this.getDEF()
		           + "\nSPD: " + this.getSPD()
		           + "\nEXP: " + this.getEXP()
		           + "\nnextLVL: " + this.next + " EXP.",
		           this.text_style); 

	}
}
