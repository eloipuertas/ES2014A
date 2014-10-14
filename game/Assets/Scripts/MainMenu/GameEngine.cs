using UnityEngine;
using System.Collections;

public class GameEngine : MonoBehaviour {

	public Texture2D cursor;

	private CursorMode mode = CursorMode.Auto;

	private RaycastHit getObjectScene;

	private GameObject character_menu;
	private GameObject option_menu;
	
	private Vector2 hotSpot = Vector2.zero;

	private string lastCharacter = "Gordo";

	public Transform[] prefab;
	public Material[] material_attributes;

	// Use this for initialization
	void Start () {

		Cursor.SetCursor(cursor, hotSpot, mode);

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
			
						if (Physics.Raycast (ray, out this.getObjectScene, 100)) {
								if (this.getObjectScene.transform.gameObject.tag.Equals ("new_game")) {

										print ("Has pulsado Nueva Partida.");
										Destroy (GameObject.FindGameObjectWithTag ("option_menu"));
										
										//Instantiate (this.prefab [2]);
										//Instantiate (this.prefab [5]);
										this.prefab[1].FindChild("Attributes").gameObject.renderer.material = this.material_attributes [0];
										this.prefab[1].FindChild("Attributes").gameObject.renderer.material = this.material_attributes [0];
										this.prefab[1].FindChild("Character").gameObject.renderer.material = this.material_attributes [3];
	
										Instantiate (this.prefab [1]);
										//this.prefab[5].renderer.material = this.material_attributes [0];

								} else if (this.getObjectScene.transform.gameObject.tag.Equals ("load_game")) {

										print ("Has pulsado Cargar Partida.");

								} else if (this.getObjectScene.transform.gameObject.tag.Equals ("exit")) {

										print ("Has pulsado Salir.");
										Application.Quit();

								} else if (this.getObjectScene.transform.gameObject.tag.Equals ("back")) {

										print ("Has pulsado Volver.");
										
										Destroy (GameObject.FindGameObjectWithTag ("character_menu"));
										//Destroy (GameObject.FindGameObjectWithTag ("attribute"));
										Destroy (GameObject.FindGameObjectWithTag ("Player"));
										Instantiate (this.prefab [0]);
										this.lastCharacter = "Gordo";

								} else if (this.getObjectScene.transform.gameObject.tag.Equals ("confirm")) {
										print ("Has pulsado Confirmar.");

										PlayerPrefs.SetString("Difficulty", "Normal");
										PlayerPrefs.SetString("Character", this.lastCharacter);
										//PlayerPrefs.Save();
										Application.LoadLevel(1);
								} else if (this.getObjectScene.transform.gameObject.name.Equals ("character_01")){
										
										print ("Has seleccionado el personaje Cubo.");
										if(!this.prefab[2].transform.name.Equals(this.lastCharacter)){
											
											this.prefab[1].FindChild("Character").gameObject.renderer.material = this.material_attributes [3];
											//Destroy (GameObject.FindGameObjectWithTag ("Player"));
											this.prefab[1].FindChild("Attributes").gameObject.renderer.material = this.material_attributes [0];
											Destroy (GameObject.FindGameObjectWithTag ("character_menu"));
											Instantiate(this.prefab[1]);
											//Instantiate (this.prefab [2]);
											this.prefab[1].FindChild("Attributes").gameObject.renderer.material = this.material_attributes [0];
											//GameObject.FindGameObjectWithTag("attribute").gameObject.renderer.material = this.material_attributes [0];
											//this.prefab[5].gameObject.renderer.material = this.material_attributes [0];
											//this.prefab[5].transform.gameObject.renderer.material = this.material_attributes[0];
											this.lastCharacter = "Gordo";
										}
											
								} else if (this.getObjectScene.transform.gameObject.name.Equals ("character_02")){
										
										print ("Has seleccionado el personaje Esfera.");
										if(!this.prefab[3].transform.name.Equals(this.lastCharacter)){
											this.prefab[1].FindChild("Character").gameObject.renderer.material = this.material_attributes [4];
											//Destroy (GameObject.FindGameObjectWithTag ("Player"));
											this.prefab[1].FindChild("Attributes").gameObject.renderer.material = this.material_attributes [1];
											
											Destroy (GameObject.FindGameObjectWithTag ("character_menu"));
											Instantiate(this.prefab[1]);
											//Instantiate (this.prefab [3]);
											//GameObject.FindGameObjectWithTag("attribute").gameObject.renderer.material = this.material_attributes [1];
											//this.prefab[5].transform.gameObject.renderer.material = this.material_attributes [1];
											this.lastCharacter = "Tia";
										}

								}else if (this.getObjectScene.transform.gameObject.name.Equals("character_03")){
										print ("Has seleccionado el personaje Triangulo.");
										if(!this.prefab[4].transform.name.Equals(this.lastCharacter)){
											this.prefab[1].FindChild("Character").gameObject.renderer.material = this.material_attributes [3];
											//Destroy (GameObject.FindGameObjectWithTag ("Player"));
											this.prefab[1].FindChild("Attributes").gameObject.renderer.material = this.material_attributes [2];

											Destroy (GameObject.FindGameObjectWithTag ("character_menu"));
											Instantiate(this.prefab[1]);
											//Instantiate (this.prefab [4]);
											//GameObject.FindGameObjectWithTag("attribute").gameObject.renderer.material = this.material_attributes [2];
											//this.prefab[5].transform.gameObject.renderer.material = this.material_attributes [2];
											this.lastCharacter = "Nino";
										}
								}

				Debug.Log( this.getObjectScene.transform.gameObject.name );
			}
			
		}
	}
}
