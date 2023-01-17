using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingLilypad : Lilypad
{
    public bool isSinking;

    private Animator animator;

    void Start()
    {
        isSinking = false;
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();
        if (!isSinking && IsFrogOnTop())
        {
            isSinking = true;
            StartCoroutine(StartSinking());
        }
    }

    private bool IsFrogOnTop()
    {
        if (transform.childCount != 0)
            return true;
        return false;
    }

    IEnumerator StartSinking()
    {
        animator.SetBool("isFrogOnTop", true);
        yield return new WaitForSeconds(4f);
        if (transform.childCount != 0)
        {
            Frog frog = transform.GetChild(0).gameObject.GetComponent<Frog>();
            frog.SankDown();
        }
    }

}
