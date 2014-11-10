using UnityEngine;
using System.Collections;

public class EarnHealth : MonoBehaviour {
	public CharacterScript cs;
	private GameObject character;
	//private GameObject heart;
	
	void Start () {
		// Buscamos al personaje principal
		//this.Player = GameObject.FindGameObjectWithTag("Player");
		this.character = GameObject.FindGameObjectWithTag ("Player");
		//this.heart = GameObject.Find ("heart1");
		//cs = GetComponent<CharacterScript>();
		//cs = GetComponent<CharacterScript>();
		
		cs = GameObject.FindObjectOfType(typeof(CharacterScript)) as CharacterScript;

	}

	// ========================= COLISION CON Player ==================================
	
	void OnTriggerEnter (Collider other){
		print("Vida.");
		if(other.gameObject == this.character){
			cs.setCure(10);
			print("Vidaaa.");
			Destroy(this.gameObject);
		}
		//this.cs.setCure(10);
	}
	
}
