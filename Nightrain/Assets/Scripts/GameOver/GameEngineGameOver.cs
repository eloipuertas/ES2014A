using UnityEngine;
using System.Collections;

public class GameEngineGameOver : MonoBehaviour {
	
	public Texture2D[] cursor;
	
	private CursorMode mode = CursorMode.Auto;
	
	private RaycastHit getObjectScene;
	
	private GameObject character_menu;
	private GameObject option_menu;
	
	private Vector2 hotSpot = Vector2.zero;
	
	private string lastCharacter = "hombre";
	
	public Transform[] prefab;
	public Material[] material_attributes;
	
	// Use this for initialization
	void Start () {
		
		Cursor.SetCursor(cursor[0], hotSpot, mode);
		
		//if (GameObject.FindGameObjectWithTag ("back") && GameObject.FindGameObjectWithTag ("confirm"))
		//GameObject.FindGameObjectWithTag ("character_menu").gameObject.SetActive (false);
		//this.character_menu.gameObject.SetActive (false);
		//Destroy (GameObject.FindGameObjectWithTag ("start"));
		Instantiate (this.prefab[0]);
	}
	
	// Update is called once per frame
	void Update () {
		this.StateMachine ();
	}
	
	
	public void StateMachine(){
		
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			
			Cursor.SetCursor(cursor[1], hotSpot, mode);
			
			if (Physics.Raycast (ray, out this.getObjectScene, 100)) {
				if (this.getObjectScene.transform.gameObject.tag.Equals ("new_game")) {
					this.audio.Play ();
					print ("Has pulsado Nueva Partida.");
					Application.LoadLevel("Level01");

				} else if (this.getObjectScene.transform.gameObject.tag.Equals ("exit")) {
					this.audio.Play ();
					print ("Has pulsado Salir.");
					Application.LoadLevel(0);
					
				} 
				
				Debug.Log( this.getObjectScene.transform.gameObject.name );
			}
			
		}else if(Input.GetMouseButtonUp(0)){
			Cursor.SetCursor(cursor[0], hotSpot, mode);
		}
	}
}
