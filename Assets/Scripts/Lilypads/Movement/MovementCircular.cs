using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementCircular : Movement
{
    public float speedOfRotation = 1f;
    public float radius = 1f;

    private Vector2 centerPoint;
    private float angle;

    private float riverLeftXLimit = -5.5f;
    private float riverRightXLimit = 5.5f;
    void Start()
    {
        if (transform.position.x - radius < riverLeftXLimit)
            transform.position = new Vector2(-4.5f, transform.position.y);
        if (transform.position.x + radius > riverRightXLimit)
            transform.position = new Vector2(4.5f, transform.position.y);
        centerPoint = transform.position;
    }

    public override void Move()
    {
        angle = angle + speedOfRotation * Time.deltaTime;

        Vector2 offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = centerPoint + offset;
    }
}
