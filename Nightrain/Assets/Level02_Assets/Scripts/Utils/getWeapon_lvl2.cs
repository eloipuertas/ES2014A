using UnityEngine;
using System.Collections;

public class getWeapon_lvl2 : MonoBehaviour {

	private GameObject character;
	public GameObject tapa;
	public GameObject arma1;
	public GameObject arma2;
	public int pos_y;
	private float AngleX = -90.0f;
	private float targetValue = 0.0f;
	private float currentValue = 0.0f;
	private float easing = 0.05f;
	private bool first = true;
	private bool first_t = true;
	
	private Music_Engine_Script music;
	
	// ========================= COLISION CON Player ==================================
	void Start(){
		arma1.SetActive(false);
		arma2.SetActive(false);
		music = GameObject.FindGameObjectWithTag ("music_engine").GetComponent<Music_Engine_Script> ();
	}
	void OnTriggerEnter (Collider other){
		/*
		 * If para que solo detecte una vez al jugador acercarse.
		 * Se abra y no vuelva a cerrarse.
		 */
		if (first_t == true) {
			if (other.gameObject == GameObject.FindGameObjectWithTag ("Player")) {
				targetValue = AngleX;
				currentValue = 0;
				first = true;
				music.play_Open_Chest ();
				first_t = false;
			}
		}
	}
	/*
	 * No descomentar, esta funcion cierra los cofres.
	 * Al descomentarla no se cerrarán.
	void OnTriggerExit (Collider other) {
		currentValue = AngleX;
		targetValue = 0.0f;
		if(arma1 != null)
			arma1.SetActive(false);
		first=false;
	}
	*/
	void Update(){
		currentValue = currentValue + (targetValue - currentValue) * easing;
		tapa.transform.rotation = Quaternion.identity; // set rotation to zero
		tapa.transform.Rotate(currentValue, pos_y, 0); // apply full Rotation
		if (currentValue<AngleX+20 && first==true && arma1 != null){
			first=false;
			arma1.SetActive(true);
		}
	}
	
}