using UnityEngine;
using System.Collections;

public class DamageFadingScript : MonoBehaviour {

	private CharacterScript cs;
	private GameObject character;

	// --- TEXTURES ---
	private Texture DamageFadeTexture;

	private float alpha = 0.0f;
	public float max_alpha = 0.65f;

	// Use this for initialization
	void Start () {

		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();

		// ADD TEXTURES
		this.DamageFadeTexture = Resources.Load<Texture>("DamageFading/bloodscreen");

	}
	
	void OnGUI(){
		
		if (this.cs.isCritical ()) {		
			// DAMAGE FADE
			if (Event.current.type.Equals (EventType.Repaint)) {
				GUI.color = new Color (1f, 1f, 1f, this.alpha);
				Rect damage_box = new Rect (0, 0, Screen.width, Screen.height);
				GUI.DrawTexture (damage_box, this.DamageFadeTexture);
			}

			if (this.alpha < this.max_alpha)
					this.alpha += 0.02f;
		} else 
			if (this.alpha > 0.0f) {
				this.alpha -= 0.015f;
				if (Event.current.type.Equals (EventType.Repaint)) {
					GUI.color = new Color (1f, 1f, 1f, this.alpha);
					Rect damage_box = new Rect (0, 0, Screen.width, Screen.height);
					GUI.DrawTexture (damage_box, this.DamageFadeTexture);
				}
			}
	}


}
