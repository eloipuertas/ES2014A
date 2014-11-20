using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {

	private CharacterScript cs;
	private GameObject character;

	private RaycastHit getObjectScene;

	public static InventoryScript inventory;

	public int id;
	public Texture2D imageTexture;
	public int slot_x;
	public int slot_y;

	public float distance = 10;

	private int itemRep;
	private Item item;


	void Start(){
		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();

	}


	void OnMouseDown() {

		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;

		if( Physics.Raycast( ray, out hit, 100 ) ){

			if (Vector3.Distance (this.character.transform.position, hit.transform.position) < this.distance) {
					
				Armor armor = new Armor ();
				armor.id = id;
				armor.ItemTexture = imageTexture;
				armor.width = slot_x;
				armor.height = slot_y;
				ItemsInventory.addArmor (armor.id, armor);
				inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<InventoryScript> ();
				if (armor.id == 4)
						inventory.setPotion (1);
				inventory.addItem (ItemsInventory.getArmor (id));
				Destroy (gameObject);

			}
		}
	}

}
