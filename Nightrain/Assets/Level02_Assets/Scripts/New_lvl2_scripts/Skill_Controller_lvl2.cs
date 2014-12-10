using UnityEngine;
using System.Collections;

public class Skill_Controller_lvl2 : MonoBehaviour {
	// Skills time to re-use them
	public float fireball_cooldown = 2.0f;
	public float dagger_skill_cooldown = 2.0f;
	public float warrior_aura_cooldown = 25.0f;
	
	// Actual time in each frame
	private float actual_time;
	
	private float fireball_time = 0.0f;
	private float warrior_aura_time = 0.0f;
	private float dagger_skill_time = 0.0f;
	
	// Objects in the game
	private GameObject fireball;
	private GameObject dagger_shot;
	private GameObject warrior_aura;
	private GameObject warrior_aura_actual; //Necesario para añadirlo al jugador como hijo
	
	// Main character
	private GameObject player;
	private ClickToMove_lvl2 cm;
	private CharacterScript_lvl2 cs;
	
	// Use this for initialization
	void Start () {
		//
		this.fireball = Resources.Load<GameObject>("Prefabs/Character_Skills/Fireball_Skill");
		this.warrior_aura = Resources.Load<GameObject>("Prefabs/Character_Skills/Warrior_Aura1");
		this.dagger_shot = Resources.Load<GameObject>("Prefabs/Character_Skills/Daga_skill/Daga_skill");
		
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.cs = this.player.GetComponent<CharacterScript_lvl2> ();
		this.cm = this.player.GetComponent<ClickToMove_lvl2> ();
		
		actual_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Si el juego no esta en PAUSE
		if (Time.timeScale == 1) {
			actual_time = Time.time;
			
			// Al presionar el boton 1 del teclado disparamos la bola de fuego
			if (Input.GetKeyDown (KeyCode.Alpha1)) {
				
				if(this.cs.getMagic() >= 15){ //<-- 15PM
					if (!skillOnCD(fireball_time, fireball_cooldown)) {
						rotatePlayerToMouse();
						// Dispara la bola en la direccion que apunta el personaje
						this.cm.attackAnim ();
						this.cs.setSpell(15);		// to cast a spell cost 15PM.
						Vector3 newPosition = player.transform.position;
						newPosition.y += 6;
						Instantiate (fireball, newPosition, player.transform.rotation);
						//Guardamos el tiempo de disparo de la bola de fuega
						fireball_time = Time.time;
						ActionBarScript.disabledSkill1 = true;
						//print ("Skill1 true");
					}
				}
			}
			
			if (Input.GetKeyDown (KeyCode.Alpha2)) {
				actual_time = Time.time;
				
				if(this.cs.getMagic() >= 10){ //<-- 10PM
					if (!skillOnCD(dagger_skill_time, dagger_skill_cooldown)) {
						rotatePlayerToMouse();
						this.cm.attackAnim ();
						// Dispara la bola en la direccion que apunta el personaje
						this.player.animation.CrossFade ("metarig|Atacar", 0.2f);
						this.cs.setSpell(10);		// to cast a spell cost 10PM.
						Vector3 newPosition = player.transform.position;
						newPosition.y += 6;
						Instantiate (dagger_shot, newPosition, player.transform.rotation);
						//Guardamos el tiempo de disparo de la bola de fuega
						dagger_skill_time = Time.time;
						ActionBarScript.disabledSkill2 = true;
						//print ("Skill2 true");
					}
				}
			}
			
			// Al presionar el boton 2 del teclado lanzamos la daga
			if (Input.GetKeyDown (KeyCode.Alpha3)) {
				actual_time = Time.time;
				
				if(this.cs.getMagic() >= 30){ //<-- 30PM
					// Si la skill no esta en cooldown
					if (!skillOnCD(warrior_aura_time, warrior_aura_cooldown)) {
						rotatePlayerToMouse();
						// Dispara la bola en la direccion que apunta el personaje
						this.player.animation.CrossFade ("metarig|Atacar", 0.2f);
						this.cs.setSpell(20);		// to cast a spell cost 30PM.
						Vector3 newPosition = player.transform.position;
						newPosition.y = transform.position.y - 2;
						warrior_aura_actual = Instantiate (warrior_aura, newPosition, player.transform.rotation) as GameObject;
						// asignamos al personaje como padre
						warrior_aura_actual.transform.parent = player.transform;
						warrior_aura_time = Time.time; // para el cooldown
						ActionBarScript.disabledSkill3 = true;
						//print ("Skill3 true");
					}
				}
			}
		}
	}
	
	bool skillOnCD (float skill_time, float cd_time) {
		if (skill_time == 0.0f || (actual_time - skill_time) >= cd_time)
			return false;
		else
			return true;
	}
	
	void rotatePlayerToMouse() {
		// Para lanzar la bola a donde apunta con el mouse
		Plane playerPlane = new Plane(Vector3.up, player.transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float hitdist = 0.0f;
		
		if (playerPlane.Raycast(ray, out hitdist)) {
			Vector3 targetPoint = ray.GetPoint(hitdist);
			Vector3 destinationPosition = ray.GetPoint(hitdist);
			Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
			player.transform.rotation = targetRotation;
		}
	}
}
