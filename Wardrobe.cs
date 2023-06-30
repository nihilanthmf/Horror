using UnityEngine;
using System.Collections;

public class Wardrobe : MonoBehaviour
{
    Animator animator;
    bool opened;
    [SerializeField] Vector3 openedPosition;
    [SerializeField] DrawerManager drawerManager;
    Vector3 closedPosition;
    bool toChangePosition;
    float curveTime = 0;

    private void Start()
    {
        animator = GetComponent<Animator>();
        closedPosition = transform.localPosition;
    }

    private void Update()
    {
        if (toChangePosition)
        {
            if (opened)
            {
                CloseWithoutAnimator();
            }
            else
            {
                OpenWithoutAnimator();
            }            
        }
    }

    public void OpenClose()
    {
        if (animator != null)
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
        else
        {
            curveTime = 0;
            toChangePosition = true;
        }
    }

    void OpenWithoutAnimator()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, openedPosition, drawerManager.curve.Evaluate(curveTime) * drawerManager.curveMultiplier * Time.deltaTime);
        curveTime += Time.deltaTime * drawerManager.curveMultiplier;

        if (transform.localPosition == openedPosition)
        {
            toChangePosition = false;
            opened = true;
        }
    }

    void CloseWithoutAnimator()
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, closedPosition, drawerManager.curve.Evaluate(curveTime) * drawerManager.curveMultiplier * Time.deltaTime);
        curveTime += Time.deltaTime * drawerManager.curveMultiplier;

        if (transform.localPosition == closedPosition)
        {
            toChangePosition = false;
            opened = false;
        }
    }

    IEnumerator Open()
    {
        animator.SetBool("Close", false);
        animator.SetBool("Open", true);

        yield return new WaitForSeconds(0.9f);
        opened = true;
    }

    IEnumerator Close()
    {
        animator.SetBool("Open", false);
        animator.SetBool("Close", true);

        yield return new WaitForSeconds(0.9f);
        opened = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6) // pickable
        {
            //collision.transform.SetParent(transform, true);
        }
    }
}
