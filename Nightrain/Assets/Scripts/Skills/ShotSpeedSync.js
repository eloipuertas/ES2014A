#pragma strict
public var offset:int = 10;
private var parentParticle:ParticleSystem;

function Start () {
	parentParticle = transform.parent.particleSystem;
	var parentSpeed:float = parentParticle.startSpeed;
	var thisSpeed:float = particleSystem.startSpeed;
	
	//Debug.Log(parentSpeed);
	if( parentSpeed >= thisSpeed){
		var sum:float = parentSpeed - thisSpeed;
		if( sum - offset > 0){
			while( sum - offset > 0){
				thisSpeed++;
				sum = parentSpeed - thisSpeed;
			}
		}
		particleSystem.startSpeed = thisSpeed;
	}
	
	//Debug.Log(particleSystem.startSpeed);
	//this.enabled = false;
}
