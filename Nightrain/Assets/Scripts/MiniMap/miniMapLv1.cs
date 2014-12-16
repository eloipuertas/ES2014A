using UnityEngine;
using System.Collections;

public class miniMapLv1 : MonoBehaviour {

	private const int reference_width = 1366; 
	private const int reference_height = 598;

	private bool pause = false;

	// ====== TEXTURES MINIMAP ======
	public Texture2D miniMapTexture;

	// ====== TEXTURES ICONS OBJECTS ======
	private Texture2D playerIcon;
	private Texture2D enemyIcon;
	private Texture2D bossIcon;
	private Texture2D chestIcon;
	private Texture2D openChestIcon;

	// ====== GAMEOBJECTS SCENE ======
	private GameObject character;
	private GameObject[] enemies;
	private GameObject[] chest;
	private GameObject boss;

	private InventoryScript inventory;

	//The width and height of your map as it'll appear on screen,
	public float mapWidth = 200;
	public float mapHeight = 200;
	//Width and Height of your scene, or the resolution of your terrain.
	public float sceneWidth = 500;
	public float sceneHeight = 500;
	//The size of your player and enemy's icon as it would appear on the map. 
	public float iconSize = 10;
	private float iconHalfSize;
	//This values is because the terrain has 150 of offset to the second terrain.
	private int offset = 150;

	private static bool mapVisible = false;

	// Use this for initialization
	void Start () {

		// REFERENCE GAMEOBJECTS
		this.character = GameObject.FindGameObjectWithTag("Player");
		this.inventory = character.GetComponentInChildren <InventoryScript> ();
		this.enemies = GameObject.FindGameObjectsWithTag("Enemy");
		this.boss = GameObject.FindGameObjectWithTag("Boss");
		this.chest = GameObject.FindGameObjectsWithTag("Chest");

		// LOAD TEXTURES
		this.playerIcon = Resources.Load<Texture2D>("MiniMap/character_v2");
		this.enemyIcon = Resources.Load<Texture2D>("MiniMap/enemy");
		this.bossIcon = Resources.Load<Texture2D>("MiniMap/boss");
		this.chestIcon = Resources.Load<Texture2D>("MiniMap/chest");
		this.openChestIcon = Resources.Load<Texture2D>("MiniMap/chest_open");

		mapVisible = false;
	}
	
	// Update is called once per frame
	void Update () {
		this.iconHalfSize = this.iconSize/2;

		if (Input.GetKeyDown (KeyCode.M) && !mapVisible && !pause){ 
			if(inventory.showInventory()){
				inventory.setShowInventory(false);
				mapVisible = true;
			}else
				mapVisible = true;
		}else if (Input.GetKeyDown (KeyCode.M) && mapVisible && !pause) 
			mapVisible = false;
	}
	

	void OnGUI() {

		if(mapVisible){

			Rect minimap_box = new Rect (Screen.width - resizeWidth (mapWidth), 
			                            0, 
			                            resizeWidth (mapWidth), 
			                            resizeHeight (mapHeight));
			GUI.DrawTexture(minimap_box, miniMapTexture);

			if(boss != null){
				float bossX = GetMapPos(boss.transform.position.x, mapWidth, sceneWidth);
				float bossZ = GetMapPos(offset+boss.transform.position.z, mapHeight, sceneHeight);
				float bossMapX = bossX - iconHalfSize;
				float bossMapZ = ((bossZ * -1) - iconHalfSize) + mapHeight;

				GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(bossMapX), 
				                         resizeHeight(bossMapZ), 
				                         resizeWidth(iconSize), 
				                         resizeHeight(iconSize)), 
				                bossIcon);
			}

			for(int i = 0; i < enemies.Length; i++){
				if(enemies[i] != null){
					float enemyX = GetMapPos(enemies[i].transform.position.x, mapWidth, sceneWidth);
					float enemyZ = GetMapPos(offset+enemies[i].transform.position.z, mapHeight, sceneHeight);
					float enemyMapX = enemyX - iconHalfSize;
					float enemyMapZ = ((enemyZ * -1) - iconHalfSize) + mapHeight;

					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(enemyMapX), 
					                         resizeHeight(enemyMapZ), 
					                         resizeWidth(iconSize), 
					                         resizeHeight(iconSize)), 
					                enemyIcon);
				}
			}

			for(int i = 0; i < chest.Length; i++){
				float cX = GetMapPos(chest[i].transform.position.x, mapWidth, sceneWidth);
				float cZ = GetMapPos(offset+chest[i].transform.position.z, mapHeight, sceneHeight);
				float chestMapX = cX - iconHalfSize;
				float chestMapZ = ((cZ * -1) - iconHalfSize) + mapHeight;

				if(!chest[i].GetComponent<getWeapon>().isChestOpened())
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(chestMapX), resizeHeight(chestMapZ), resizeWidth(iconSize), resizeHeight(iconSize)), chestIcon);
				else
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(chestMapX), resizeHeight(chestMapZ), resizeWidth(iconSize), resizeHeight(iconSize)), openChestIcon);
			}

			float pX = GetMapPos(character.transform.position.x, mapWidth, sceneWidth);
			float pZ = GetMapPos(offset+character.transform.position.z, mapHeight, sceneHeight);
			float playerMapX = pX - iconHalfSize;
			float playerMapZ = ((pZ * -1) - iconHalfSize) + mapHeight;

			GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(playerMapX), 
			                         resizeHeight(playerMapZ), 
			                         resizeWidth(iconSize), resizeHeight(iconSize)), 
			                playerIcon);
		}

	}

	public void setPause(bool pause){
		this.pause = pause;
	}

	public void setShowMiniMap(bool show){
		mapVisible = show;
	}

	public bool showMiniMap(){
		return mapVisible;
	}

	private float GetMapPos(float pos, float mapSize, float sceneSize) {
		return (pos * mapSize/sceneSize);
	}
	

	private float resizeWidth(float width){
		return ((Screen.width * width) / (reference_width * 1.0f));
	}


	private float resizeHeight(float height){
		return ((Screen.height * height) / (reference_height * 1.0f));
	}

}
