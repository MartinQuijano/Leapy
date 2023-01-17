using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementHorizontal : Movement
{
    private float xLeftLimit;
    private float xRightLimit;

    public int moveSpeed = 2;

    public bool goingRight = true;

    void Start()
    {
        xLeftLimit = -5.5f;
        xRightLimit = 5.5f;
    }

    public override void Move()
    {
        if (goingRight)
        {
            transform.position = new Vector2(transform.position.x + (moveSpeed * Time.deltaTime), transform.position.y);
            if (transform.position.x >= xRightLimit)
            {
                goingRight = false;
            }
        }
        else
        {
            transform.position = new Vector2(transform.position.x - (moveSpeed * Time.deltaTime), transform.position.y);
            if (transform.position.x <= xLeftLimit)
            {
                goingRight = true;
            }
        }
    }
}
