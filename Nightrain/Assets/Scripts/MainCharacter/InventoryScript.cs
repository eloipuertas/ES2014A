using UnityEngine;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	// MINIMAPS
	private miniMapLv1 map_lvl1;
	private miniMapLv2 map_lvl2;

	// MEMORY CARD 
	private MemoryCard mc;
	private SaveData save;
	private LoadData load;

	private bool pause = false;

	// ========== INVENTORY ============
	public Rect inventory_box;
	private List<Item> list_inventory;
	private Item[] equip;
	private bool show_inventory = false;

	// ========== SLOT INVENTORY ============

	// WEAPON REGION
	public Rect weaponRegion_box;
	private float weaponX = 210;	//20
	private float weaponY = 192;	//45
	private float weaponW = 65;	//56
	private float weaponH = 157;	//112

	// SHIELD REGION
	public Rect shieldRegion_box;
	private float shieldX = 383;	//250
	private float shieldY = 192;	//45
	private float shieldW = 65;	//56
	private float shieldH = 157;	//112

	// HELMET REGION
	public Rect helmetRegion_box;
	private float helmetX = 308;	
	private float helmetY = 66;	
	private float helmetW = 50;	
	private float helmetH = 58;	

	// HELMET REGION
	public Rect armorRegion_box;
	private float armorX = 0;	
	private float armorY = 0;	
	private float armorW = 0;	
	private float armorH = 0;	
	
	// BOOTS REGION
	public Rect bootRegion_box;
	private float bootX = 293;	
	private float bootY = 395;	
	private float bootW = 71;	
	private float bootH = 81;	

	// ITEM REGION
	public Rect slotRegion_box;
	private Slot[,] slot;
	private Rect[,] s;
	private int slot_row = 11;  //11
	private int slot_column = 5; //5
	private float slotX = 15; //17
	private float slotY = 522; //255
	private float slot_w = 43; //29
	private float slot_h = 43; // 29
	private Vector2 slot_selected;
	private Vector2 last_slot;


	// ========== ITEMS ============
	private Item temp_item;


	// ========== TEXTURES ============
	private Texture2D inventoryTexture;

	private string character;

	private CharacterScript cs;
	private ClickToMove cm;
	private ClickToMove_lvl2 cm2;

	private GUIStyle attributes_style;
	private GUIStyle level_style;
	private GUIStyle exp_style;
	private GUIStyle guiStyleBack;

	// Use this for initialization
	void Start () {
	
		this.character = PlayerPrefs.GetString ("Player");

		this.list_inventory = new List<Item>();
		this.equip = new Item[5]; //"Weapon, Shield, Helmet, Armor, Boots"
		this.inventoryTexture = Resources.Load<Texture2D>("Inventory/Misc/inventory_" + this.character);
		this.map_lvl1 = GameObject.FindGameObjectWithTag ("Minimap").GetComponent<miniMapLv1> ();
		this.map_lvl2 = GameObject.FindGameObjectWithTag ("Minimap").GetComponent<miniMapLv2> ();
		//this.prueba = Resources.Load<Texture2D>("Inventory/Misc/slot");

		this.cs = GameObject.FindGameObjectWithTag ("Player").GetComponent<CharacterScript> ();
		this.cm = GameObject.FindGameObjectWithTag ("Player").GetComponent<ClickToMove> ();
		this.cm2 = GameObject.FindGameObjectWithTag ("Player").GetComponent<ClickToMove_lvl2> ();

		// Memory Card Save/Load data
		this.mc = GameObject.FindGameObjectWithTag ("MemoryCard").GetComponent<MemoryCard> ();
		this.save = this.mc.saveData();
		this.load = this.mc.loadData();

		// STYLES TEXT
		this.attributes_style = new GUIStyle ();
		//this.text_style.font = Resources.Load<Font>("MainMenu/avqest");
		this.attributes_style.fontStyle = FontStyle.Bold;
		this.attributes_style.normal.textColor = new Color (236f/255f,219f/255f,31f/255f);
		this.attributes_style.fontSize = 20;

		this.exp_style = new GUIStyle ();
		//this.text_style.font = Resources.Load<Font>("MainMenu/avqest");
		this.exp_style.fontStyle = FontStyle.Bold;
		this.exp_style.normal.textColor = new Color (236f/255f,219f/255f,31f/255f);
		this.exp_style.fontSize = 17;

		this.level_style = new GUIStyle ();
		//this.text_style.font = Resources.Load<Font>("MainMenu/avqest");
		this.level_style.fontStyle = FontStyle.Bold;
		this.level_style.normal.textColor = new Color (236f/255f,219f/255f,31f/255f);
		this.level_style.fontSize = 13;

		// CREATE INVENTORY
		this.resizeInventory ();
		this.createInventorySlot ();

		int num = this.load.loadNumItemsInventory ();

		if (num > 0)
			this.load.loadInventoryItems (num);

		this.equip = load.loadEquipItem ();
	}
	

	void Update(){
		
		if (Input.GetKeyDown (KeyCode.I) && !show_inventory && !pause){ 
			if(map_lvl1 != null && map_lvl1.showMiniMap()){
				map_lvl1.setShowMiniMap(false);
				show_inventory = true;
			}else
				show_inventory = true;

			if(map_lvl2 != null && map_lvl2.showMiniMap()){
				map_lvl2.setShowMiniMap(false);
				show_inventory = true;
			}else
				show_inventory = true;

		}else if(Input.GetKeyDown (KeyCode.I) && show_inventory && !pause){
			show_inventory = false;
		}

		// If cursor is inside inventory action "Don't walk" is outside walk
		if(this.cm != null)
			if (this.inventory_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y)) && show_inventory)
				this.cm.dontWalk();
			else
				this.cm.Walk();

		if(this.cm2 != null)
			if (this.inventory_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y)) && show_inventory)
				this.cm2.dontWalk();
		else
			this.cm2.Walk();
	}


	// Method that create a grid with the position of every slot
	void createInventorySlot(){
		
		// COORDENATES INVENTORY REGION
		this.inventory_box.x = Screen.width - this.inventory_box.width;
		this.inventory_box.y = 0;//Screen.height - this.inventory_box.height - Screen.height*.2f;
		
		// SLOTS REGIONS
		
		
		//WEAPON REGION
		this.weaponRegion_box = new Rect (this.inventory_box.x + weaponX,
		                                  this.inventory_box.y + weaponY,
		                                  weaponW,
		                                  weaponH);
		
		// SHIELD REGION
		this.shieldRegion_box = new Rect (this.inventory_box.x + shieldX,
		                                  this.inventory_box.y + shieldY,
		                                  shieldW,
		                                  shieldH);

		//HELMET REGION
		this.helmetRegion_box = new Rect (this.inventory_box.x + helmetX,
		                                  this.inventory_box.y + helmetY,
		                                  helmetW,
		                                  helmetH);

		//ARMOR REGION
		this.armorRegion_box = new Rect (this.inventory_box.x + armorX,
		                                  this.inventory_box.y + armorY,
		                                  armorW,
		                                  armorH);

		// BOOT REGION
		this.bootRegion_box = new Rect (this.inventory_box.x + bootX,
		                                  this.inventory_box.y + bootY,
		                                  bootW,
		                                  bootH);

		
		// ITEMS REGION
		this.slot = new Slot[this.slot_row, this.slot_column];
		this.s = new Rect[this.slot_row, this.slot_column];
		
		for (int i = 0; i < this.slot_row; i++)
		for (int j = 0; j < this.slot_column; j++){

			this.slot [i, j] = new Slot (new Rect(this.slotX + (this.slot_w*i),
			                                      this.slotY + (this.slot_h*j),
			                                      this.slot_w,
			                                      this.slot_h));
			
			this.s[i,j] = new Rect(this.inventory_box.x + this.slot[i,j].position.x,
			                       this.inventory_box.y + this.slot[i,j].position.y,
			                       this.slot_w,
			                       this.slot_h);
		}
	}

	void inventoryEngine(){

		for (int i = 0; i < this.slot_row; i++) {
			for (int j = 0; j < this.slot_column; j++) {

				if (this.inventory_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {

					if (this.slotRegion_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {

						if (s [i, j].Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))) {

							// This condition is to understand the drag and drop with the items
							if (Event.current.isMouse && Input.GetMouseButtonDown (0)) {
								this.slot_selected.x = i;
								this.slot_selected.y = j;

								// Loop all the items to the inventory
								for (int index = 0; index < this.list_inventory.Count; index++)
									// Loop the rows that item ocupped in the inventory
									for (int x = this.list_inventory[index].x; x < this.list_inventory[index].x + this.list_inventory[index].width; x++)
										// Loop the columns that item ocupped in the inventory
										for (int y = this.list_inventory[index].y; y < this.list_inventory[index].y + this.list_inventory[index].height; y++)
											// if the slot where I click save the item
											if (x == i && y == j) {
												this.temp_item = this.list_inventory [index];
												this.removeItem (this.temp_item);
												return;
											}

							} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {
					
								this.last_slot.x = i;
								this.last_slot.y = j;

								//this.checkRemoveItem(this.temp_item);
								//print ("Debug 1");
								// If the slots are diferents drag the item to the new slot position
								if (this.slot_selected.x != this.last_slot.x ||
									this.slot_selected.y != this.last_slot.y) {
									//print ("Debug 2");
									//this.checkRemoveItem(this.temp_item);

									if (this.temp_item != null) {
										if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
											//print ("Debug 3");
										} else {
											if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
												//print ("Debug 4");
												this.checkRemoveItem(this.temp_item);
											}else{
												//print ("Debug 5");
												this.checkUnquipedItem(this.temp_item);
												this.temp_item = null;
											}
											//print ("Debug 6");
										}	
										//print ("Debug 7");
										this.checkRemoveItem(this.temp_item);
										//this.checkUnquipedItem(null);
										this.temp_item = null;
									}
								
								} else {
									if (this.temp_item != null) {
										//print ("Debug 8");
										//this.checkUnquipedItem(this.temp_item);
										this.checkRemoveItem(this.temp_item); // <-- Esto es lo ultimo k he tocado
										this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item);
										this.temp_item = null;
									}
									//print ("Debug 9");
								}
							} 
							return;
						}
					}else if(this.weaponRegion_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))){

						if (Event.current.isMouse && Input.GetMouseButtonDown (0)) {
							this.temp_item = this.equip[0];
							this.removeWeapon ();
							//print ("Debug 10");
							return;
							
						} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {

							if (this.equip[0] == null && temp_item is Weapon) {
								this.equip[0] = this.temp_item;
								EquipWeapons.setWeapon((Weapon)this.equip[0]);
								this.removeItem(this.equip[0]);
								this.temp_item = null;
								//print ("Debug 11");
							}else{

								this.last_slot.x = i;
								this.last_slot.y = j;
								//print ("Debug 12");
								// If the slots are diferents drag the item to the new slot position
								if (this.temp_item != null) {
									if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
										//print ("Debug 13");
									} else {
										if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
											//print ("Debug 14");
											if(equip[0] == this.temp_item)
												this.checkRemoveItem(equip[0]);
										}else{
											if(this.temp_item is Weapon){
												this.equip[0] = this.temp_item;
												//print ("Debug 15");
												this.temp_item = null;
											}
										}
									}	

									this.temp_item = null;
									//print ("Debug 16");
								}

							}
						}

					}else if(this.shieldRegion_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
						
						if (Event.current.isMouse && Input.GetMouseButtonDown (0)) {
							this.temp_item = this.equip[1];
							this.removeShield ();
							//print ("Debug 17");
							return;
							
						} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {

							if (this.equip[1] == null && temp_item is Shield) {
								this.equip[1] = this.temp_item;
								//print ("Debug 18");
								EquipWeapons.setShield((Shield)this.equip[1]);
								this.removeItem(this.equip[1]);
								this.temp_item = null;

							}else{
								//print ("Debug 19");
								this.last_slot.x = i;
								this.last_slot.y = j;
								
								// If the slots are diferents drag the item to the new slot position
								if (this.temp_item != null) {
									if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
										//print ("Debug 20");
									} else {
										if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
											//print ("Debug 21");
											if(equip[1] == this.temp_item)
												this.checkRemoveItem(equip[1]);
										}
										else{
											//print ("Debug 22");
											//print("Unequip Shield");
											if(this.temp_item is Shield){
												this.equip[1] = this.temp_item;
												this.temp_item = null;
											}
										}
									}	
									//print ("Debug 23");
									this.temp_item = null;		
								}
								
							}
						}
						
					}else if(this.helmetRegion_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
						
						if (Event.current.isMouse && Input.GetMouseButtonDown (0)) {
							this.temp_item = this.equip[2];
							this.removeHelmet ();
							//print ("Debug 17");
							return;
							
						} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {
							
							if (this.equip[2] == null && temp_item is Helmet) {
								this.equip[2] = this.temp_item;
								//print ("Debug 18");
								EquipWeapons.setHelmet((Helmet)this.equip[2]);
								this.removeItem(this.equip[2]);
								this.temp_item = null;
								
							}else{
								//print ("Debug 19");
								this.last_slot.x = i;
								this.last_slot.y = j;
								
								// If the slots are diferents drag the item to the new slot position
								if (this.temp_item != null) {
									if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
										//print ("Debug 20");
									} else {
										if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
											//print ("Debug 21");
											if(equip[2] == this.temp_item)
												this.checkRemoveItem(equip[2]);
										}
										else{
											//print ("Debug 22");
											//print("Unequip Shield");
											this.equip[2] = this.temp_item;
											this.temp_item = null;
										}
									}	
									//print ("Debug 23");
									this.temp_item = null;		
								}
								
							}
						}
						
					}else if(this.armorRegion_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
						
						if (Event.current.isMouse && Input.GetMouseButtonDown (0)) {
							this.temp_item = this.equip[3];
							this.removeArmor ();
							//print ("Debug 17");
							return;
							
						} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {
							
							if (this.equip[3] == null && temp_item is Armor) {
								this.equip[3] = this.temp_item;
								//print ("Debug 18");
								EquipWeapons.setArmor((Armor)this.equip[3]);
								this.removeItem(this.equip[3]);
								this.temp_item = null;
								
							}else{
								//print ("Debug 19");
								this.last_slot.x = i;
								this.last_slot.y = j;
								
								// If the slots are diferents drag the item to the new slot position
								if (this.temp_item != null) {
									if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
										//print ("Debug 20");
									} else {
										if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
											//print ("Debug 21");
											if(equip[3] == this.temp_item)
												this.checkRemoveItem(equip[3]);
										}
										else{
											//print ("Debug 22");
											//print("Unequip Shield");
											this.equip[3] = this.temp_item;
											this.temp_item = null;
										}
									}	
									//print ("Debug 23");
									this.temp_item = null;		
								}
								
							}
						}
						
					}else if(this.bootRegion_box.Contains (new Vector2 (Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
						
						if (Event.current.isMouse && Input.GetMouseButtonDown (0)) {
							this.temp_item = this.equip[4];
							this.removeBoot ();
							//print ("Debug 17");
							return;
							
						} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {
							
							if (this.equip[4] == null && temp_item is Boots) {
								this.equip[4] = this.temp_item;
								//print ("Debug 18");
								EquipWeapons.setBoots((Boots)this.equip[4]);
								this.removeItem(this.equip[4]);
								this.temp_item = null;
								
							}else{
								//print ("Debug 19");
								this.last_slot.x = i;
								this.last_slot.y = j;
								
								// If the slots are diferents drag the item to the new slot position
								if (this.temp_item != null) {
									if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
										//print ("Debug 20");
									} else {
										if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
											//print ("Debug 21");
											if(equip[4] == this.temp_item)
												this.checkRemoveItem(equip[4]);
										}
										else{
											//print ("Debug 22");
											//print("Unequip Shield");
											this.equip[4] = this.temp_item;
											this.temp_item = null;
										}
									}	
									//print ("Debug 23");
									this.temp_item = null;		
								}
								
							}
						}
						
					} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {

						this.last_slot.x = i;
						this.last_slot.y = j;
						//print ("Debug 24");
						// If the slots are diferents drag the item to the new slot position
						if (this.temp_item != null) {

							if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
								//print ("Debug 25");
								this.checkRemoveItem(this.temp_item);
							} else {
								if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
									//print ("Debug 26");
									this.checkRemoveItem(this.temp_item);
								}else{
									//print ("Debug 27");
									this.checkUnquipedItem(this.temp_item);
									this.temp_item = null;
								}
							}	
							//print ("Debug 28");
							this.temp_item = null;

						}		
					} 
						
				} else if (Event.current.isMouse && Input.GetMouseButtonUp (0)) {

					this.last_slot.x = i;
					this.last_slot.y = j;
					//print ("Debug 29");

					// If the slots are diferents drag the item to the new slot position
					if (this.temp_item != null) {
						if (this.addItem ((int)last_slot.x, (int)last_slot.y, this.temp_item)) {
							//print ("Debug 30");
							this.checkRemoveItem(this.temp_item);
						} else {
							if(this.addItem (this.temp_item.x, this.temp_item.y, this.temp_item)){
								//print ("Debug 31");
								this.checkRemoveItem(this.temp_item);
							}else{
								//print ("Debug 32");
								this.checkUnquipedItem(this.temp_item);
								this.temp_item = null;
							}
						}	
						//print ("Debug 33");
						this.temp_item = null;
					}
				}
			}
		}		

	}

	void checkUnquipedItem(Item item){

		if(item is Weapon){
			this.equip[0] = item;
		}else if(item is Shield){
			this.equip[1] = item;
		}else if(item is Helmet){
			this.equip[2] = item;
		}else if(item is Armor){
			this.equip[3] = item;
		}else if(item is Boots){
			this.equip[4] = item;
		}
	}

	void checkRemoveItem(Item item){


		if(item != null && item is Weapon){
			EquipWeapons.removeWeapon(item);
		}else if(item != null && item is Shield){
			EquipWeapons.removeShield(item);
		}else if(item != null && item is Helmet){
			EquipWeapons.removeHelmet(item);
		}else if(item != null && item is Armor){
			EquipWeapons.removeArmor(item);
		}else if(item != null && item is Boots){
			EquipWeapons.removeBoots(item);
		}
			
	}

	private int potion = 0;

	public void setPotion(int potion){				

		this.potion += potion;

		if (potion < 0)
			for (int i = 0; i < this.list_inventory.Count; i++)
				if (this.list_inventory [i].id == 1) {
					this.removeItem (list_inventory [i]);
				return;					
				}

	}

	public int getPotion(){				
		return potion;
	}


	// ====================================
	// ======== ADD/REMOVE ITEMS ==========
	// ====================================

	public bool addItem(Item item){
	
		for (int i = 0; i < this.slot_row; i++)
			for (int j = 0; j < this.slot_column; j++)
				if (addItem (i, j, item)) 
					return true;
	
		Debug.Log ("Inventory fully");
		return false;
	}

	public bool addItem(int x, int y, Item item){

		// Comprove if the grid is empty.
		for (int i = 0; i < item.width; i++)
			for (int j = 0; j < item.height; j++)
				if (slot [x, y].available) {
					//Debug.Log ("Ocupado en " + x + "," + y );
					return false;
				}



		// Comprove if the item is outside of the grid in X and Y direction.
		if (x + item.width > this.slot_row)
			return false;
		else if (y + item.height > this.slot_column)
			return false;

		//Debug.Log("New Armor");
		//Debug.Log ("Añadido en " + x + "," + y );

		item.x = x;
		item.y = y;
		this.list_inventory.Add (item);

		// Mark the position where the new item is assigned.
		for (int i = x; i < item.width + x; i++)
			for (int j = y; j < item.height + y; j++)
					slot[i,j].available = true;
				
		return true;
	}


	public void removeItem(Item item){

		// Dismark the position where the item was assigned.
		for (int i = item.x; i < item.width + item.x; i++)
			for (int j = item.y; j < item.height + item.y; j++)
				slot[i,j].available = false;

		this.list_inventory.Remove (item);

	}

	void removeWeapon(){
		//print ("Unequip: " + this.equip[0].ItemTexture.name.ToString());
		this.equip[0] = null;
	}

	void removeShield(){
		//print ("Unequip: " + this.equip[1].ItemTexture.name.ToString());
		this.equip[1] = null;
	}

	void removeHelmet(){
		//print ("Unequip: " + this.equip[0].ItemTexture.name.ToString());
		this.equip[2] = null;
	}

	void removeArmor(){
		//print ("Unequip: " + this.equip[1].ItemTexture.name.ToString());
		this.equip[3] = null;
	}
	
	void removeBoot(){
		//print ("Unequip: " + this.equip[1].ItemTexture.name.ToString());
		this.equip[4] = null;
	}



	// ====================================
	// ========== DRAW METHODS ============
	// ====================================

	void drawDragItem(){

		if (this.temp_item != null)
			GUI.DrawTexture (new Rect (Input.mousePosition.x,
			                           Screen.height - Input.mousePosition.y,
			                           this.temp_item.width * this.slot_w,
			                           this.temp_item.height * this.slot_h),
			                 this.temp_item.ItemTexture);

		/*if(temp_item != null)
			print ("Width: " + this.temp_item.width + "\nHeight: " + this.temp_item.height);*/
	}
	
	void drawItems(){

		for (int i = 0; i < this.list_inventory.Count; i++)
			GUI.DrawTexture (new Rect(this.inventory_box.x + slotX + (slot_w * this.list_inventory[i].x) + 4,
			                          this.inventory_box.y + slotY + (slot_h * this.list_inventory[i].y) + 4,
			                          (slot_w * this.list_inventory[i].width) - 8,
			                          (slot_h * this.list_inventory[i].height) - 8),
			                 this.list_inventory[i].ItemTexture);
	}
	

	void drawWeapon(){

		//GUI.DrawTexture(this.shieldRegion_box,this.prueba);
		if(this.equip[0] != null)
			GUI.DrawTexture (this.weaponRegion_box, this.equip [0].ItemTexture);
	}

	void drawShield(){

		//GUI.DrawTexture(this.weaponRegion_box,this.prueba);
		if(this.equip[1] != null)
			GUI.DrawTexture (this.shieldRegion_box, this.equip [1].ItemTexture);
	}

	void drawHelmet(){
		
		//GUI.DrawTexture(this.helmetRegion_box,this.prueba);
		if(this.equip[2] != null)
			GUI.DrawTexture (this.helmetRegion_box, this.equip [2].ItemTexture);
	}

	void drawArmor(){
		
		//GUI.DrawTexture(this.armorRegion_box,this.prueba);
		if(this.equip[3] != null)
			GUI.DrawTexture (this.armorRegion_box, this.equip [3].ItemTexture);
	}

	void drawBoot(){
		
		//GUI.DrawTexture(this.bootRegion_box,this.prueba);
		if(this.equip[4] != null)
			GUI.DrawTexture (this.bootRegion_box, this.equip [4].ItemTexture);
	}

	void drawSlots(){

		for (int i = 0; i < this.slot_row; i++)
			for (int j = 0; j < this.slot_column; j++){
				this.slot [i, j].drawSlot (this.inventory_box.x,
				                           this.inventory_box.y);
				//GUI.DrawTexture(this.s[i,j],this.prueba);
			}

	}

	void drawInventory(){

		this.slotRegion_box = new Rect (this.inventory_box.x + this.slotX,
		                                this.inventory_box.y + this.slotY,
		                                this.slot_row * this.slot_w,
		                                this.slot_column * this.slot_h);

		GUI.DrawTexture (this.inventory_box, this.inventoryTexture);
	}

	void drawAttributes(){

		// LABEL LEVEL VALUES
		GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (415), 
		                     this.inventory_box.y + resizeSlotY (66), 
		                     resizeSlotX (75), 
		                     resizeSlotY (25)),
		           this.cs.getLVL().ToString(),
		           this.level_style);

		// LABEL VIT VALUES
		if(this.cs.getHealth () >= 0)
			GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (122), 
			                    this.inventory_box.y + resizeSlotY (132), 
			                    resizeSlotX (75), 
			                    resizeSlotY (25)),
			           this.cs.getHealth ().ToString(),
			           this.attributes_style);
		else if(this.cs.getHealth () < 0)
			GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (122), 
			                     this.inventory_box.y + resizeSlotY (132), 
			                     resizeSlotX (75), 
			                     resizeSlotY (25)),
			           "0",
			           this.attributes_style);

		// LABEL PM VALUES
		GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (111), 
		                     this.inventory_box.y + resizeSlotY (170), 
		                     resizeSlotX (75), 
		                     resizeSlotY (25)),
		           Mathf.FloorToInt(this.cs.getMagic ()).ToString(),
		           this.attributes_style);

		// LABEL FRZ VALUES
		GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (125), 
		                     this.inventory_box.y + resizeSlotY (207), 
		                     resizeSlotX (75), 
		                     resizeSlotY (25)),
		           this.cs.getFRZ ().ToString(),
		           this.attributes_style);

		// LABEL DEF VALUES
		GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (125), 
		                     this.inventory_box.y + resizeSlotY (245), 
		                     resizeSlotX (75), 
		                     resizeSlotY (25)),
		           this.cs.getDEF ().ToString(),
		           this.attributes_style);

		// LABEL SPD VALUES
		GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (125), 
		                     this.inventory_box.y + resizeSlotY (285), 
		                     resizeSlotX (75), 
		                     resizeSlotY (25)),
		           this.cs.getSPD ().ToString(),
		           this.attributes_style);

		// LABEL EXP VALUES
		GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (124), 
		                     this.inventory_box.y + resizeSlotY (325), 
		                     resizeSlotX (150), 
		                     resizeSlotY (25)),
		           this.cs.getEXP().ToString(),
		           this.exp_style);

		// LABEL NEXT EXP VALUES
		GUI.Label (new Rect (this.inventory_box.x + resizeSlotX (58), 
		                     this.inventory_box.y + resizeSlotY (441), 
		                     resizeSlotX (150), 
		                     resizeSlotY (25)),
		           "Next: " + this.cs.getNextExpLevel().ToString(),
		           this.level_style);
	}


	// Update is called once per frame
	void OnGUI () {

		if (show_inventory) {
			this.drawInventory ();
			this.drawAttributes();
			this.drawSlots ();
			this.drawItems ();
			this.drawWeapon();
			this.drawShield();
			this.drawHelmet();
			this.drawArmor();
			this.drawBoot();
			this.drawDragItem ();
			this.inventoryEngine ();
		}
	}

	// Method to adapt all inventory to the scene.
	void resizeInventory(){

		this.inventory_box.width = resizeWidth (this.inventory_box.width);
		this.inventory_box.height = Screen.height;
		
		slotX = resizeSlotX(slotX);
		slotY = resizeSlotY(slotY); 
		slot_w = resizeSlotX(slot_w); 
		slot_h = resizeSlotY(slot_h); 

		weaponX = resizeSlotX(weaponX);
		weaponY = resizeSlotY(weaponY);
		weaponW = resizeSlotX(weaponW);
		weaponH = resizeSlotY(weaponH);

		shieldX = resizeSlotX(shieldX);
		shieldY = resizeSlotY(shieldY);
		shieldW = resizeSlotX(shieldW);
		shieldH = resizeSlotY(shieldH);

		helmetX = resizeSlotX(helmetX);
		helmetY = resizeSlotY(helmetY);
		helmetW = resizeSlotX(helmetW);
		helmetH = resizeSlotY(helmetH);

		if (this.character == "hombre") {
			armorX = resizeSlotX(291);
			armorY = resizeSlotY(213);
			armorW = resizeSlotX(84);
			armorH = resizeSlotY(115);
		}else if (this.character == "mujer") {
			armorX = resizeSlotX(293);
			armorY = resizeSlotY(191);
			armorW = resizeSlotX(68);
			armorH = resizeSlotY(125);
		}else if (this.character == "joven") {
			armorX = resizeSlotX(290);
			armorY = resizeSlotY(238);
			armorW = resizeSlotX(85);
			armorH = resizeSlotY(95);
		}
		
		bootX = resizeSlotX(bootX);
		bootY = resizeSlotY(bootY);
		bootW = resizeSlotX(bootW);
		bootH = resizeSlotY(bootH);	
		
	
	}

	public void saveInventory(){

		this.save.saveNumItemsInventory (list_inventory.Count);
		this.save.saveEquipedItem (equip);
		
		for (int i = 0; i < list_inventory.Count; i++) {
			this.save.saveInventoryItem(i, list_inventory[i]);
		}

	}

	public void loadInventory(){


	}

	public void setPause(bool pause){
		this.pause = pause;
	}

	public void setShowInventory(bool show){
		show_inventory = show;
	}
	
	public bool showInventory(){
		return show_inventory;
	}

	private float resizeWidth(float width){
		return ((Screen.width * width) / (reference_width * 1.0f));
	}

	private float resizeSlotX(float width){
		return ((this.inventory_box.width * (width/this.inventoryTexture.width))/* / (reference_width * 1.0f)*/);
	}


	
	private float resizeSlotY(float height){
		return ((this.inventory_box.height * (height/this.inventoryTexture.height))/* / (reference_height * 1.0f)*/);
	}

	private float resizeHeight(float height){
		return ((Screen.height * height) / (reference_height * 1.0f));
	}
	
}
