using UnityEngine;
using System.Collections;

public class CursorScript_lvl2 : MonoBehaviour {

	private Texture2D[] cursorTexture;
	private CursorMode mode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;

	public static bool isHover = false;

	// Use this for initialization
	void Start () {
		// ADD CURSOR
		this.cursorTexture = new Texture2D[2];
		this.cursorTexture[0] = Resources.Load<Texture2D>("Misc/cursor");
		this.cursorTexture[1] = Resources.Load<Texture2D>("Misc/cursor_click");
		Cursor.SetCursor(cursorTexture[0], hotSpot, mode);
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0) && !isHover) 
			Cursor.SetCursor(cursorTexture[1], hotSpot, mode);
		else if(Input.GetMouseButtonUp(0) && !isHover)
			Cursor.SetCursor(cursorTexture[0], hotSpot, mode);
		
	}
}
