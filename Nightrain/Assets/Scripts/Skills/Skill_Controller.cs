using UnityEngine;
using System.Collections;

public class Skill_Controller : MonoBehaviour {
	public float fireball_cooldown = 2.0f;
	public float warrior_aura_cooldown = 25.0f;
	
	private float actual_time;
	
	private float fireball_time = 0.0f;
	private float warrior_aura_time = 0.0f;
	
	private Transform fireball;
	private GameObject warrior_aura;
	private GameObject actual_warrior_aura;
	//private Transform player;
	
	private CharacterScript cs;
	private GameObject character;
	
	
	// Use this for initialization
	void Start () {
		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();
		
		this.fireball = Resources.Load<Transform>("Prefabs/Character_Skills/Fireball_Skill");
		this.warrior_aura = Resources.Load<GameObject>("Prefabs/Character_Skills/Warrior_Aura1");
		//this.player = GameObject.FindGameObjectWithTag("Player").transform;
		actual_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale == 1) {
			if (Input.GetKeyDown (KeyCode.Alpha1) && ActionBarScript.disabledSkill1 == false) {

				actual_time = Time.time;

				if(cs.getMagic() > 15){ //<-- 15PM
					if (fireball_time == 0.0f || (actual_time - fireball_time) >= fireball_cooldown) {
						this.cs.setSpell(15);		// to cast a spell cost 15PM.
						// Para lanzar la bola a donde apunta con el mouse
						Plane playerPlane = new Plane(Vector3.up, character.transform.position);
						Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
						float hitdist = 0.0f;
						
						if (playerPlane.Raycast(ray, out hitdist)) {
							Vector3 targetPoint = ray.GetPoint(hitdist);
							Vector3 destinationPosition = ray.GetPoint(hitdist);
							Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
							character.transform.rotation = targetRotation;
						}
						
						// Dispara la bola en la direccion que apunta el personaje
						this.character.animation.CrossFade ("metarig|Atacar", 0.2f);
						fireball.rotation = transform.rotation;
						Vector3 newPosition = transform.position;
						newPosition.y += 2;
						fireball.transform.position = newPosition;
						Instantiate (fireball);
						fireball_time = Time.time;
						ActionBarScript.disabledSkill1 = true;
					}
				}
			}
			
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				actual_time = Time.time;
				
				// Si la skill no esta en cooldown
				if (warrior_aura_time == 0.0f || (actual_time - warrior_aura_time) >= warrior_aura_cooldown) {
					this.character.animation.CrossFade ("metarig|Atacar", 0.2f);
					
					// ponemos a la skill la posicion y rotacion del personaje
					
					warrior_aura.transform.rotation = transform.rotation;
					Vector3 newPosition = transform.position;
					newPosition.y = 0.0f;
					warrior_aura.transform.position = newPosition;
					
					actual_warrior_aura = Instantiate (warrior_aura) as GameObject;
					// asignamos al personaje como padre
					actual_warrior_aura.transform.parent = character.transform;
					warrior_aura_time = Time.time; // para el cooldown
				}
			}
		}
	}
	
	void OnParticleCollision(GameObject other) {
		print ("hit by fire");
	}
}
