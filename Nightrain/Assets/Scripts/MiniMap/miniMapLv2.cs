using UnityEngine;
using System.Collections;

public class miniMapLv2 : MonoBehaviour {

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

	// MAINCHARACTER
	private GameObject character;

	// ENEMIES
	private GameObject skeleton;
	private GameObject[] skeleton1;
	private GameObject[] skeleton2;
	private GameObject[] skeleton3; 	
	private GameObject[] skeleton5; 
	private GameObject[] miniicedemon7;
	private GameObject[] miniicedemon8;

	private InventoryScript inventory;

	// BOSS
	private GameObject firedemon4;
	private GameObject icedemon6;
	private GameObject boss;

	// CHEST
	private GameObject[] chest;
	
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
	private int offset_w = 0;
	private int offset_h = 0;

	private bool mapVisible = false;

	// Use this for initialization
	void Start () {

		// REFERENCE GAMEOBJECTS
		this.character = GameObject.FindGameObjectWithTag("Player");
		this.skeleton = GameObject.FindGameObjectWithTag("Skeleton");
		this.skeleton1 = GameObject.FindGameObjectsWithTag("Skeleton1");
		this.skeleton2 = GameObject.FindGameObjectsWithTag("Skeleton2");
		this.skeleton3 = GameObject.FindGameObjectsWithTag("Skeleton3");
		this.skeleton5 = GameObject.FindGameObjectsWithTag("Skeleton5");
		this.miniicedemon7 = GameObject.FindGameObjectsWithTag("MiniIceDemon7");
		this.miniicedemon8 = GameObject.FindGameObjectsWithTag("MiniIceDemon8");

		this.firedemon4 = GameObject.FindGameObjectWithTag("FireDemon4");
		this.icedemon6 = GameObject.FindGameObjectWithTag("IceDemon6");
		this.boss = GameObject.FindGameObjectWithTag("Boss");

		this.chest = GameObject.FindGameObjectsWithTag("Chest");

		this.inventory = character.GetComponentInChildren <InventoryScript> ();

		// LOAD TEXTURES
		this.playerIcon = Resources.Load<Texture2D>("MiniMap/character_v2");
		this.enemyIcon = Resources.Load<Texture2D>("MiniMap/enemy");
		this.bossIcon = Resources.Load<Texture2D>("MiniMap/boss");
		this.chestIcon = Resources.Load<Texture2D>("MiniMap/chest");
		this.openChestIcon = Resources.Load<Texture2D>("MiniMap/chest_open");
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


			if(firedemon4 != null){
				float bossX = GetMapPos(-offset_w+firedemon4.transform.position.x, mapWidth, sceneWidth);
				float bossZ = GetMapPos(offset_h+firedemon4.transform.position.z, mapHeight, sceneHeight);
				float bossMapX = bossX - iconHalfSize;
				float bossMapZ = ((bossZ * -1) - iconHalfSize) + mapHeight;
				
				GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(bossMapX), 
				                         resizeHeight(bossMapZ), 
				                         resizeWidth(iconSize), 
				                         resizeHeight(iconSize)), 
				                bossIcon);
			}

			if(icedemon6 != null){
				float bossX = GetMapPos(-offset_w+icedemon6.transform.position.x, mapWidth, sceneWidth);
				float bossZ = GetMapPos(offset_h+icedemon6.transform.position.z, mapHeight, sceneHeight);
				float bossMapX = bossX - iconHalfSize;
				float bossMapZ = ((bossZ * -1) - iconHalfSize) + mapHeight;
				
				GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(bossMapX), 
				                         resizeHeight(bossMapZ), 
				                         resizeWidth(iconSize), 
				                         resizeHeight(iconSize)), 
				                bossIcon);
			}

			if(boss != null){
				float bossX = GetMapPos(-offset_w+boss.transform.position.x, mapWidth, sceneWidth);
				float bossZ = GetMapPos(offset_h+boss.transform.position.z, mapHeight, sceneHeight);
				float bossMapX = bossX - iconHalfSize;
				float bossMapZ = ((bossZ * -1) - iconHalfSize) + mapHeight;

				GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(bossMapX), 
				                         resizeHeight(bossMapZ), 
				                         resizeWidth(iconSize), 
				                         resizeHeight(iconSize)), 
				                bossIcon);
			}

			for(int i = 0; i < skeleton1.Length; i++){
				if(skeleton1[i] != null){
					float enemyX = GetMapPos(-offset_w+skeleton1[i].transform.position.x, mapWidth, sceneWidth);
					float enemyZ = GetMapPos(offset_h+skeleton1[i].transform.position.z, mapHeight, sceneHeight);
					float enemyMapX = enemyX - iconHalfSize;
					float enemyMapZ = ((enemyZ * -1) - iconHalfSize) + mapHeight;

					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(enemyMapX), 
					                         resizeHeight(enemyMapZ), 
					                         resizeWidth(iconSize), 
					                         resizeHeight(iconSize)), 
					                enemyIcon);
				}
			}

			for(int i = 0; i < skeleton2.Length; i++){
				if(skeleton2[i] != null){
					float enemyX = GetMapPos(-offset_w+skeleton2[i].transform.position.x, mapWidth, sceneWidth);
					float enemyZ = GetMapPos(offset_h+skeleton2[i].transform.position.z, mapHeight, sceneHeight);
					float enemyMapX = enemyX - iconHalfSize;
					float enemyMapZ = ((enemyZ * -1) - iconHalfSize) + mapHeight;
					
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(enemyMapX), 
					                         resizeHeight(enemyMapZ), 
					                         resizeWidth(iconSize), 
					                         resizeHeight(iconSize)), 
					                enemyIcon);
				}
			}

			for(int i = 0; i < skeleton3.Length; i++){
				if(skeleton3[i] != null){
					float enemyX = GetMapPos(-offset_w+skeleton3[i].transform.position.x, mapWidth, sceneWidth);
					float enemyZ = GetMapPos(offset_h+skeleton3[i].transform.position.z, mapHeight, sceneHeight);
					float enemyMapX = enemyX - iconHalfSize;
					float enemyMapZ = ((enemyZ * -1) - iconHalfSize) + mapHeight;
					
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(enemyMapX), 
					                         resizeHeight(enemyMapZ), 
					                         resizeWidth(iconSize), 
					                         resizeHeight(iconSize)), 
					                enemyIcon);
				}
			}

			for(int i = 0; i < skeleton5.Length; i++){
				if(skeleton5[i] != null){
					float enemyX = GetMapPos(-offset_w+skeleton5[i].transform.position.x, mapWidth, sceneWidth);
					float enemyZ = GetMapPos(offset_h+skeleton5[i].transform.position.z, mapHeight, sceneHeight);
					float enemyMapX = enemyX - iconHalfSize;
					float enemyMapZ = ((enemyZ * -1) - iconHalfSize) + mapHeight;
					
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(enemyMapX), 
					                         resizeHeight(enemyMapZ), 
					                         resizeWidth(iconSize), 
					                         resizeHeight(iconSize)), 
					                enemyIcon);
				}
			}

			for(int i = 0; i < miniicedemon7.Length; i++){
				if(miniicedemon7[i] != null){
					float enemyX = GetMapPos(-offset_w+miniicedemon7[i].transform.position.x, mapWidth, sceneWidth);
					float enemyZ = GetMapPos(offset_h+miniicedemon7[i].transform.position.z, mapHeight, sceneHeight);
					float enemyMapX = enemyX - iconHalfSize;
					float enemyMapZ = ((enemyZ * -1) - iconHalfSize) + mapHeight;
					
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(enemyMapX), 
					                         resizeHeight(enemyMapZ), 
					                         resizeWidth(iconSize), 
					                         resizeHeight(iconSize)), 
					                enemyIcon);
				}
			}

			for(int i = 0; i < miniicedemon8.Length; i++){
				if(miniicedemon8[i] != null){
					float enemyX = GetMapPos(-offset_w+miniicedemon8[i].transform.position.x, mapWidth, sceneWidth);
					float enemyZ = GetMapPos(offset_h+miniicedemon8[i].transform.position.z, mapHeight, sceneHeight);
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
				float cX = GetMapPos(-offset_w+chest[i].transform.position.x, mapWidth, sceneWidth);
				float cZ = GetMapPos(offset_h+chest[i].transform.position.z, mapHeight, sceneHeight);
				float chestMapX = cX - iconHalfSize;
				float chestMapZ = ((cZ * -1) - iconHalfSize) + mapHeight;

				if(!chest[i].GetComponent<getWeapon>().isChestOpened())
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(chestMapX), resizeHeight(chestMapZ), resizeWidth(iconSize), resizeHeight(iconSize)), chestIcon);
				else
					GUI.DrawTexture(new Rect(minimap_box.x + resizeWidth(chestMapX), resizeHeight(chestMapZ), resizeWidth(iconSize), resizeHeight(iconSize)), openChestIcon);
			}

			float pX = GetMapPos(-offset_w+character.transform.position.x, mapWidth, sceneWidth);
			float pZ = GetMapPos(offset_h+character.transform.position.z, mapHeight, sceneHeight);
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
