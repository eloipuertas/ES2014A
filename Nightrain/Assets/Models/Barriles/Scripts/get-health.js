var apear : GameObject;
var destroy : GameObject;
function Start(){
	apear.active=false;
}
function OnTriggerEnter (other : Collider) {
	apear.active=true;
	Destroy(destroy.gameObject);
}