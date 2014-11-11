using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {

	// ATRIBUTES CHARACTER

	// === HEALTH ===
	public int bar_health;		// Value of initial HEALTH
	public float max_health;	// Value of max HEALTH on the GAME. This value basically is to resize the health bar.

	// === MAGIC ===
	public int bar_magic;		// Value of initial MAGIC
	public float max_magic;		// Value of max MAGIC on the GAME. This value basically is to resize the magic bar.

	// ADD MORE ATTRIBUTES OF CHARACTER
	private bool critical = false;

	// =========================
	private GameObject NPCs;

	// Use this for initialization
	void Start () {
	
		// ADD COMPONENT
		// Buscamos al personaje principal
		this.NPCs = GameObject.FindGameObjectWithTag("Enemy");
	}
	
	// Update is called once per frame.
	void Update () {
	
	}


	// ========================= COLISION CON NPC ==================================
	
	void OnTriggerEnter (Collider other){
		if(other.gameObject == this.NPCs){
			this.NPCs.GetComponent<Movement>().setDamage(20);
			print("Tocado."); 
		}	
	}
	
	// ==============================================================================


	// === METHODS HEALTH ===

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
	}

	// === METHODS MAGIC ===

	// Get the actual value of health.
	public int getMagic(){
		return this.bar_magic;
	}
	
	// Get the max value of health in the game.
	public float getMaxMagic(){
		return this.max_magic;
	}

	// Set the max magic value.
	public void setMaxMagic(){
		this.max_magic = this.bar_magic;
	}



}
