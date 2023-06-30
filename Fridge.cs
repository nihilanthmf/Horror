using UnityEngine;
using System.Collections;

public class Fridge : MonoBehaviour
{
    Animator animator;
    bool opened;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Interact()
    {
        if (opened)
        {
            StartCoroutine(Close());
        }
        else
        {
            StartCoroutine(Open());
        }
    }

    IEnumerator Open()
    {
        animator.SetBool("Close", false);
        animator.SetBool("Open", true);

        yield return new WaitForSeconds(1.9f);
        opened = true;
    }

    IEnumerator Close()
    {
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);

        yield return new WaitForSeconds(1.9f);
        opened = false;
    }
}
