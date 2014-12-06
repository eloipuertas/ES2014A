using UnityEngine;
using System.Collections;

public class ItemDrop_lvl2 : MonoBehaviour {

	private CharacterScript_lvl2 cs;
	private GameObject character;

	private RaycastHit getObjectScene;

	public static InventoryScript_lvl2 inventory;



	public int id;
	public string name;
	public string type;			// Type: Weapon, Shield, Armor, Healing
	public int VIT;
	public int PM;
	public int FRZ;
	public int DEF;
	public int SPD;
	public int heal;
	public Texture2D imageTexture;
	public int slot_x;
	public int slot_y;

	public float distance = 10;
		


	void Start(){
		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript_lvl2> ();
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<InventoryScript_lvl2> ();

	}


	void OnMouseDown() {

		Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );
		RaycastHit hit;

		if( Physics.Raycast( ray, out hit, 100 ) ){

			if (Vector3.Distance (this.character.transform.position, hit.transform.position) < this.distance) {

				if(this.type.Equals("Weapon"))
					this.TypeWeapon();
				else if(this.type.Equals("Shield"))
					this.TypeShield();
				else if(this.type.Equals("Armor"))
					this.TypeArmor();
				else if(this.type.Equals("Healing"))
					this.TypeHealing();

			}
		}
	}

	void TypeWeapon(){
		
		Weapon weapon = new Weapon ();
		weapon.id = id;
		weapon.name = name;
		weapon.type = type;
		weapon.VIT = VIT;
		weapon.PM = PM;
		weapon.FRZ = FRZ;
		weapon.DEF = DEF;
		weapon.SPD = SPD;
		weapon.ItemTexture = imageTexture;
		weapon.width = slot_x;
		weapon.height = slot_y;
		
		ItemsInventory.addWeapon (id, weapon);
		inventory.addItem (ItemsInventory.getWeapon (id));
		Destroy (gameObject);
		
	}

	void TypeShield(){
		
		Shield shield = new Shield ();
		shield.id = id;
		shield.name = name;
		shield.type = type;
		shield.VIT = VIT;
		shield.PM = PM;
		shield.FRZ = FRZ;
		shield.DEF = DEF;
		shield.SPD = SPD;
		shield.ItemTexture = imageTexture;
		shield.width = slot_x;
		shield.height = slot_y;
		
		ItemsInventory.addShield (id, shield);
		inventory.addItem (ItemsInventory.getShield (id));
		Destroy (gameObject);
		
	}

	void TypeArmor(){

		Armor armor = new Armor ();
		armor.id = id;
		armor.name = name;
		armor.type = type;
		armor.VIT = VIT;
		armor.PM = PM;
		armor.FRZ = FRZ;
		armor.DEF = DEF;
		armor.SPD = SPD;
		armor.ItemTexture = imageTexture;
		armor.width = slot_x;
		armor.height = slot_y;

		ItemsInventory.addArmor (id, armor);
		inventory.addItem (ItemsInventory.getArmor (id));
		Destroy (gameObject);

	}

	void TypeHealing(){
		
		Healing healing = new Healing ();
		healing.id = id;
		healing.name = name;
		healing.type = type;
		healing.VIT = VIT;
		healing.PM = PM;
		healing.FRZ = FRZ;
		healing.DEF = DEF;
		healing.SPD = SPD;
		healing.heal = heal;
		healing.ItemTexture = imageTexture;
		healing.width = slot_x;
		healing.height = slot_y;
		
		ItemsInventory.addHealing (id, healing);
		inventory.setPotion (1);
		inventory.addItem (ItemsInventory.getHealing (id));
		Destroy (gameObject);
		
	}

}
