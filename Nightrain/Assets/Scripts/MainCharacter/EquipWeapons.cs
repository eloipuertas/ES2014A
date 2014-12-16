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

	private Vector3 weaponRotation;
	private Vector3 weaponPosition;
	private static bool weaponRotated = false;
	private static string weaponName = "";

	private Vector3 shieldRotation;
	private Vector3 shieldPosition;
	private static bool shieldRotated = false;
	private static string shieldName = "";

	void Start(){

		
		this.character = GameObject.FindGameObjectWithTag ("Player");
		cs = this.character.GetComponent<CharacterScript> ();

		weaponTransform = weapon.transform;
		shieldTransform = shield.transform;
 
	}

	void Update() {

		if (w != null && !weaponRotated) {
			setWeaponRotation();
		}

		if (s != null && !shieldRotated) {
			setShieldRotation();
		}
	}
	

	public static void setWeapon(Weapon weapon){
		//if(weapon.name.Equals("Iron Axe"));
		weaponName = weapon.name;

		if(w == null){
			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
			cs.setFRZ(weapon.FRZ);
			weaponRotated = false;
		}else{
			item = w.GetComponent<ItemDrop> ();
			cs.setFRZ(-item.FRZ);
			Destroy(w);

			w = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Weapons/" + weapon.name)) as GameObject;
			w.transform.position = weaponTransform.position;
			w.transform.parent = weaponTransform;
			cs.setFRZ(weapon.FRZ);
			weaponRotated = false;
		}
		//w.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

	}

	private void setWeaponRotation () {
		switch (weaponName) {
			case "Iron Axe":
				weaponRotation = new Vector3(90f,1f,1f);
				weaponPosition = new Vector3(-0.07f,-0.05f,-0.008f);
				break;
			case "Baston de mago":
				weaponRotation = new Vector3(90f,1f,1f);
				weaponPosition = new Vector3(-0.02f,-0.0001f,0.01f);
				break;
			case "Buster Sword":
				weaponRotation = new Vector3(1f,1f,180f);
				if (PlayerPrefs.GetString ("Player").Equals("joven")) {
					weaponPosition = new Vector3(0f,0.1f,0.05f);
				} else if (PlayerPrefs.GetString ("Player").Equals("hombre")) {
					weaponPosition = new Vector3(-0.07f,0.07f,0.02f);	
				} else if (PlayerPrefs.GetString ("Player").Equals("mujer")) {
					weaponPosition = new Vector3(-0.025f,0.03f,0.015f);
				}
				break;
			case "Daga":
				weaponRotation = new Vector3(1,180f,180f);
				weaponPosition = new Vector3(0f,-0.25f,0.02f);
				break;
		}


		w.transform.localPosition = weaponPosition;
		w.transform.localEulerAngles = weaponRotation;

		weaponRotated = true;
	}

	private void setShieldRotation () {
		switch (shieldName) {
			case "Leather Shield":
				if (PlayerPrefs.GetString ("Player").Equals("joven")) {
					shieldRotation = new Vector3(0f,0f,15f);
					shieldPosition = new Vector3(0f,-0.05f,-0.05f);
				} else if (PlayerPrefs.GetString ("Player").Equals("hombre")) {
					shieldRotation = new Vector3(0f,0f,15f);
					shieldPosition = new Vector3(0f,-0.05f,-0.05f);
				} else if (PlayerPrefs.GetString ("Player").Equals("mujer")) {
					shieldRotation = new Vector3(0f,0f,15f);
					shieldPosition = new Vector3(0f,-0.05f,-0.05f);
				}

			break;
		}
		
		//s.transform.localPosition = shieldPosition;
		s.transform.localEulerAngles = shieldRotation;
		
		shieldRotated = true;
	}


	public static void removeWeapon(Item i){

		if(w != null){
			item = w.GetComponent<ItemDrop> ();

			if(item.id == i.id){
				cs.setFRZ(-item.FRZ);
				Destroy (w);
			}
			weaponRotated = false;
		}
	}


	public static void setShield(Shield shield){

		shieldName = shield.name;

		if(s == null){
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
			cs.setDEF(shield.DEF);
			shieldRotated = false;
		}else{
			item = s.GetComponent<ItemDrop> ();
			cs.setDEF(-item.DEF);
			Destroy(s);
			s = Instantiate(Resources.Load<GameObject>("Prefabs/Inventory/Shields/" + shield.name)) as GameObject;
			s.transform.position = shieldTransform.position;
			s.transform.parent = shieldTransform;
			cs.setDEF(shield.DEF);
			shieldRotated = false;
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
			shieldRotated = false;
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
