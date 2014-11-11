using UnityEngine;
using System.Collections.Generic;

public class InventoryScript : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	// ========== INVENTORY ============
	public Rect inventory_box;
	public Rect slotRegion_box;
	private List<Item> list_inventory;


	// ========== SLOT INVENTORY ============
	private Slot[,] slot;
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

	private bool show_inventory = false;

	// Use this for initialization
	void Start () {
	
		this.list_inventory = new List<Item>();
		this.inventoryTexture = Resources.Load<Texture2D>("Inventory/Misc/inventory");

		// CREATE INVENTORY SLOT
		this.createInventorySlot ();

		//this.addItem (0, 0, ItemsInventory.getArmor(0));
		//this.addItem (0, 2, ItemsInventory.getArmor(1));

	}
	

	void Update(){

		if (Input.GetKeyDown (KeyCode.I) && !this.show_inventory) 
			this.show_inventory = true;
		else if(Input.GetKeyDown (KeyCode.I) && this.show_inventory)
			this.show_inventory = false;
	}

	void inventoryFocused(){
		if (this.inventory_box.Contains (Input.mousePosition)) {
			this.slotFocused();
			//Debug.Log ("Disable ClickToMove");
			return;
		}
		//Debug.Log ("Enable ClickToMove");

		// Click to Move disable
	}

	void slotFocused(){

		for (int i = 0; i < this.slot_row; i++){
			for (int j = 0; j < this.slot_column; j++) {

				Rect s = new Rect(this.inventory_box.x + slot[i,j].position.x,
				                     this.inventory_box.y + slot[i,j].position.y,
				                     this.slot_w,
				                     this.slot_h);

				if(this.slotRegion_box.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){
					if(s.Contains(new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y))){

						// This condition is to understand the drag and drop with the items
						if(Event.current.isMouse && Input.GetMouseButtonDown(0)){
							this.slot_selected.x = i;
							this.slot_selected.y = j;

							// Loop all the items to the inventory
							for(int index = 0; index < this.list_inventory.Count; index++)
								// Loop the rows that item ocupped in the inventory
								for(int x = this.list_inventory[index].x; x < this.list_inventory[index].x + this.list_inventory[index].width; x++)
									// Loop the columns that item ocupped in the inventory
									for(int y = this.list_inventory[index].y; y < this.list_inventory[index].y + this.list_inventory[index].height; y++)
										// if the slot where I click save the item
										if(x == i && y == j){
											this.temp_item = this.list_inventory[index];
											this.removeItem(this.temp_item);
											return;
										}

						}else if(Event.current.isMouse && Input.GetMouseButtonUp(0)){

							this.last_slot.x = i;
							this.last_slot.y = j;

							// If the slots are diferents drag the item to the new slot position
							if(this.slot_selected.x != this.last_slot.x ||
							   this.slot_selected.y != this.last_slot.y){

								if(this.temp_item != null){
									if(this.addItem((int)last_slot.x, (int)last_slot.y, this.temp_item)){

									}else{
										this.addItem(this.temp_item.x, this.temp_item.y, this.temp_item);
									}	
									this.temp_item = null;
								}
							}else{
								if(this.temp_item != null){
									this.addItem(this.temp_item.x, this.temp_item.y, this.temp_item);
									this.temp_item = null;
								}
							}

						} 
						return;
					}
				}else if(Event.current.isMouse && Input.GetMouseButtonUp(0)){
					
					this.last_slot.x = i;
					this.last_slot.y = j;
					
					// If the slots are diferents drag the item to the new slot position

						
					if(this.temp_item != null){
						if(this.addItem((int)last_slot.x, (int)last_slot.y, this.temp_item)){
							
						}else{
							this.addItem(this.temp_item.x, this.temp_item.y, this.temp_item);
						}	
						this.temp_item = null;
					}		
				} 
			}
		}			
	}

	private int potion = 0;

	public void setPotion(int potion){				

		this.potion += potion;
		if (potion < 0)
			for (int i = 0; i < this.list_inventory.Count; i++)
				if (this.list_inventory [i].ItemTexture.name == "icon_potion") {
					this.removeItem (list_inventory [i]);
				return;					
			}
	}

	public int getPotion(){				
		return potion;
	}

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

	// Method that create a grid with the position of every slot
	void createInventorySlot(){
		
		this.slot = new Slot[this.slot_row, this.slot_column];
		//Slot.test = test;
		
		for (int i = 0; i < this.slot_row; i++)
			for (int j = 0; j < this.slot_column; j++)
				this.slot [i, j] = new Slot (new Rect(slotX + (this.slot_w*i),
				                                      slotY + (this.slot_h*j),
				                                      slot_w,
				                                      slot_h));
	}

	void drawSlots(){

		for (int i = 0; i < this.slot_row; i++)
			for (int j = 0; j < this.slot_column; j++)
				this.slot [i, j].drawSlot (this.inventory_box.x,
				                           this.inventory_box.y);

	}

	void drawInventory(){
		/*this.inventory_box = new Rect (Screen.width - resizeTextureWidth(this.inventoryTexture),
		                               Screen.height/2 - resizeTextureHeight(this.inventoryTexture)/2,
		                               resizeTextureWidth(this.inventoryTexture),
		                               resizeTextureHeight(this.inventoryTexture));*/

		this.inventory_box.x = Screen.width - this.inventory_box.width;
		this.inventory_box.y = Screen.height - this.inventory_box.height - Screen.height*.2f;

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
			this.drawDragItem ();
			this.inventoryFocused ();
		}
	}
	/*private float resizeTextureWidth(Texture2D texture){
		return ((Screen.width * texture.width) / (reference_width * 1.0f));
	}
	
	private float resizeTextureHeight(Texture2D texture){
		return ((Screen.height * texture.height) / (reference_height * 1.0f));
	}*/
}
