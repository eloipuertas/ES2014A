var AngleY : float = 90.0;



private var targetValue : float = 0.0;

private var currentValue : float = 0.0;

private var easing : float = 0.05;



var Target : GameObject;



function Update(){

	
	currentValue = currentValue + (targetValue - currentValue) * easing;

	Target.transform.rotation = Quaternion.identity; // set rotation to zero

	Target.transform.Rotate(0, currentValue, 0); // apply full Rotation


}



function OnTriggerEnter (other : Collider) {

	this.audio.Play();
	
	targetValue = AngleY;

	currentValue = 0;

}



function OnTriggerExit (other : Collider) {


	currentValue = AngleY;

	targetValue = 0.0;


}
