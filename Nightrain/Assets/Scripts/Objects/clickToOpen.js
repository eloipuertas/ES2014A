#pragma strict


private var smooth : int;
private var speed : int;

private var getObjectScene : RaycastHit;
private var targetPosition : Vector3;
private var targetPoint : Vector3;
private var moving : boolean = false;
private var object : GameObject;
private var getWeapon : Component;
function Start()
{
}


function Update ()
{
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
            if(getObjectScene.transform.gameObject.tag.Equals("Object")){
            	Debug.Log("Object");
				getWeapon = this.gameObject.GetComponent("getWeapon");
				
				//getWeapon.setDamage(5);
				//this.objects.GetComponent<getWeapon>().setDamage(5);
			}
		}
    }
}
