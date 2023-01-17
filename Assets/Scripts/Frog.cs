using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frog : MonoBehaviour
{
    public ScoreManager scoreManager;
    public DragAndShoot dragAndShoot;
    public float radius;

    public bool safe;

    void Start()
    {
        safe = true;
        radius = 0.6f;
    }

    void Update()
    {
        CheckStatus();
    }

    private void CheckStatus()
    {
        if (dragAndShoot.IsJumping())
        {
            if (dragAndShoot.ShouldSetParent())
            {
                transform.SetParent(null);
                dragAndShoot.SetShouldSetParent(false);
            }
        }
        else
        {
            Collider2D collider = Physics2D.OverlapCircle(transform.position, radius);
            if (!collider)
            {
                safe = false;
            }
            else
            {
                if (collider.gameObject.tag == "lilypad")
                {
                    if (!collider.gameObject.GetComponent<Lilypad>().AlreadyGaveScore())
                        scoreManager.AddScore(collider.gameObject.GetComponent<Lilypad>().GetScore());
                    safe = true;
                    if (dragAndShoot.ShouldSetParent())
                    {
                        transform.SetParent(collider.gameObject.transform);
                        dragAndShoot.SetShouldSetParent(false);
                    }
                }
            }
        }
        if (IsOutOfCamera())
            safe = false;
    }

    private bool IsOutOfCamera()
    {
        if (transform.position.y + 1 < Camera.main.ScreenToWorldPoint(Vector3.zero).y)
        {
            return true;
        }
        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public bool isSafe()
    {
        return safe;
    }

    public void SankDown()
    {
        safe = false;
    }
}