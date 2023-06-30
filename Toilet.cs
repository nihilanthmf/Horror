using UnityEngine;

public class Toilet : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Flush()
    {
        animator.Play("Flush");
        gameObject.tag = "Untagged";
    }

    void AllowFlushAgain()
    {
        gameObject.tag = "Toilet";
    }
}
