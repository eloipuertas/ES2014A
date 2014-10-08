#pragma strict

var smooth : int;
var speed : int;

private var targetPosition : Vector3;

function Update ()
{
    if(Input.GetKeyDown(KeyCode.Mouse0)) {                   
        //smooth=1;
        speed = 75;

        //Set walking animation
        //animation.Play("walk");
        //transform.animation.Play("Armature|ArmatureAction");

        var playerPlane = new Plane(Vector3.up, transform.position);
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var hitdist = 0.0;

        if (playerPlane.Raycast(ray, hitdist))
        {
            //Debug.DrawLine(transform.position, transform.position + ray.GetPoint(hitdist), Color.red, 2, false);
            var targetPoint = ray.GetPoint(hitdist);
            targetPosition = ray.GetPoint(hitdist);
            var targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
            transform.rotation = targetRotation;
        }

    } else {
        // Stop animation
        //animation.Play("walk", PlayMode.StopAll);
        //transform.animation.Stop("Armature|ArmatureAction");  
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