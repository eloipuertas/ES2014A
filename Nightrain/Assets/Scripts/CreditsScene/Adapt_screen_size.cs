using UnityEngine;
using System.Collections;

public class Adapt_screen_size : MonoBehaviour {
	private float ratio;
	private float screen_ratio;

	// Use this for initialization
	void Start () {
		ratio = 8f / 6f;
		screen_ratio = (float) Screen.width / (float) Screen.height;
		adaptToScreen ();
	}
	
	// Update is called once per frame
	void Update () {
		screen_ratio = Screen.width / Screen.height;
		adaptToScreen ();
	}

	void adaptToScreen () {
		if (Mathf.Abs (ratio - screen_ratio) > 0f) {
			float widthScale =  (float) Screen.width / (float) Screen.height;
			float heightScale = (float) Screen.height / (float) Screen.width;

			float heightScale2 =  Camera.main.orthographicSize * 2.0f;
			float widthScale2 = heightScale2 * (float) Screen.width / (float) Screen.height;

			transform.localScale = new Vector3 (widthScale2-(widthScale2/2.6f), 1f, 36f);
			//transform.localScale = new Vector3 (8f, 1f, 36f*heightScale);
			//float y_move = (36f - 36f*heightScale) / 2;
			//transform.position = new Vector3 (0f, 0f, 0f+y_move);
		}
	}
}
