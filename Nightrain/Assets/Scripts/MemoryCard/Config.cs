using UnityEngine;
using System.Collections;

public class Config : MonoBehaviour {

	// Initial configure of Nightrain when game begins.
	void Start() {
		// Cuando inicio el juego quiero que cargue la Intro.
		PlayerPrefs.SetInt ("Cutscene", 0);
		// Cargamos el numero de frames de la Intro.
		PlayerPrefs.SetInt ("Frames", 800);
		// Cargamos el path del cutscene inicial.
		PlayerPrefs.SetString ("Path", "Cutscenes/Intro/Intro ");
		// Despues de la intro queremos que cargue el MainMenu.
		PlayerPrefs.SetInt ("Scene", 1); //<-- El numero indica el indice del scene
	}
}
