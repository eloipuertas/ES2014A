using UnityEngine;
using System.Collections;

public class CharacterScript_lvl2 : MonoBehaviour {
	
	// ATRIBUTES CHARACTER
	
	private int level;			// Value of level main character.
	
	// === HEALTH ===
	private int bar_health;		// Value of initial HEALTH
	public float max_health;	// Value of max HEALTH on the GAME. This value basically is to resize the health bar.
	
	// === MAGIC ===
	private float bar_magic;		// Value of initial MAGIC
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
	private bool hasMagic = true;
	
	// =========================
	//private GameObject[] NPCs;
	//private GameObject boss;
	
	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;
	
	// MUSIC AND EFFECTS
	private Music_Engine_Script music;
	private float danger_delay = 0f;
	
	private float magic_delay = .5f;
	private bool  sound_magic = false;
	
	// Effect to level UP
	private GameObject level_effect;
	private float effect_delay = 1f;
	private bool levelUp_Effect = false; 
	
	private Texture2D LevelUpTexture;
	
	private GameObject heal_effect;
	private float heal_delay = 1f;
	private bool heal_Effect = false; 
	
	private GameObject mana_effect;
	private float mana_delay = 1f;
	private bool mana_Effect = false; 
	
	
	// Use this for initialization
	void Awake () {
		
		// ADD COMPONENT
		/*
		// Buscamos al personaje principal
		this.NPCs = GameObject.FindGameObjectsWithTag("Enemy");
		this.boss = GameObject.FindGameObjectWithTag("Boss");
*/
		this.music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
		
		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData();
		this.load = this.mc.loadData();
		
		// ADD TEXTURES
		this.LevelUpTexture = Resources.Load<Texture2D>("Misc/levelup");
	}
	
	void Start(){
		
		/*this.text_style = new GUIStyle ();
		this.text_style.normal.textColor = Color.black;
		this.text_style.fontSize = 15;
		//this.text_style.alignment = TextAnchor.UpperCenter ; 
		this.text_style.wordWrap = true;*/
		
		// LOAD ATTRIBUTES
		this.loadAttributes ();
		this.calculateEXP ();
	}
	
	// Update is called once per frame.
	void Update () {
		
		if(music != null && this.isCritical()){ 
			this.danger_delay -= Time.deltaTime;
			if(this.danger_delay < 0){
				this.music.play_Danger_Life();
				this.danger_delay = 1f;
			}
		}
		
		if(music != null && this.sound_magic){ 
			this.magic_delay -= Time.deltaTime;
			if(this.magic_delay < 0){
				this.sound_magic = false;
				this.magic_delay = .5f;
			}
		}
		
		if(this.heal_effect){
			this.heal_delay -= Time.deltaTime;
			if(this.heal_delay < 0){
				Destroy(heal_effect);
				this.heal_Effect = false;
				this.heal_delay = 2f;
			}
		}
		
		if(this.mana_effect){
			this.mana_delay -= Time.deltaTime;
			if(this.mana_delay < 0){
				Destroy(mana_effect);
				this.mana_Effect = false;
				this.mana_delay = 2f;
			}
		}
		
		
		if(this.level < 100)
			levelUP ();
		
		if(this.levelUp_Effect){
			this.effect_delay -= Time.deltaTime;
			if(this.effect_delay < 0){
				Destroy(level_effect);
				this.levelUp_Effect = false;
				this.effect_delay = 2f;
			}
		}
		
		// Regeneration per second
		this.magicRegeneration (Time.deltaTime);
	}
	
	/*
	// ========================= COLISION CON NPC ==================================
	
	void OnTriggerEnter (Collider other){
		int damage = Random.Range ((this.strength + 1) - (int)(4 + this.strength * 0.25f), this.strength+1);

		if(other.gameObject == this.boss)
			this.boss.GetComponent<Movement>().setDamage(damage);
			

		for(int i = 0; i < NPCs.Length; i++)
				if(other.gameObject == this.NPCs[i])
					this.NPCs[i].GetComponent<Movement_graveler>().setDamage(damage);
				
			
	}
	
	// ==============================================================================
*/
	
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
	
	// Set actual strength value.
	public void setVIT(int VIT){
		
		this.bar_health += VIT;
		
		if(this.bar_health > 510)
			this.bar_health = 510;
		else if(this.bar_health <= 0)
			this.bar_health = this.load.loadVIT();
		
		this.max_health = this.bar_health;
		
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
	public float getMagic(){
		return this.bar_magic;
	}
	
	// Set the magic value.
	public void setMagic(int magic){
		this.max_magic = magic;
	}
	
	// Set actual strength value.
	public void setPM(int PM){
		
		this.bar_magic += PM;
		
		if(this.bar_magic > 510)
			this.bar_magic = 510;
		else if(this.bar_magic <= 0)
			this.bar_magic = this.load.loadPM();
		
		this.max_magic = this.bar_magic;
		
	}
	
	// Function magic regeneration
	public void magicRegeneration(float magic){
		if ((this.bar_magic + magic) < this.max_magic)
			this.bar_magic += magic;
		else
			this.bar_magic = this.max_magic;
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
		
	}
	
	// Get the actual value of speed.
	public int getSPD(){
		return this.speed;
	}
	
	// Set actual speed value.
	public void setSPD(int SPD){
		this.speed += SPD;
		
		if(this.speed > 255)
			this.speed = 255;
		else if(this.speed <= 0)
			this.speed = this.load.loadSPD();
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
	
	public int getNextExpLevel(){
		return this.next;
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
	
	public bool HasEnoughtMagic(int cost_spell){
		
		if (this.bar_magic < cost_spell && !this.sound_magic){
			this.music.play_low_PM ();
			this.sound_magic = true;
			return false;
		}else if(this.bar_magic > cost_spell)
			return true;
		
		return false;
	}

	public void setManaRestore (int mana) {
		if (this.bar_magic < this.max_magic) {
			this.bar_magic += mana;
			if(this.bar_magic > this.max_magic)
				this.bar_magic = Mathf.FloorToInt(this.max_magic);
		}
	}
	
	// Method to Cure the 'Character'.
	public void setCure(int heal){
		if (this.bar_health < this.max_health){
			
			this.music.play_Recover_Life();
			this.bar_health += heal;
			if(this.bar_health > this.max_health)
				this.bar_health = Mathf.FloorToInt(this.max_health);
			
			if(!this.heal_Effect){ 
				this.heal_effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/heal")) as GameObject;
				this.heal_effect.transform.position = transform.position;
				this.heal_effect.transform.parent = transform;
				this.heal_Effect = true;
				
				
			}
		}
	}
	
	// Method to compute Damage
	public int computeDamage(){
		return Random.Range((this.strength + 1) - (int)(4 + this.strength * 0.25f), this.strength + 1);
	}
	
	// Method to Damage the 'Character'
	public void setDamage(int damage){
		this.bar_health -= damage;
		// Reproducimos un sonido de dolor del personaje al recibir el golpe
		if (music != null && this.bar_health > 0) music.play_Player_Hurt ();
	}
	
	// Method to Spell magic the 'Character'
	public void setSpell(int spell){
		this.bar_magic -= spell;
		// Reproducimos un sonido de dolor del personaje al recibir el golpe
		//if (music != null) music.play_Player_Hurt ();
	}
	
	// Method to Cure the 'Character'.
	public void setRecoverMagic(int magic){
		//if (this.bar_magic < this.max_magic){	
		//if(this.bar_magic > this.max_magic)
		//this.bar_magic = Mathf.FloorToInt(this.max_magic);		
		/*this.bar_magic += magic;
		if (this.bar_magic > this.max_magic)
			this.bar_magic = this.max_magic;*/
		//}
		
		if (this.bar_magic < this.max_magic){
			
			this.music.play_Recover_Life();
			this.bar_magic += magic;
			if(this.bar_magic > this.max_magic)
				this.bar_magic = Mathf.FloorToInt(this.max_magic);
			
			if(!this.mana_Effect){ 
				this.mana_effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/mana_recover")) as GameObject;
				this.mana_effect.transform.position = transform.position;
				this.mana_effect.transform.parent = transform;
				this.mana_Effect = true;
				
				
			}
		}
	}
	
	void levelUP(){
		
		int experience = getEXP ();
		int nextLevel = this.next;
		
		if (experience >= nextLevel) {
			
			if(!this.levelUp_Effect){
				this.music.play_level_Up();
				this.level_effect = Instantiate(Resources.Load<GameObject>("Prefabs/Effects/level_up")) as GameObject;
				this.level_effect.transform.position = new Vector3(transform.position.x, (transform.position.y+10.0f), transform.position.z);
				this.level_effect.transform.parent = transform;
				this.levelUp_Effect = true;
			}
			
			//UPDATE STATS
			this.setLevel(1);
			this.calculateEXP();
			this.updateCharacterAttributes();
			
		}
		
		
	}
	
	
	void updateCharacterAttributes(){
		
		// SAVE ATTRIBUTES
		//this.save.saveLevel (this.level);
		//this.save.saveStatus ("LVL", this.level);
		
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
		
		if(this.levelUp_Effect){
			
			Vector2 xy = Camera.main.WorldToScreenPoint(new Vector3(this.transform.position.x,
			                                                        this.transform.position.y,
			                                                        this.transform.position.z));
			
			Rect level_box = new Rect (xy.x/1.1f,
			                           xy.y/1.75f, 
			                           this.LevelUpTexture.width/2f, 
			                           this.LevelUpTexture.height/1.5f);
			
			GUI.DrawTexture (level_box, this.LevelUpTexture);
		}
		
		
	}
}
