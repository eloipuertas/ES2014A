var apear : GameObject;
var destroy : GameObject;
<<<<<<< HEAD:Nightrain/Assets/Scripts/Objects/AppearHeart.js

function Start(){
	apear.SetActive(false);
}
function OnTriggerEnter (other : Collider) {
	apear.SetActive(true);
=======
private var music : Component;

function Start(){
	apear.active=false;
	music = GameObject.Find("MusicEngine").GetComponent("Music_Engine_Script");
}
function OnTriggerEnter (other : Collider) {
	apear.active=true;
	music.SendMessage("play_Barrel_Open");
>>>>>>> devel_c_music_issue#115:Nightrain/Assets/Models/Barriles/Scripts/get-health.js
	Destroy(destroy.gameObject);
}