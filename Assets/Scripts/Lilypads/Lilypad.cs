using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lilypad : MonoBehaviour
{
    public Movement movementLogic;
    public bool gaveScore;

    protected virtual void Update()
    {
        movementLogic.Move();
    }

    public int GetScore()
    {
        gaveScore = true;
        return 50;
    }

    public bool AlreadyGaveScore()
    {
        return gaveScore;
    }
}
