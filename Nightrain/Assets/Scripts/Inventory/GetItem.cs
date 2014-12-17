using UnityEngine;
using System.Collections;

public class GetItem : MonoBehaviour {
	
	public static Weapon setWeapon(int id, string name, string type, int VIT, int PM, int FRZ,
	                                int DEF, int SPD, string icon_name, int slot_x, int slot_y){
		
		Weapon weapon = new Weapon ();
		weapon.id = id;
		weapon.name = name;
		weapon.type = type;
		weapon.VIT = VIT;
		weapon.PM = PM;
		weapon.FRZ = FRZ;
		weapon.DEF = DEF;
		weapon.SPD = SPD;
		weapon.ItemTexture = Resources.Load<Texture2D>("Inventory/"+type+"/"+icon_name);
		weapon.width = slot_x;
		weapon.height = slot_y;
		
		return weapon;
		
	}
	
	public static Shield setShield(int id, string name, string type, int VIT, int PM, int FRZ,
	                               int DEF, int SPD, string icon_name, int slot_x, int slot_y){
		
		Shield shield = new Shield ();
		shield.id = id;
		shield.name = name;
		shield.type = type;
		shield.VIT = VIT;
		shield.PM = PM;
		shield.FRZ = FRZ;
		shield.DEF = DEF;
		shield.SPD = SPD;
		shield.ItemTexture = Resources.Load<Texture2D>("Inventory/"+type+"/"+icon_name);
		shield.width = slot_x;
		shield.height = slot_y;

		return shield;
		
	}

	public static Helmet setHelmet(int id, string name, string type, int VIT, int PM, int FRZ,
	                               int DEF, int SPD, string icon_name, int slot_x, int slot_y){
		
		Helmet helmet = new Helmet ();
		helmet.id = id;
		helmet.name = name;
		helmet.type = type;
		helmet.VIT = VIT;
		helmet.PM = PM;
		helmet.FRZ = FRZ;
		helmet.DEF = DEF;
		helmet.SPD = SPD;
		helmet.ItemTexture = Resources.Load<Texture2D>("Inventory/"+type+"/"+icon_name);
		helmet.width = slot_x;
		helmet.height = slot_y;
		
		return helmet;
		
	}
	
	public static Armor setArmor(int id, string name, string type, int VIT, int PM, int FRZ,
	                             int DEF, int SPD, string icon_name, int slot_x, int slot_y){
		
		Armor armor = new Armor ();
		armor.id = id;
		armor.name = name;
		armor.type = type;
		armor.VIT = VIT;
		armor.PM = PM;
		armor.FRZ = FRZ;
		armor.DEF = DEF;
		armor.SPD = SPD;
		armor.ItemTexture = Resources.Load<Texture2D>("Inventory/"+type+"/"+icon_name);
		armor.width = slot_x;
		armor.height = slot_y;

		return armor;
	}

	public static Boots setBoots(int id, string name, string type, int VIT, int PM, int FRZ,
	                             int DEF, int SPD, string icon_name, int slot_x, int slot_y){
		
		Boots boots = new Boots ();
		boots.id = id;
		boots.name = name;
		boots.type = type;
		boots.VIT = VIT;
		boots.PM = PM;
		boots.FRZ = FRZ;
		boots.DEF = DEF;
		boots.SPD = SPD;
		boots.ItemTexture = Resources.Load<Texture2D>("Inventory/"+type+"/"+icon_name);
		boots.width = slot_x;
		boots.height = slot_y;
		
		return boots;
		
	}
	
	public static Healing setHealing(int id, string name, string type, int VIT, int PM, int FRZ,
	                                 int DEF, int SPD, int heal, int magic, string icon_name, 
	                                 int slot_x, int slot_y){
		
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
		healing.magic = magic;
		healing.ItemTexture = Resources.Load<Texture2D>("Inventory/"+type+"/"+icon_name);
		healing.width = slot_x;
		healing.height = slot_y;

		return healing;
	}

}
