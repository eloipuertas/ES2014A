using UnityEngine;
using System.Collections;

public class ClickToMove_lvl2 : MonoBehaviour {
	private int speed = 30;
	
	private RaycastHit getObjectScene;
	private Vector3 targetPosition;
	private Vector3 targetPoint;
	private bool moving; //Whether the player is moving or has stopped
	private CharacterController controller;
	private Music_Engine_Script music;

	//MODIFICACIO PER MANTENIRLO SOBRE EL TERRA
	private float gravity;

	// Use this for initialization
	void Start () {
		animation["metarig|Caminar"].speed = 2.75f;
		animation["metarig|Atacar"].speed = 1.5f;
		music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script> ();
		controller = this.gameObject.GetComponent<CharacterController> ();
		print ("controler"+controller);
		targetPosition = transform.position;
		speed = 30;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.deltaTime != 0) {
			//UpdateHealth();
			// Walking animation control
			if(moving){
				// Stop animation
				if(transform.position.x == targetPoint.x & transform.position.z == targetPoint.z){
					//animation.CrossFade("Armature|Idle",0.2f);
					//Debug.Log ("moved to target location");
					animation.Stop("metarig|Caminar"); 
					moving = false;
					//Set walking animation
				} else {
					animation.Play("metarig|Caminar");
					//Debug.Log ("on the way...");
				}
			}
			
			
			if(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetMouseButton(0)) { 
				//smooth=1;
				
				Plane playerPlane = new Plane(Vector3.up, transform.position);
				Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
				float hitdist = 0.0f;
				
				//deteccion movimiento
				if (playerPlane.Raycast(ray, out hitdist))
				{
					targetPoint = ray.GetPoint(hitdist);
					targetPosition = ray.GetPoint(hitdist);
					Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
					transform.rotation = targetRotation;
				}
				
				if(Physics.Raycast(ray, out getObjectScene, 100.0f)){
					if(getObjectScene.transform.gameObject.tag.Equals("Enemy")){
						//Debug.Log("Enemigo seleccionado");
						//animation.Stop("metarig|Caminar");
						moving = false;
						//attack = true;
						animation.CrossFade("metarig|Atacar",0.2f);
						music.play_Player_Sword_Attack ();
					} else {
						animation.Play("metarig|Caminar");
						moving = true;
						//animation["Armature|Correr"].wrapMode = WrapMode.Loop;
					}
				} 	
			}
			
			//transform.position = Vector3.Slerp (transform.position, targetPosition, Time.deltaTime * smooth);
			//transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);
			
			// find the target position relative to the player:
			Vector3 dir = targetPosition - transform.position;
			
			//MODIFICACIO MANTENIRLO SOBRE EL TERRA
			gravity -= 9.81f * Time.deltaTime;
			if(controller.isGrounded) gravity = 0.0f; // treure el getComponent al Start() i aqui utilitzar simplement la instancia
			dir.y = gravity;
			
			// calculate movement at the desired speed:
			Vector3 movement = dir.normalized * speed * Time.deltaTime;
			// limit movement to never pass the target position:
			if (movement.magnitude > dir.magnitude) movement = dir;
			
			
			
			//MODIFICACIO OPTIMITZACIO
			// move the character:
			controller.Move(movement);
		}
	}

	public void teleport(Vector3 position) {
		targetPosition = position;
	}
}
