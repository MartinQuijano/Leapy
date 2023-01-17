using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndShoot : MonoBehaviour
{
    public float powerMultiplier = 5f;

    public Vector2 minPower;
    public Vector2 maxPower;

    private DragLine dragLine;

    Camera cam;
    Vector3 force;
    Vector3 startPoint;
    Vector3 endPoint;
    public float duration;

    public bool jumping;
    private bool shouldSetParent;

    public AudioClip jumpClip;
    private AudioSource audioSource;

    private void Start()
    {
        cam = Camera.main;
        dragLine = GetComponent<DragLine>();
        jumping = false;
        shouldSetParent = true;
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!GameManager.gameIsPaused)
        {
            if (!IsJumping())
            {
                if (Input.GetMouseButtonDown(0))
                {
                    startPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    startPoint.z = 15;
                }

                if (Input.GetMouseButton(0))
                {
                    Vector3 currentPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    currentPoint.z = -1;
                    Vector3 auxStartPoint = startPoint;
                    auxStartPoint.z = -1;
                    dragLine.RenderLine(auxStartPoint, currentPoint);
                }

                if (Input.GetMouseButtonUp(0))
                {
                    endPoint = cam.ScreenToWorldPoint(Input.mousePosition);
                    endPoint.z = 15;
                    force = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                    Vector3 initialPosition = transform.position;
                    Vector3 finalPosition = transform.position + (force * powerMultiplier);

                    StartCoroutine(Jump(initialPosition, finalPosition));
                    dragLine.EndLine();
                }
            }
        }
    }

    IEnumerator Jump(Vector3 initialPosition, Vector3 finalPosition)
    {
        audioSource.PlayOneShot(jumpClip, 0.5f);
        shouldSetParent = true;
        jumping = true;
        float time = 0;
        bool isSizeAtMax = false;
        float scaleModifier = 1f;

        Vector3 startScale = transform.localScale;
        while (time < duration)
        {
            if (!isSizeAtMax)
            {
                scaleModifier = Mathf.Lerp(1f, 1.5f, time / (duration / 2));
                transform.localScale = startScale * scaleModifier;
            }
            else
            {
                scaleModifier = Mathf.Lerp(1.5f, 1f, (time - 0.5f) / (duration / 2));
                transform.localScale = startScale * scaleModifier;
            }

            if (scaleModifier >= 1.5f)
            {
                isSizeAtMax = true;
                transform.localScale = new Vector3(1.5f, 1.5f, 0);
            }

            transform.position = Vector2.Lerp(initialPosition, finalPosition, time / duration);
            time += Time.deltaTime;

            yield return null;
        }
        transform.localScale = new Vector3(1, 1, 0);
        transform.position = finalPosition;
        jumping = false;
        shouldSetParent = true;
    }

    public bool IsJumping()
    {
        return jumping;
    }

    public bool ShouldSetParent()
    {
        return shouldSetParent;
    }

    public void SetShouldSetParent(bool value)
    {
        shouldSetParent = value;
    }

}
