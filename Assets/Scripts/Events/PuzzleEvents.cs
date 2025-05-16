using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleEvents
{
    private static PuzzleEvents instance;
    public static PuzzleEvents Instance
    {
        get
        {
            if(instance == null)
            {
                instance = new PuzzleEvents();
            }
            return instance;
        }
    }
       
    public EventController OnCodeSuccess { get; private set; }
    public EventController OnCodeFailure { get; private set; }
    public EventController<bool> OnPuzzleCompleted { get; private set; }
    PuzzleEvents()
    {
        OnCodeSuccess = new EventController();
        OnCodeFailure = new EventController();
        OnPuzzleCompleted = new EventController<bool> ();
    }
}
