using UnityEngine;
using System.Collections;

public class EquipWeapons : MonoBehaviour {

	public GameObject weapon;
	public GameObject shield;

	private static GameObject w;
	private static GameObject s;
	private static Helmet h = null;
	private static Armor a = null;
	private static Boots b = null;

	private static Transform weaponTransform;
	private static Transform shieldTransform;

	private static ItemDrop item;

	// ========== CHARACTER ============
	private GameObject character;
	private static CharacterScript cs;

	void Start(){

		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		cs = this.character.GetComponent<CharacterScript> ();

		weaponTransform = weapon.transform;
		shieldTransform = shield.transform;
 
	}
	

	public static void setWeapon(Weapon weapon){

		if(w == null){
			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
			cs.setFRZ(weapon.FRZ);
		}else{
			item = w.GetComponent<ItemDrop> ();
			cs.setFRZ(-item.FRZ);
			Destroy(w);
			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
			cs.setFRZ(weapon.FRZ);
		}
		//w.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

	}
	

	public static void removeWeapon(Item i){

		if(w != null){
			item = w.GetComponent<ItemDrop> ();

			if(item.id == i.id){
				cs.setFRZ(-item.FRZ);
				Destroy (w);
			}
		}
	}


	public static void setShield(Shield shield){

		if(s == null){
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
			cs.setDEF(shield.DEF);
		}else{
			item = s.GetComponent<ItemDrop> ();
			cs.setDEF(-item.DEF);
			Destroy(s);
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
			cs.setDEF(shield.DEF);
		}
		//s.transform.rotation = Quaternion.Euler(new Vector3(20, 160, 0));
	}


	public static void removeShield(Item i){

		if(s != null){
			item = s.GetComponent<ItemDrop> ();

			if(i.id == item.id){
				cs.setDEF(-item.DEF);
				Destroy (s);
			}
		}
	}

	public static void setHelmet(Helmet helmet){
		
		if(h == null){
			h = helmet;
			cs.setDEF(helmet.DEF);
			cs.setVIT(helmet.VIT);
			cs.setPM(helmet.PM);
		}else{
			cs.setDEF(-helmet.DEF);
			cs.setVIT(-helmet.VIT);
			cs.setPM(-helmet.PM);
			h = helmet;
			cs.setDEF(helmet.DEF);
			cs.setVIT(helmet.VIT);
			cs.setPM(helmet.PM);
		}
		//s.transform.rotation = Quaternion.Euler(new Vector3(20, 160, 0));
	}
	
	
	public static void removeHelmet(Item i){
		
		if(h != null){
			if(i.id == h.id){
				cs.setDEF(-h.DEF);
				cs.setVIT(-h.VIT);
				cs.setPM(-h.PM);
				h = null;
			}
		}
	}

	public static void setArmor(Armor armor){
		
		if(a == null){
			a = armor;
			cs.setDEF(armor.DEF);
			cs.setVIT(armor.VIT);
			cs.setPM(armor.PM);
		}else{
			cs.setDEF(-armor.DEF);
			cs.setVIT(-armor.VIT);
			cs.setPM(-armor.PM);
			a = armor;
			cs.setDEF(armor.DEF);
			cs.setVIT(armor.VIT);
			cs.setPM(armor.PM);
		}
		//s.transform.rotation = Quaternion.Euler(new Vector3(20, 160, 0));
	}
	
	
	public static void removeArmor(Item i){
		
		if(a != null){
			if(i.id == a.id){
				cs.setDEF(-a.DEF);
				cs.setVIT(-a.VIT);
				print ("Remove Armor: " + -a.VIT);
				cs.setPM(-a.PM);
				a = null;
			}
		}
	}

	
	public static void setBoots(Boots boot){
		
		if(b == null){
			b = boot;
			cs.setSPD(boot.SPD);
		}else{
			cs.setSPD(-boot.SPD);
			b = boot;
			cs.setSPD(boot.SPD);
		}
		//s.transform.rotation = Quaternion.Euler(new Vector3(20, 160, 0));
	}
	
	
	public static void removeBoots(Item i){
		
		if(b != null){
			if(i.id == b.id){
				cs.setSPD(-b.SPD);
				b = null;
			}
		}
	}


}
