using UnityEngine;
using System.Collections;

public class GameEngineLevel01 : MonoBehaviour {
	
	public Texture2D[] cursor;
	private CursorMode mode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;

	private Transform prefab;
	private GameObject character;

	public GameObject ambientLight;
	private CharacterScript cs;
	private Color c;

	// Use this for initialization
	void Start () {

		Cursor.SetCursor(cursor[0], hotSpot, mode);

		string str_character = PlayerPrefs.GetString ("Character");
		string dificulty = PlayerPrefs.GetString ("Dificulty");

		this.prefab = Resources.Load<Transform>("Prefabs/MainCharacters/" + PlayerPrefs.GetString("Character"));
		Instantiate (prefab);
		this.character = GameObject.FindGameObjectWithTag ("Player");

		this.cs = this.character.GetComponent<CharacterScript> ();
		this.c = this.ambientLight.light.color;

		print ("Se ha cargado el personaje " + str_character);
	}
	
	// Update is called once per frame
	void Update () {
	
		this.CautionScreen ();

		if (Input.GetMouseButtonDown (0)) {
			Cursor.SetCursor(cursor[1], hotSpot, mode);
		}else if(Input.GetMouseButtonUp(0)){
			Cursor.SetCursor(cursor[0], hotSpot, mode);
		}
	}

	// Efecto critico con luz roja
	void CautionScreen(){

		if (cs.isCritical()) {
			this.c.r = 1.0f;
			if(this.c.g >= 0.5f)
				this.c.g -= 0.02f;
			if(this.c.b >= 0.5f)
				this.c.b -= 0.02f;
			this.ambientLight.light.color = this.c;
		} else {
			this.c.r = 1.0f;
			if(this.c.g <= 1.0f)
				this.c.g += 0.02f;
			if(this.c.b <= 1.0f)
				this.c.b += 0.02f;
			this.ambientLight.light.color = this.c;
		}
	}
}
