




    var teclado: AnimationClip;
    
    function Update () {
    
    if (Input.GetKey("1"))
    		{
    		animation.Play ("run");}
    		
    else if(Input.GetKey("2"))
    		{
  			  animation.Play ("attack");}
  			  
  	else if(Input.GetKey("3"))
    		{
  			  animation.Play ("walk");}		  

    else if(Input.GetKey("4"))
    		{
  			  animation.Play ("jump");}
  			  
  	else 
   			 {
    		animation.Play ("idle");}

   	 }
    



    
    
    
    
    
