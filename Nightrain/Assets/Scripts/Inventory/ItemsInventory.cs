using UnityEngine;
using System.Collections.Generic;

public class ItemsInventory : MonoBehaviour {

	private static Dictionary<int, Item> weapon_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> shield_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> helmet_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> armor_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> boots_list = new Dictionary<int, Item>();
	private static Dictionary<int, Item> healing_list = new Dictionary<int, Item>();


	// === WEAPON ===
	
	public static void addWeapon(int id, Item item){
		
		if (!ItemsInventory.weapon_list.ContainsKey (id)) {
			ItemsInventory.weapon_list.Add (id, item);
		}
	}
	
	public static Weapon getWeapon(int id){
		
		Weapon weapon = new Weapon ();
		weapon.id = ItemsInventory.weapon_list [id].id;
		weapon.name = ItemsInventory.weapon_list [id].name;
		weapon.type = ItemsInventory.weapon_list [id].type;
		weapon.VIT = ItemsInventory.weapon_list [id].VIT;
		weapon.PM = ItemsInventory.weapon_list [id].PM;
		weapon.FRZ = ItemsInventory.weapon_list [id].FRZ;
		weapon.DEF = ItemsInventory.weapon_list [id].DEF;
		weapon.SPD = ItemsInventory.weapon_list [id].SPD;
		weapon.ItemTexture = ItemsInventory.weapon_list [id].ItemTexture;
		weapon.width = ItemsInventory.weapon_list [id].width;
		weapon.height = ItemsInventory.weapon_list [id].height;
		
		return weapon;
	}


	// === SHIELD ===

	public static void addShield(int id, Item item){
		
		if (!ItemsInventory.shield_list.ContainsKey (id)) {
			ItemsInventory.shield_list.Add (id, item);
		}
	}

	public static Shield getShield(int id){
		
		Shield shield = new Shield ();
		shield.id = ItemsInventory.shield_list [id].id;
		shield.name = ItemsInventory.shield_list [id].name;
		shield.type = ItemsInventory.shield_list [id].type;
		shield.VIT = ItemsInventory.shield_list [id].VIT;
		shield.PM = ItemsInventory.shield_list [id].PM;
		shield.FRZ = ItemsInventory.shield_list [id].FRZ;
		shield.DEF = ItemsInventory.shield_list [id].DEF;
		shield.SPD = ItemsInventory.shield_list [id].SPD;
		shield.ItemTexture = ItemsInventory.shield_list [id].ItemTexture;
		shield.width = ItemsInventory.shield_list [id].width;
		shield.height = ItemsInventory.shield_list [id].height;
		
		return shield;
	}

	// === HELMET ===
	
	public static void addHelmet(int id, Item item){
		
		if (!ItemsInventory.helmet_list.ContainsKey (id)) {
			ItemsInventory.helmet_list.Add (id, item);
		}
	}
	
	public static Helmet getHelmet(int id){
		
		Helmet helmet = new Helmet ();
		helmet.id = ItemsInventory.helmet_list [id].id;
		helmet.name = ItemsInventory.helmet_list [id].name;
		helmet.type = ItemsInventory.helmet_list [id].type;
		helmet.VIT = ItemsInventory.helmet_list [id].VIT;
		helmet.PM = ItemsInventory.helmet_list [id].PM;
		helmet.FRZ = ItemsInventory.helmet_list [id].FRZ;
		helmet.DEF = ItemsInventory.helmet_list [id].DEF;
		helmet.SPD = ItemsInventory.helmet_list [id].SPD;
		helmet.ItemTexture = ItemsInventory.helmet_list [id].ItemTexture;
		helmet.width = ItemsInventory.helmet_list [id].width;
		helmet.height = ItemsInventory.helmet_list [id].height;
		
		return helmet;
	}


	// === ARMOR ===
	
	public static void addArmor(int id, Item item){
		
		if (!ItemsInventory.armor_list.ContainsKey (id)) {
			ItemsInventory.armor_list.Add (id, item);
		}
	}
	
	public static Armor getArmor(int id){
		
		Armor armor = new Armor ();
		armor.id = ItemsInventory.armor_list [id].id;
		armor.name = ItemsInventory.armor_list [id].name;
		armor.type = ItemsInventory.armor_list [id].type;
		armor.VIT = ItemsInventory.armor_list [id].VIT;
		armor.PM = ItemsInventory.armor_list [id].PM;
		armor.FRZ = ItemsInventory.armor_list [id].FRZ;
		armor.DEF = ItemsInventory.armor_list [id].DEF;
		armor.SPD = ItemsInventory.armor_list [id].SPD;
		armor.ItemTexture = ItemsInventory.armor_list [id].ItemTexture;
		armor.width = ItemsInventory.armor_list [id].width;
		armor.height = ItemsInventory.armor_list [id].height;
		
		return armor;
	}


	// === BOOTS ===
	
	public static void addBoots(int id, Item item){
		
		if (!ItemsInventory.boots_list.ContainsKey (id)) {
			ItemsInventory.boots_list.Add (id, item);
		}
	}
	
	public static Boots getBoots(int id){
		
		Boots boots = new Boots ();
		boots.id = ItemsInventory.boots_list [id].id;
		boots.name = ItemsInventory.boots_list [id].name;
		boots.type = ItemsInventory.boots_list [id].type;
		boots.VIT = ItemsInventory.boots_list [id].VIT;
		boots.PM = ItemsInventory.boots_list [id].PM;
		boots.FRZ = ItemsInventory.boots_list [id].FRZ;
		boots.DEF = ItemsInventory.boots_list [id].DEF;
		boots.SPD = ItemsInventory.boots_list [id].SPD;
		boots.ItemTexture = ItemsInventory.boots_list [id].ItemTexture;
		boots.width = ItemsInventory.boots_list [id].width;
		boots.height = ItemsInventory.boots_list [id].height;
		
		return boots;
	}


	// === HEALING ===
	
	public static void addHealing(int id, Item item){
		
		if (!ItemsInventory.healing_list.ContainsKey (id)) {
			ItemsInventory.healing_list.Add (id, item);
		}
	}
	
	public static Healing getHealing(int id){
		
		Healing healing = new Healing ();
		healing.id = ItemsInventory.healing_list [id].id;
		healing.name = ItemsInventory.healing_list [id].name;
		healing.type = ItemsInventory.healing_list [id].type;
		healing.VIT = ItemsInventory.healing_list [id].VIT;
		healing.PM = ItemsInventory.healing_list [id].PM;
		healing.FRZ = ItemsInventory.healing_list [id].FRZ;
		healing.DEF = ItemsInventory.healing_list [id].DEF;
		healing.SPD = ItemsInventory.healing_list [id].SPD;
		healing.heal = ItemsInventory.healing_list [id].heal;
		healing.magic = ItemsInventory.healing_list [id].magic;
		healing.ItemTexture = ItemsInventory.healing_list [id].ItemTexture;
		healing.width = ItemsInventory.healing_list [id].width;
		healing.height = ItemsInventory.healing_list [id].height;
		
		return healing;
	}


}
