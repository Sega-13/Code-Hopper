                                                            **# Code-Hopper**
                                                         Learn Coding with Gaming
                                                         
A 2D platformer game that contains traps when a Player collides with traps a coding puzzle appears. Player have to solve puzzle to move forward.    

2D puzzle-platformer that teaches coding through interactive gameplay. Built modular player controls using design patterns, integrated a live code editor with solution validation, and implemented data-driven level and hint systems with ScriptableObjects. Employed clean architecture for scalability and seamless gameplay experience.   

**GENRE** : Puzzle, Adventure, Learning    
**Technology**   
            •	Unity Engine   
            •	C# Language   
            •	Unity UI Toolkit or uGUI   
            •	TextMesh Pro   
            •	External Compiler Integration       
            •	Remote Config      
**•	Strategy Pattern**    
    o	Files: IJumpStrategy.cs, DoubleJump.cs, PlayerMovement.cs
    o	Allows switching between different movement and jump behaviors at runtime.
**•	Observer Pattern**
    o	Files: ILevelObserver.cs, LevelManager.cs
    o	Enables objects (e.g., UI, sound, analytics) to react to level events (complete, fail, start) without tight coupling.
**•	ScriptableObjects for Configuration**
    o	Files: CodePuzzle.cs, HintData.cs, CollectibleData.cs
    o	Enables easy authoring of puzzles, hints, and collectibles in the Unity Editor.
**•	Singleton / Manager Pattern**
    o	Managers like LevelManager, HintManager, CodePuzzleManager likely function as singletons or central controllers coordinating between systems.

  **Images**
   
  **Video**




