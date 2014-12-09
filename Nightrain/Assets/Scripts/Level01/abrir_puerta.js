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

	if (other.tag.Equals("Player") || other.tag.Equals("Enemy")){
		this.audio.Play();
		
		targetValue = AngleY;

		currentValue = 0;
	
	}

}



function OnTriggerExit (other : Collider) {

	if (other.tag.Equals("Player") || other.tag.Equals("Enemy")){
		currentValue = AngleY;

		targetValue = 0.0;
	}

}
