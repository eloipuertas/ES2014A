using UnityEngine;
using System.Collections;

[System.Serializable]
public class Slot {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	public Item item;
	public bool available;
	public Rect position;


	public Slot(Rect slot){
		this.position = slot;
	}

	public void drawSlot(float x, float y){


		if (this.item != null) {
			Rect inventory_box = new Rect (x + position.x,
	               y + position.y, 
	               position.width,
	               position.height);

			GUI.DrawTexture (inventory_box, this.item.ItemTexture);
		}
	}

	private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}
}
