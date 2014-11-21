using UnityEngine;
using System.Collections;

public class GameObjectHover : MonoBehaviour {

	private Texture2D[] cursorTexture;
	private CursorMode mode = CursorMode.Auto;
	private Vector2 hotSpot = Vector2.zero;

	// Use this for initialization
	void Start () {
		// ADD CURSOR
		this.cursorTexture = new Texture2D[2];
		this.cursorTexture[0] = Resources.Load<Texture2D>("Misc/cursor_axe");
		this.cursorTexture[1] = Resources.Load<Texture2D>("Misc/cursor_click");
		Cursor.SetCursor(cursorTexture[1], hotSpot, mode);
	
	}

	void OnMouseEnter(){
		CursorScript.isHover = true;
		Cursor.SetCursor(cursorTexture[0], hotSpot, mode);
	}

	void OnMouseExit() {
		CursorScript.isHover = false;
		Cursor.SetCursor(cursorTexture[1], hotSpot, mode);
	}

	void OnDestroy() {
		CursorScript.isHover = false;
		//Cursor.SetCursor(cursorTexture[1], hotSpot, mode);
	}
}
