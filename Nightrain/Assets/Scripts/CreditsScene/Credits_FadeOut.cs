using UnityEngine;
using System.Collections;

public class Credits_FadeOut : MonoBehaviour {
	public Texture2D fadeOutTexture;
	public float fadeSpeed = 0.8f;	
	
	private int depth = -1;			// Value that says the order that GUI executes 
	private float alpha = 1.0f;
	private int direction = -1;		// Direction -1 = Fade in
	
	// When the scenes begin we'll see a effect to fade in because direction -1.
	void OnGUI(){
		this.alpha += direction * fadeSpeed * Time.deltaTime;		// We decrease or increase alpha depends direction.
		alpha = Mathf.Clamp01 (this.alpha);							// Values between 0 and 1.
		
		GUI.color = new Color (GUI.color.r, GUI.color.g, GUI.color.b, this.alpha);
		GUI.depth = depth;	// <-- There we put the priority of GUI paint
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), this.fadeOutTexture);
	}
	
	// Call Fading to Draw onGUI depends the direction you choose.
	public float Fading(int direction){
		this.direction = direction;
		return 1.75f;	// <-- Return the time aprox. in seconds that we expet that late the animation.
	}
	
	// When I collider with the invisible wall I call a corutine and load next level.
	void OnTriggerEnter (Collider other){
		StartCoroutine(corutineFade());
	}
	
	IEnumerator corutineFade() {
		float time = Fading (1);
		yield return new WaitForSeconds(time);
		Application.LoadLevel(5);
	}
	
	IEnumerator corutineFadeNoLoad() {
		float time = Fading (1);
		yield return new WaitForSeconds(time);
	}
	
	public void fade_out() {
		StartCoroutine(corutineFadeNoLoad());
	}
}
