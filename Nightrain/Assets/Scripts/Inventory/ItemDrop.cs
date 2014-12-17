using UnityEngine;
using System.Collections;

public class ItemDrop : MonoBehaviour {

	private CharacterScript cs;
	private GameObject character;

	private RaycastHit getObjectScene;

	public static InventoryScript inventory;

	public int id;
	public string name;
	public string type;			// Type: Weapon, Shield, Armor, Healing
	public int VIT;
	public int PM;
	public int FRZ;
	public int DEF;
	public int SPD;
	public int heal;
	public int magic;
	public Texture2D imageTexture;
	public int slot_x;
	public int slot_y;

	public float distance = 10;
		


	void Start(){
		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		this.cs = this.character.GetComponent<CharacterScript> ();
		inventory = GameObject.FindGameObjectWithTag ("Inventory").GetComponent<InventoryScript> ();

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
				else if(this.type.Equals("Helmet"))
					this.TypeHelmet();
				else if(this.type.Equals("Armor"))
					this.TypeArmor();
				else if(this.type.Equals("Boots"))
					this.TypeBoots();
				else if(this.type.Equals("Healing"))
					this.TypeHealing();

			}
		}
	}

	void TypeWeapon(){
		
		/*Weapon weapon = new Weapon ();
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
		weapon.height = slot_y;*/
		if (name == "Buster Sword") {
			Music_Engine_Script music = GameObject.FindGameObjectWithTag("music_engine").GetComponent<Music_Engine_Script>();
			TrophyEngine trofeo = GameObject.FindGameObjectWithTag("Trofeos").GetComponent<TrophyEngine>();
			music.play_BusterSword();
			trofeo.TrophyBusterSword();
		}

		Weapon weapon = GetItem.setWeapon (id, name, type, VIT, PM, FRZ, DEF, SPD, imageTexture.name, slot_x, slot_y);
		ItemsInventory.addWeapon (id, weapon);

		if(inventory.addItem (ItemsInventory.getWeapon (id))){
			Destroy (gameObject);
		}else
			print ("Full inventory.");

		
	}

	void TypeShield(){
		
		/*Shield shield = new Shield ();
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
		shield.height = slot_y;*/

		Shield shield = GetItem.setShield (id, name, type, VIT, PM, FRZ, DEF, SPD, imageTexture.name, slot_x, slot_y);
		ItemsInventory.addShield (id, shield);

		if(inventory.addItem (ItemsInventory.getShield (id))){
			Destroy (gameObject);
		}else
			print ("Full inventory.");

		
	}

	void TypeHelmet(){
		
		/*Helmet helmet = new Helmet ();
		helmet.id = id;
		helmet.name = name;
		helmet.type = type;
		helmet.VIT = VIT;
		helmet.PM = PM;
		helmet.FRZ = FRZ;
		helmet.DEF = DEF;
		helmet.SPD = SPD;
		helmet.ItemTexture = imageTexture;
		helmet.width = slot_x;
		helmet.height = slot_y;*/
		Helmet helmet = GetItem.setHelmet (id, name, type, VIT, PM, FRZ, DEF, SPD, imageTexture.name, slot_x, slot_y);
		ItemsInventory.addHelmet (id, helmet);

		if(inventory.addItem (ItemsInventory.getHelmet (id))){
			Destroy (gameObject);
		}else
			print ("Full inventory.");

		
	}

	void TypeArmor(){

		/*Armor armor = new Armor ();
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
		armor.height = slot_y;*/
		Armor armor = GetItem.setArmor (id, name, type, VIT, PM, FRZ, DEF, SPD, imageTexture.name, slot_x, slot_y);
		ItemsInventory.addArmor (id, armor);

		if(inventory.addItem (ItemsInventory.getArmor (id))){
			Destroy (gameObject);
		}else
			print ("Full inventory.");


	}

	void TypeBoots(){
		
		/*Boots boots = new Boots ();
		boots.id = id;
		boots.name = name;
		boots.type = type;
		boots.VIT = VIT;
		boots.PM = PM;
		boots.FRZ = FRZ;
		boots.DEF = DEF;
		boots.SPD = SPD;
		boots.ItemTexture = imageTexture;
		boots.width = slot_x;
		boots.height = slot_y;*/
		Boots boots = GetItem.setBoots (id, name, type, VIT, PM, FRZ, DEF, SPD, imageTexture.name, slot_x, slot_y);
		ItemsInventory.addBoots (id, boots);


		if(inventory.addItem (ItemsInventory.getBoots (id))){
			Destroy (gameObject);
		}else
			print ("Full inventory.");
		
		
	}

	void TypeHealing(){
		
		/*Healing healing = new Healing ();
		healing.id = id;
		healing.name = name;
		healing.type = type;
		healing.VIT = VIT;
		healing.PM = PM;
		healing.FRZ = FRZ;
		healing.DEF = DEF;
		healing.SPD = SPD;
		healing.heal = heal;
		healing.magic = magic;
		healing.ItemTexture = imageTexture;
		healing.width = slot_x;
		healing.height = slot_y;*/
		Healing healing = GetItem.setHealing (id, name, type, VIT, PM, FRZ, DEF, SPD, heal, magic, imageTexture.name, slot_x, slot_y);
		ItemsInventory.addHealing (id, healing);

		if(inventory.addItem (ItemsInventory.getHealing (id))){
			inventory.setPotion (1);
			Destroy (gameObject);
		}else
			print ("Full inventory.");
		
	}

}
