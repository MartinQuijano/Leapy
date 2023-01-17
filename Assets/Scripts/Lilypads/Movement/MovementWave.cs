using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementWave : Movement
{
    public float frequency = 3f;
    public float amplitude = 1f;
    public float cycleSpeed = 1f;
    private Vector3 positionOfTheObject;
    private Vector3 axis;

    private float xLeftLimit;
    private float xRightLimit;
    public bool goingRight = true;

    void Start()
    {
        xLeftLimit = -5.5f;
        xRightLimit = 5.5f;

        positionOfTheObject = transform.position;
        axis = transform.up;
    }

    public override void Move()
    {
        if (goingRight)
        {
            positionOfTheObject += Vector3.right * Time.deltaTime * cycleSpeed;
            transform.position = positionOfTheObject + axis * Mathf.Sin(Time.time * frequency) * amplitude;
            if (transform.position.x >= xRightLimit)
            {
                goingRight = false;
            }
        }
        else
        {
            positionOfTheObject -= Vector3.right * Time.deltaTime * cycleSpeed;
            transform.position = positionOfTheObject + axis * Mathf.Sin(Time.time * frequency) * amplitude;
            if (transform.position.x <= xLeftLimit)
            {
                goingRight = true;
            }
        }
    }
}
