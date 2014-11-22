using UnityEngine;
using System.Collections;

public class ButtonAction : MonoBehaviour {
	
	public Material[] material_buttons;


	// Use this for initialization
	void Start () {

	}


	// Update is called once per frame
	void Update () {
	
	}


	//This function is called when the mouse entered the GUIElement or Collider
	public void OnMouseEnter(){
		this.renderer.material = this.material_buttons [1];
		
	}


	//This function is called when the mouse is not any longer over the GUIElement or Collider
	public void OnMouseExit(){
		this.renderer.material = this.material_buttons [0];
	}


	//This function is called when the user has released the mouse button
	public void OnMouseDown(){

		if (this.tag.Equals ("new_game"))
			this.renderer.material = this.material_buttons [0];
			//print ("Has pulsado Nueva Partida.");
		else if (this.tag.Equals ("load_game"))
			this.renderer.material = this.material_buttons [0];
			//print ("Has pulsado Cargar Partida.");
		else if (this.tag.Equals ("option"))
			this.renderer.material = this.material_buttons [0];
			//print ("Has pulsado Opciones.");
		else if (this.name.Equals ("character_01"))
			print ("Has seleccionado el personaje Cubo.");
		else if (this.name.Equals ("character_02"))
			print ("Has seleccionado el personaje Esfera.");
		else if (this.name.Equals ("character_03"))
			print ("Has seleccionado el personaje Triangulo.");
	}
}
