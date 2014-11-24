using UnityEngine;
using System.Collections;

public class NPCAttributes : MonoBehaviour {
<<<<<<< HEAD
	
=======

>>>>>>> 50cf72db9b3f7bd29eb04ae90e83644dbe027dd7
	//##############################
	//Atributos personaje
	private float moveSpeed = 5; 
	public float health = 75;
	private float max_health = 75;
	private float defense = 5;
	private float attackPower = 3;
	//##############################
<<<<<<< HEAD
	
	public void Start(){
	}
	
	public NPCAttributes(){
	}
	
=======

	public void Start(){
	}

	public NPCAttributes(){
	}

>>>>>>> 50cf72db9b3f7bd29eb04ae90e83644dbe027dd7
	public void setDificulty(string difficulty){
		float percent = 0.0f;
		if (difficulty.Equals ("Normal")) {
			percent = 0.75f;
		} 
		else if (difficulty.Equals ("Hard")) {
			percent = 1.75f;
		}
		else if (difficulty.Equals("Extreme")){
			percent = 2.25f;
		}
		
		//moveSpeed = moveSpeed + (moveSpeed * percent);
		health = health + (health * percent);
		max_health = max_health + (max_health * percent);
		defense = defense + (defense * percent);
		attackPower = attackPower + (attackPower * percent);
	}
<<<<<<< HEAD
	
	public void Update(){
		
	}
	
=======

	public void Update(){
		
	}

>>>>>>> 50cf72db9b3f7bd29eb04ae90e83644dbe027dd7
	public void setHealth(float health){
		this.health = health;
		this.max_health = health;
	}
	
	public void setDefense(float defense){
		this.defense = defense;
	}
	
	public void setAttackPower(float attackPower){
		this.attackPower = attackPower;
	}
	
	public void setMoveSpeed(float moveSpeed){
		this.moveSpeed = moveSpeed;
	}
	
	public float getHealth(){
		return this.health;
	}
	
	public float getMaxHealth(){
		return this.max_health;
	}
	
	public float getDefense(){
		return this.defense;
	}
	
	public float getMoveSpeed(){
		return this.moveSpeed;
	}
	
	public float getAttackPower(){
		return this.attackPower;
	}
<<<<<<< HEAD
	
=======

>>>>>>> 50cf72db9b3f7bd29eb04ae90e83644dbe027dd7
	public void setDamage(float damage){
		health -= damage;
	}	
}
