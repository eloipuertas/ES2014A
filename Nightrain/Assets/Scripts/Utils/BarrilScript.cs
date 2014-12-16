using UnityEngine;
using System.Collections;

public class BarrilScript : MonoBehaviour {

	private GameObject character;
	private ClickToMove cm;
	private ClickToMove_lvl2 cm2;
	private Music_Engine_Script music;
	
	public GameObject barrel;
	public GameObject[] parts_barrel;
	public GameObject sphere;
	
	public int drop_item = 1;

	private float delay = 10f;
	private bool destroy;

	// Use this for initialization
	void Start () {
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cm = this.character.GetComponent<ClickToMove> ();
		this.cm2 = this.character.GetComponent<ClickToMove_lvl2> ();
		this.music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script> ();
		destroy = false;
		this.sphere.SetActive(false);
	}

	void Update(){

		if (destroy) {
			delay -= Time.deltaTime;
			if(delay <= 0f)
				Destroy(barrel);
		}
	}

	void OnTriggerEnter(Collider collision) {

		if(!destroy){
			if(collision.gameObject.tag == "Player" && sphere != null){

				this.music.play_Player_Sword_Attack ();


				if(cm != null)
					cm.attack();
				else if(cm2 != null)
					cm2.attackAnim();


				this.music.play_destroyBarrel();

				foreach (GameObject part in parts_barrel){
					Rigidbody gameObjectsRigidBody = part.AddComponent<Rigidbody>(); // Add the rigidbody.
					gameObjectsRigidBody.mass = 1000; // Set the GO's mass to 5 via the Rigidbody.
					gameObjectsRigidBody.drag = 0;
					gameObjectsRigidBody.angularDrag = 10;
					gameObjectsRigidBody.useGravity = true;
				}

				sphere.SetActive(true);
				destroy = true;
			}


		}
	}

}
