using UnityEngine;
using System.Collections;

public class life_Major_sphere_controller : MonoBehaviour {

	private CharacterScript health;
	//private int max_health;
	
	// Use this for initialization
	void Start () {
		health = GameObject.FindGameObjectWithTag ("Player").GetComponent <CharacterScript>();
		//max_health = (int) health.getMaxHealth();
		Destroy (this.gameObject, 15.0f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			/*int curr_health = health.getHealth();
			int dif = max_health - curr_health;
			if (dif >= 10) health.setHealth(curr_health + 10);
			else health.setHealth(curr_health + dif);*/
			health.setCure ((int) health.getMaxHealth ());
			
			Destroy (this.gameObject);
		}
	}
}
