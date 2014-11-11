using UnityEngine;
using System.Collections;

public class ItemInfo : MonoBehaviour {

	public string text_item;
	private GUIStyle text_style;
	private GUIStyle guiStyleBack;
	
	// Use this for initialization
	void Start () {
		this.text_item = "";
		this.text_style = new GUIStyle ();
		this.text_style.normal.textColor = Color.white;
		this.text_style.fontSize = 15;
		this.text_style.alignment = TextAnchor.UpperCenter ; 
		this.text_style.wordWrap = true; 
	}
	
	void OnMouseEnter () {	
		this.text_item = this.gameObject.name;
	}

	void OnMouseExit () { 
		this.text_item = ""; 
	}

	void OnGUI() { 
		if (this.text_item != "") { 
			var x = Event.current.mousePosition.x; 
			var y = Event.current.mousePosition.y; 
			GUI.Label (new Rect (x-150,y+20,300,60), this.text_item, this.text_style); 
		} 
	}

}
