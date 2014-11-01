#pragma strict


private var smooth : int;
private var speed : int;

private var getObjectScene : RaycastHit;
private var targetPosition : Vector3;
private var targetPoint : Vector3;
private var moving : boolean = false; //Whether the player is moving or has stopped
// SCREEN VALUES
/*private var width:int = Screen.width;
private var height:int = Screen.height;

// ATRIBUTES CHARACTER
var bar_health:int;
var bar_magic:int;

private var health:float;

// HEALTH BAR
// --- TEXTURES ---
private var AvatarTexture:Texture2D;

private var HealthTexture:Texture2D;
private var HealthBarTexture:Texture2D;

private var MagicTexture:Texture2D;
private var MagicBarTexture:Texture2D;

// --- MATERIALS ---
private var HealthBarMaterial:Material;
private var MagicBarMaterial:Material;

// LOW HEALTH
private var c:Color;*/

function Start()
{
    // Walk at double speed
    animation["metarig|Caminar"].speed = 2.75;
    animation["metarig|Atacar"].speed = 1.5;
    
    /*this.bar_health = 100;
	this.bar_magic = 100;
	
	var path = Application.dataPath;
	
	// ADD TEXTURES
	this.AvatarTexture = Resources.LoadAssetAtPath("Assets/Textures/HealthBar/avatar.png", typeof(Texture2D));
	this.HealthTexture = Resources.LoadAssetAtPath("Assets/Textures/HealthBar/health.png", typeof(Texture2D));
	this.HealthBarTexture = Resources.LoadAssetAtPath("Assets/Textures/HealthBar/bar_health.png", typeof(Texture2D));
	this.MagicTexture = Resources.LoadAssetAtPath("Assets/Textures/HealthBar/magic.png", typeof(Texture2D));
	this.MagicBarTexture = Resources.LoadAssetAtPath("Assets/Textures/HealthBar/bar_magic.png", typeof(Texture2D));
  	
  	// ADD MATERIALS
  	this.HealthBarMaterial = Resources.LoadAssetAtPath("Assets/Textures/HealthBar/Materials/bar_health.mat", typeof(Material));
    this.MagicBarMaterial = Resources.LoadAssetAtPath("Assets/Textures/HealthBar/Materials/bar_magic.mat", typeof(Material));
    
    this.c = this.HealthBarMaterial.color;
    
    this.HealthBarMaterial.SetFloat("_Cutoff", .5f);
	this.MagicBarMaterial.SetFloat("_Cutoff", .5f);*/
	
}


function Update ()
{

	//UpdateHealth();
    // Walking animation control
    if(moving){
        // Stop animation
        if(transform.position == targetPoint){
            //animation.CrossFade("Armature|Idle",0.2f);
            Debug.Log ("moved to target location");
            animation.Stop("metarig|Caminar"); 
            moving = false;
        //Set walking animation
        } else {
            animation.Play("metarig|Caminar");
            Debug.Log ("on the way...");
        }
    }


    if(Input.GetKeyDown(KeyCode.Mouse0)) { 
       //smooth=1;
        speed = 30; 
        
    	var playerPlane = new Plane(Vector3.up, transform.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hitdist = 0.0;
        
    	//deteccion movimiento
        if (playerPlane.Raycast(ray, hitdist))
        {
            targetPoint = ray.GetPoint(hitdist);
            targetPosition = ray.GetPoint(hitdist);
            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = targetRotation;
        }
       
    	if(Physics.Raycast(ray, getObjectScene, 100)){
            if(getObjectScene.transform.gameObject.tag.Equals("Enemy")){
            	Debug.Log("Enemigo seleccionado");
            	//animation.Stop("metarig|Caminar");
            	moving = false;
            	//attack = true;
            	animation.CrossFade("metarig|Atacar",0.2f);
			} else {
		        animation.Play("metarig|Caminar");
		        moving = true;
		        //animation["Armature|Correr"].wrapMode = WrapMode.Loop;
			}
		} 
		
		
		


    }

    //transform.position = Vector3.Slerp (transform.position, targetPosition, Time.deltaTime * smooth);
    //transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed);

    // find the target position relative to the player:
    var dir: Vector3 = targetPosition - transform.position;
    // calculate movement at the desired speed:
    var movement: Vector3 = dir.normalized * speed * Time.deltaTime;
    // limit movement to never pass the target position:
    if (movement.magnitude > dir.magnitude) movement = dir;

   

    // move the character:
    GetComponent(CharacterController).Move(movement);

	

}

function SelectTarget(){
	 //selectedTarget.renderer.material.color = Color.red;
	 Debug.Log ("Enemy Targeted");
}

/*function UpdateHealth () {


	// Esto es para cuando le quede 1/4 de vida se ponga en rojo
	if(this.health >= 0.9f){
		this.c.r = 1.0f;
		this.c.g = 0;
		this.c.b = 0;
		this.HealthBarMaterial.color = c;
	}else{
		this.c.r = 1.0f;
		this.c.g = 1.0f;
		this.c.b = 1.0f;
		this.HealthBarMaterial.color = c;
	}
	
	// Si la vida llega a 0 
	if(this.health == 1.0f){
		this.bar_health = 0;
	}else{ // Sino k con el tiempo siga bajando la vida
		this.health = (this.bar_health + Mathf.FloorToInt(Time.time)) * .005f; // <-- este .005 es para mantener el valor del Cutoff[0.5, 1]
		this.HealthBarMaterial.SetFloat("_Cutoff", health);
	}
	
}

function getHealth():int{
	return this.bar_health;
}

function setHealth(health:int){
	this.bar_health = health;
}

function OnGUI(){

	if(Event.current.type.Equals(EventType.Repaint)){
		
		// AVATAR ZONE
		var avatar_box:Rect = new Rect(0,
									   0, 
									   this.AvatarTexture.width/2, 
									   this.AvatarTexture.height/2);
									   
		Graphics.DrawTexture(avatar_box, this.AvatarTexture);
		
		// HEALTH BAR ZONE
		
		var healthbar_box:Rect = new Rect((this.AvatarTexture.width/2) - 5,
									   44, 
									   (this.HealthBarTexture.width/2), 
									   this.HealthBarTexture.height/2);
									   
		
		Graphics.DrawTexture(healthbar_box, this.HealthBarTexture, this.HealthBarMaterial);
		
		var health_box:Rect = new Rect((this.AvatarTexture.width/2) - 5,
									   44, 
									   this.HealthTexture.width/2, 
									   this.HealthTexture.height/2);
		
		Graphics.DrawTexture(health_box, this.HealthTexture);
		
		// MAGIC BAR ZONE
		
		var magicbar_box:Rect = new Rect((this.AvatarTexture.width/2) - 5,
									   75, 
									   (this.MagicBarTexture.width/2), 
									   (this.MagicBarTexture.height/2) - 8);
									   
		
		Graphics.DrawTexture(magicbar_box, this.MagicBarTexture, this.MagicBarMaterial);
		
		var magic_box:Rect = new Rect((this.AvatarTexture.width/2) - 5,
									   75, 
									   this.MagicTexture.width/2, 
									   this.MagicTexture.height/2);
		
		Graphics.DrawTexture(magic_box, this.MagicTexture);
	}
}*/
