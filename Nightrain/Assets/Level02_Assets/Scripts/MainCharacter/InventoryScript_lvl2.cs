using UnityEngine;
using System.Collections.Generic;

public class InventoryScript_lvl2 : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	// ========== INVENTORY ============
	public Rect inventory_box;
	private List<Item> list_inventory;
	private Item[] equip;
	private bool show_inventory = false;

	// ========== SLOT INVENTORY ============

	// WEAPON REGION
	public Rect weaponRegion_box;
	private const int weaponX = 20;
	private const int weaponY = 45;
	private const int weaponW = 56;
	private const int weaponH = 112;

	// SHIELD REGION
	public Rect shieldRegion_box;
	private const int shieldX = 250;
	private const int shieldY = 45;
	private const int shieldW = 56;
	private const int shieldH = 112;

	// ITEM REGION
	public Rect slotRegion_box;
	private Slot[,] slot;
	private Rect[,] s;
	private int slot_row = 10;
	private int slot_column = 4;
	private int slotX = 17;
	private int slotY = 255;
	private int slot_w = 29;
	private int slot_h = 29;
	private Vector2 slot_selected;
	private Vector2 last_slot;


	// ========== ITEMS ============
	private Item temp_item;


	// ========== TEXTURES ============
	private Texture2D inventoryTexture;


	// Use this for initialization
	void Start () {
	
		this.list_inventory = new List<Item>();
		this.equip = new Item[3];
		this.inventoryTexture = Resources.Load<Texture2D>("Inventory/Misc/inventory");

		// CREATE INVENTORY
		this.createInventorySlot ();

	}
	

	void Update(){

		if (Input.GetKeyDown (KeyCode.I) && !this.show_inventory) 
			this.show_inventory = true;
		else if(Input.GetKeyDown (KeyCode.I) && this.show_inventory)
			this.show_inventory = false;
	}


	// Method that create a grid with the position of every slot
	void createInventorySlot(){
		
		// COORDENATES INVENTORY REGION
		this.inventory_box.x = Screen.width - this.inventory_box.width;
		this.inventory_box.y = Screen.height - this.inventory_box.height - Screen.height*.2f;
		
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
											this.equip[0] = this.temp_item;
											//print ("Debug 15");
											this.temp_item = null;
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
												this.checkRemoveItem(equip[0]);
										}
										else{
											//print ("Debug 22");
											//print("Unequip Shield");
											this.equip[1] = this.temp_item;
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
		}
	}

	void checkRemoveItem(Item item){


		if(item != null && item is Weapon){
				EquipWeapons.removeWeapon(item);
			//this.cs.setFRZ(-1*(item.FRZ));
		}else if(item != null && item is Shield){
				EquipWeapons.removeShield(item);
			//this.cs.setDEF((-1 * item.DEF));
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

	bool addItem(int x, int y, Item item){

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


	void removeItem(Item item){

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

		if(this.equip[0] != null)
			GUI.DrawTexture (this.weaponRegion_box, this.equip [0].ItemTexture);
	}

	void drawShield(){
		
		if(this.equip[1] != null)
			GUI.DrawTexture (this.shieldRegion_box, this.equip [1].ItemTexture);
	}

	void drawSlots(){

		for (int i = 0; i < this.slot_row; i++)
			for (int j = 0; j < this.slot_column; j++)
				this.slot [i, j].drawSlot (this.inventory_box.x,
				                           this.inventory_box.y);

	}

	void drawInventory(){

		this.slotRegion_box = new Rect (this.inventory_box.x + this.slotX,
		                                this.inventory_box.y + this.slotY,
		                                this.slot_row * this.slot_w,
		                                this.slot_column * this.slot_h);

		GUI.DrawTexture (this.inventory_box, this.inventoryTexture);
	}


	// Update is called once per frame
	void OnGUI () {

		if (this.show_inventory) {
			this.drawInventory ();
			this.drawSlots ();
			this.drawItems ();
			this.drawWeapon();
			this.drawShield();
			this.drawDragItem ();
			this.inventoryEngine ();
		}
	}
	
}
