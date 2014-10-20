#pragma strict

var smooth : int;
private var speed : int;

private var targetPosition : Vector3;
private var targetPoint : Vector3;
private var moving : boolean = false; //Whether the player is moving or has stopped



function Start()
{
    // Walk at double speed
    animation["Armature|Correr"].speed = 3.75;
}


function Update ()
{
    // Walking animation control
    if(moving){
        // Stop animation
        if(transform.position == targetPoint){
            //animation.CrossFade("Armature|Idle",0.2f);
            animation.Stop("Armature|Correr");
            moving = false;
        //Set walking animation
        } else {
            animation.Play("Armature|Correr");
        }
    }
    

    if(Input.GetKeyDown(KeyCode.Mouse0)) {                   
        //smooth=1;
        speed = 30;

        
        animation.Play("Armature|Correr");
        moving = true;

        //animation["Armature|Correr"].wrapMode = WrapMode.Loop;

        var playerPlane = new Plane(Vector3.up, transform.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hitdist = 0.0;

        if (playerPlane.Raycast(ray, hitdist))
        {
            Debug.DrawLine(transform.position, transform.position + ray.GetPoint(hitdist), Color.red, 2, false);
            targetPoint = ray.GetPoint(hitdist);
            targetPosition = ray.GetPoint(hitdist);
            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = targetRotation;
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