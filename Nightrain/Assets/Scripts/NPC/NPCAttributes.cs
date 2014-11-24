using UnityEngine;
using System.Collections;

public class NPCAttributes{
	
	//##############################
	//Atributos personaje
	private float moveSpeed; 
	private float health;
	private float max_health;
	private float defense;
	private float attackPower;
	private int experience;
	//##############################
	

	public NPCAttributes(float VIT, float maxVIT, float STR, float DEF, float SPD, int EXP){

		this.health = VIT;
		this.max_health = maxVIT;
		this.attackPower = STR;
		this.defense = DEF;
		this.moveSpeed = SPD;
		this.experience = EXP;

	}


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

	public int getExperience(){
		return this.experience;
	}


	public void setDamage(float damage){
		health -= damage;
	}	
}
