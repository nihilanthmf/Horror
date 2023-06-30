using UnityEngine;


public class Chair : MonoBehaviour
{
    [SerializeField] Vector3 seatedPosition;
    Vector3 playerPositionBeforeSeat;
    [SerializeField] Player player;
    PlayerInteraction playerInteraction;

    private void Start()
    {
        playerInteraction = player.GetComponent<PlayerInteraction>();
    }

    public void Seat()
    {
        playerPositionBeforeSeat = player.transform.position;

        player.toAllowMoving = false;
        player.transform.SetParent(transform, true);
        player.transform.localPosition = seatedPosition;

        playerInteraction.seated = true;
    }

    void StandUp()
    {
        player.transform.position = playerPositionBeforeSeat;

        player.toAllowMoving = true;
        player.transform.SetParent(null, true);

        playerInteraction.seated = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerInteraction.seated)
        {
            StandUp();
        }
    }
}
