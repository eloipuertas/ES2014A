NIGHTRAIN Project
==================
This is the mainCharacter pathFinding final branch



* * * * * * * * * * * * * * * * * * * *
* Files to be merged into Devel branch.
* * * * * * * * * * * * * * * * * * * *

>[NEW MODEL FILES]:
-------------------
	/Assets/Models/MainCharacter/Main Controller.controller
	/Assets/Models/MainCharacter/Main Controller.controller.meta

	/Assets/Models/MainCharacter/Fat/Materials/*.*
	/Assets/Models/MainCharacter/Fat/*.*		->[ including new version of "escudo.blend" & "hacha.blend" ]

	/Assets/Models/MainCharacter/Woman/Materials/*.*
	/Assets/Models/MainCharacter/Woman/*.*		->[ including new files "baston.blend" & "cuchillo.blend" ]

>[NEW PREFAB FILES]:
-------------------
	/Assets/Resources/Prefabs/MainCharacters/fat.prefab
	/Assets/Resources/Prefabs/MainCharacters/fat.prefab.meta

	/Assets/Resources/Prefabs/MainCharacters/woman.prefab
	/Assets/Resources/Prefabs/MainCharacters/woman.prefab.meta

>[PATHFINDING MOV. SCRIPT]:
--------------------------
	/Assets/Scripts/MainCharacter/ClickToMove.cs
	/Assets/Scripts/MainCharacter/ClickToMove.cs.meta

* * * * * * * * * * * * * * * * * * * *
* IMPORTANT NOTE:
* * * * * * * * * * * * * * * * * * * *
Final file hierarchy for the directories
  - /Assets/Models/MainCharacter/..
  - /Assets/Resources/Prefabs/MainCharacters/.. 
should be exactly this one (all obsolete versions has been removed).
However, old models and prefabs could be probably required by other developers
as they has been working with old prefab versions.