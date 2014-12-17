using UnityEngine;
using System.Collections;

public class Skill_Controller : MonoBehaviour {
	// Skills time to re-use them
	public float fireball_cooldown = 2.0f;
	public float dagger_skill_cooldown = 2.0f;
	public float warrior_aura_cooldown = 15.0f;
	
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
	private CharacterScript cs;
	private ClickToMove cm;
	private ClickToMove_lvl2 cm2;

	private static bool effect = false;
	
	// Use this for initialization
	void Start () {
		//
		this.fireball = Resources.Load<GameObject>("Prefabs/Character_Skills/Fireball_Skill");
		this.warrior_aura = Resources.Load<GameObject>("Prefabs/Character_Skills/Warrior_Aura1");
		this.dagger_shot = Resources.Load<GameObject>("Prefabs/Character_Skills/Daga_skill/Daga_skill");
		
		this.player = GameObject.FindGameObjectWithTag("Player");
		this.cs = this.player.GetComponent<CharacterScript> ();
		this.cm = this.player.GetComponent<ClickToMove> ();
		this.cm2 = this.player.GetComponent<ClickToMove_lvl2> ();

		effect = false;
		actual_time = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		// Si el juego no esta en PAUSE
		if (Time.timeScale == 1) {
			actual_time = Time.time;
			
			// Al presionar el boton 1 del teclado disparamos la bola de fuego
			if (Input.GetKeyDown (KeyCode.Alpha1)/* && !effect*/) {

				if(this.cs.HasEnoughtMagic(15)){ //<-- 15PM
					if (!skillOnCD(fireball_time, fireball_cooldown)) {
						effect = true;
						rotatePlayerToMouse();
						// Dispara la bola en la direccion que apunta el personaje
						//this.player.animation.CrossFade ("metarig|Atacar", 0.2f);
						if(cm != null)
							this.cm.attack();
						else if(cm2 != null)
							this.cm2.attackAnim();

						this.cs.setSpell(15);		// to cast a spell cost 15PM.
						Vector3 newPosition = player.transform.position;
						newPosition.y += 2;
						Instantiate (fireball, newPosition, player.transform.rotation);
						//Guardamos el tiempo de disparo de la bola de fuega
						fireball_time = Time.time;
						ActionBarScript.disabledSkill1 = true;
					}
				}
			}
			
			if (Input.GetKeyDown (KeyCode.Alpha2)/* && !effect*/) {
				actual_time = Time.time;

				if(this.cs.HasEnoughtMagic(10)){ //<-- 10PM
					if (!skillOnCD(dagger_skill_time, dagger_skill_cooldown)) {
						effect = true;
						rotatePlayerToMouse();
						// Dispara la bola en la direccion que apunta el personaje
						//this.player.animation.CrossFade ("metarig|Atacar", 0.2f);
						if(cm != null)
							this.cm.attack();
						else if(cm2 != null)
							this.cm2.attackAnim();

						this.cs.setSpell(10);		// to cast a spell cost 10PM.
						Vector3 newPosition = player.transform.position;
						newPosition.y += 2;
						Instantiate (dagger_shot, newPosition, player.transform.rotation);
						//Guardamos el tiempo de disparo de la bola de fuega
						dagger_skill_time = Time.time;
						ActionBarScript.disabledSkill2 = true;
					}
				}
			}
			
			// Al presionar el boton 2 del teclado lanzamos la daga
			if (Input.GetKeyDown (KeyCode.Alpha3)/* && !effect*/) {
				actual_time = Time.time;

				if(this.cs.HasEnoughtMagic(30)){ //<-- 30PM
					// Si la skill no esta en cooldown
					if (!skillOnCD(warrior_aura_time, warrior_aura_cooldown)) {
						effect = true;
						rotatePlayerToMouse();
						// Dispara la bola en la direccion que apunta el personaje
						//this.player.animation.CrossFade ("metarig|Atacar", 0.2f);
						if(cm != null)
							this.cm.attack();
						else if(cm2 != null)
							this.cm2.attackAnim();

						this.cs.setSpell(30);		// to cast a spell cost 30PM.
						Vector3 newPosition = player.transform.position;
						newPosition.y = 0;
						warrior_aura_actual = Instantiate (warrior_aura, newPosition, player.transform.rotation) as GameObject;
						// asignamos al personaje como padre
						warrior_aura_actual.transform.parent = player.transform;
						warrior_aura_actual.transform.localPosition = new Vector3(0, 0, 0);
						warrior_aura_time = Time.time; // para el cooldown
						ActionBarScript.disabledSkill3 = true;
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

	public static void setEffect(bool effect_off){
		effect = effect_off;
	}
}
