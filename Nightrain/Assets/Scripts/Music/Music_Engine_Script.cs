using UnityEngine;
using System.Collections;

public class Music_Engine_Script : MonoBehaviour {
	
	public AudioClip npc_golem_attack;
	public AudioClip npc_golem_agresive;
	public AudioClip character_attack;
	public AudioClip[] character_hurt;
	public AudioClip character_steps;
	public AudioClip barrel_open;
	public AudioClip button_click;
	public AudioClip button_hover;
	public AudioClip open_chest;
	public AudioClip fireball_explosion;
	public AudioClip fireball_shot;
	public AudioClip lethalknife_shot;
	public AudioClip lethalknife_collision;
	public AudioClip danger_life;
	public AudioClip recover_life;
	public AudioClip low_PM;
	public AudioClip levelUp;

	private string character;
	
	// Use this for initialization
	void Start () {
		this.character = PlayerPrefs.GetString("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}
	
	public void play_Golem_Attack() {
		//Debug.Log ("Reproduciendo sonido");
		audio.PlayOneShot (npc_golem_attack);
	}
	
	public void play_Golem_Agresive() {
		//Debug.Log ("Reproduciendo sonido");
		audio.PlayOneShot (npc_golem_agresive);
	}
	
	public void play_Player_Sword_Attack() {
		//Debug.Log ("Reproduciendo sonido");
		audio.PlayOneShot (character_attack);
	}
	
	public void play_Player_Hurt() {
		//Debug.Log ("Reproduciendo sonido");
		if(this.character == "hombre")
			audio.PlayOneShot (character_hurt[0]);
		else if(this.character == "mujer")
			audio.PlayOneShot (character_hurt[1]);
		else if(this.character == "joven")
			audio.PlayOneShot (character_hurt[2]);
	}
	
	public void play_Player_Steps() {
		//Debug.Log ("Reproduciendo sonido");
		audio.PlayOneShot (character_steps);
	}
	
	public void stop_Player_Steps() {
		//Debug.Log ("Reproduciendo sonido");
		audio.Stop ();
	}
	
	public void Play_Barrel_Open() {
		//Debug.Log ("Reproduciendo sonido");
		audio.PlayOneShot (barrel_open);
	}
	
	public void Play_Button_Click() {
		//Debug.Log ("Reproduciendo sonido");
		audio.PlayOneShot (button_click);
	}
	
	public void Play_Button_Hover() {
		//Debug.Log ("Reproduciendo sonido");
		audio.PlayOneShot (button_hover);
	}
	
	public void play_Open_Chest() {
		audio.PlayOneShot (open_chest);
	}
	
	public void play_Fireball_Shot () {
		audio.PlayOneShot (fireball_shot);
	}
	
	public void play_Fireball_Explosion () {
		audio.PlayOneShot (fireball_explosion);
	}
	
	public void play_Lethalknife_Shot () {
		audio.PlayOneShot (lethalknife_shot);
	}
	
	public void play_Lethalknife_Collision () {
		audio.PlayOneShot (lethalknife_collision);
	}

	public void play_Danger_Life () {
		audio.PlayOneShot (danger_life);
	}

	public void play_Recover_Life () {
		audio.PlayOneShot (recover_life);
	}

	public void play_low_PM () {
		audio.PlayOneShot (low_PM);
	}

	public void play_level_Up () {
		audio.PlayOneShot (levelUp);
	}
}