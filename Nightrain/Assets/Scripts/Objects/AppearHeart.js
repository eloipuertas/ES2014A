var apear : GameObject;
var destroy : GameObject;

function Start(){
	apear.SetActive(false);
}
function OnTriggerEnter (other : Collider) {
	apear.SetActive(true);
	Destroy(destroy.gameObject);
}