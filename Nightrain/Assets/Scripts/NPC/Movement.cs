using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
	private float rotationSpeed = 1.0f;
	private int moveSpeed = 20;
	int[] degreesList = {90,180,270,360};
	// Los nombres de los tres puntos que estan distribuidos por el mapa
	string[] points = {"Point1", "Point2", "Point3"};
	int i = 0, j=0;
	int delay = 300;
	
	// Use this for initialization
	void Start () {
		// Mas adelante inicializar atributos del NPC
	}
	
	// Update is called once per frame
	void Update () {
		seguirPuntos();
	}

	// Metodo que hace que el personaje vaya uno a uno a los tres puntos del mapa
	void seguirPuntos(){
		GameObject punto = GameObject.Find(points[j]);
		// Calculamos la distancia entre nuestra posicion y el punto del mapa
		float distancia = Vector3.Distance(transform.position, punto.transform.position);
		// Si ya hemos llegado al punto, cambiamos la i para ir al siguiente
		if (distancia <= 1) {
			if (j < 2) {
					j++;
			} else {
					j = 0;
			}
		}
		// Rotamos hacia la direccion
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(punto.transform.position - transform.position), rotationSpeed * Time.deltaTime);
		// Transladamos el NPC hacia el punto
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
	}

	// Camina, rotandose 90 grados cada cierto tiempo
	void caminar(){
		// Transladamos el NPC hacia adelante
		transform.position += transform.forward * moveSpeed * Time.deltaTime;
		delay--;
		// Cada cierto tiempo movemos el personaje 90 grados a la derecha
		if (delay < 0) {
			delay = 300;
			rotar (degreesList[i]);
			i++;
			if (i > 3) {
				i = 0;
			}
		}
	}

	// Metodo que rota el NPC unos determinados grados
	void rotar(int degrees){
		Quaternion newRotation = Quaternion.AngleAxis (degrees, Vector3.up);
		transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, rotationSpeed);
	}
	
}
