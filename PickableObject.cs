using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public bool isHeld { get; set; }

    Rigidbody rb;

    PlayerInteraction playerInteraction;
    [HideInInspector] public TaskObject taskObject;

    Vector3 currentVelocity;

    bool taskCompletedOnce;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void SetPlayerController(PlayerInteraction playerInteraction)
    {
        this.playerInteraction = playerInteraction;
    }


    void CheckingTask()
    {
        if (!taskCompletedOnce && !isHeld)
        {
            taskObject.TryCompletingTask();
            taskCompletedOnce = true;
        }
    }


    private void FixedUpdate()
    {
        if (isHeld)
        {
            Vector3 smoothPosition = Vector3.SmoothDamp(transform.position, playerInteraction.objectHolder.transform.position, ref currentVelocity, 0.03f);

            rb.velocity = (smoothPosition - transform.localPosition) * 50;            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 10 && taskObject != null)
        {
            CheckingTask();
        }
    }
}
