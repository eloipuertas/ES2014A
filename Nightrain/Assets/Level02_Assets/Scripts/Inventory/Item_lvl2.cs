using UnityEngine;
using System.Collections;

[System.Serializable]
public abstract class Item_lvl2 {

	public int id;
	public string name;
	public string type;			// Type: Weapon, shield, armor, healings
	public int VIT;
	public int PM;
	public int FRZ;
	public int DEF;
	public int SPD;
	public int heal;
	public Texture2D ItemTexture;
	public int x;				// Position X in the inventory
	public int y;				// Position Y in the iventoty
	public int width;
	public int height;

	// Method when I click a Item
	public abstract void actionPerform();

}
