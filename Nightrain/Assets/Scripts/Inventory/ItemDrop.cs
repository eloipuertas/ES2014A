using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {

	public static InventoryScript inventory;

	public int id;
	public Texture2D imageTexture;
	public int slot_x;
	public int slot_y;

	private int itemRep;
	private Item item;

	/*void awake(){

		item = new Armor ();
		this.item.id = id;
		this.item.ItemTexture = imageTexture;
		this.item.width = slot_x;
		this.item.height = slot_y;
	}*/

	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(){

		Armor armor = new Armor();
		armor.id = id;
		armor.ItemTexture = imageTexture;
		armor.width = slot_x;
		armor.height = slot_y;
		ItemsInventory.addArmor (armor.id, armor);
		inventory = GameObject.FindGameObjectWithTag("Inventory").GetComponent<InventoryScript> ();
		if (armor.id == 4)
			inventory.setPotion (1);
		inventory.addItem(ItemsInventory.getArmor (id));
		Destroy (gameObject);
	}
}
