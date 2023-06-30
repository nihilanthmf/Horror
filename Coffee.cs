using UnityEngine;

public class Coffee : MonoBehaviour
{
    Animator animator;
    [SerializeField] Transform parentOnPlayer;
    [SerializeField] GameObject coffeeLiquid;
    [SerializeField] GameObject toDeleteWhenFinishedDrinking;
    Rigidbody rb;
    bool hasStoppedDrinkingOnce;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
    }

    public void Drink()
    {
        gameObject.tag = "Untagged";
        transform.SetParent(parentOnPlayer);
        transform.localPosition = Vector3.zero;
        animator.SetBool("Drink", true);
    }

    private void Update()
    {
        if (!hasStoppedDrinkingOnce && !coffeeLiquid.activeSelf) // when stops drinkning animation
        {
            StopDrinking();
            hasStoppedDrinkingOnce = true;
        }
    }

    void StopDrinking()
    {
        Destroy(animator);
        Destroy(toDeleteWhenFinishedDrinking);

        transform.SetParent(null);
        rb.isKinematic = false;
        gameObject.AddComponent<PickableObject>();
        gameObject.layer = 6;
    }
}
