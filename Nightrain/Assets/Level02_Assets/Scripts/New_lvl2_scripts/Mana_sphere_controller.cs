using UnityEngine;
using System.Collections;

public class Mana_sphere_controller : MonoBehaviour {
	private CharacterScript mana;
	//private int max_health;
	
	// Use this for initialization
	void Start () {
		mana = GameObject.FindGameObjectWithTag ("Player").GetComponent <CharacterScript>();
		//max_health = (int) health.getMaxHealth();
		Destroy (this.gameObject, 15.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			mana.setRecoverMagic(10);
			
			Destroy (this.gameObject);
		}
	}
}
