using UnityEngine;
using System.Collections;

public class AspectRatio_lvl2 : MonoBehaviour {


	// Use this for initialization
	void Start () {

		// Obtenemos las coordenadas del Viewport de la parte superior derecha
		Vector3 TopRight = Camera.main.ViewportToWorldPoint (new Vector3 (1, 1, this.transform.localScale.z));
		
		// Obtenemos las coordenadas del Viewport de la parte inferior izquierda
		Vector3 BottomLeft = Camera.main.ViewportToWorldPoint (new Vector3 (-1, -1, this.transform.localScale.z));
		
		// Escalamos el fondo al ViewPort de la camara
		//this.transform.localScale = TopRight - BottomLeft;
		this.transform.localScale =  (new Vector3 (	Mathf.Abs (	TopRight.x - BottomLeft.x),
		                                            			0.00001f,
		                                            Mathf.Abs (	TopRight.z - BottomLeft.z)));

		//print ("UpRight = " + TopRight.x + "   DownLeft = " + BottomLeft.x);

	}

}
