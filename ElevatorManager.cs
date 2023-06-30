using UnityEngine;
using System.Collections.Generic;

public class ElevatorManager : MonoBehaviour
{
    public Elevator[] elevators;

    public Player player;

    [SerializeField] Transform[] buttonHolders;
    List<ElevatorButton> elevatorButtons;

    public float timeToPassOneFloor { get; private set; } = 4f; 
    public float timeForOpenDoor { get; private set; } = 5f;

    private void Start()
    {
        elevatorButtons = new List<ElevatorButton>();

        for (int i = 0; i < buttonHolders.Length; i++)
        {
            for (int a = 0; a < buttonHolders[i].childCount; a++)
            {
                elevatorButtons.Add(buttonHolders[i].GetChild(a).GetComponent<ElevatorButton>());
            }
        }
    }

    public void MakeButtonsInactive()
    {
        foreach (var button in elevatorButtons)
        {
            button.tag = "Untagged";
        }
    }
    public void MakeButtonsActive()
    {
        foreach (var button in elevatorButtons)
        {
            button.tag = "Elevator Floor Button";
        }
    }

    void TransportFromGroundFloor(float y, List<GameObject> objectsInside) // 10.9174f    9.795969f
    {
        float yDeltaForObjects = y - player.transform.position.y;
        player.transform.position = new Vector3(player.transform.position.x + 0.02301f, y, player.transform.position.z + 0.65f);
        foreach (GameObject item in objectsInside)
        {
            item.transform.position = new Vector3(item.transform.position.x + 0.02301f, item.transform.position.y + yDeltaForObjects, item.transform.position.z + 0.65f);
        }
    }

    void Transport(float y, List<GameObject> objectsInside) // 10.9174f
    {
        float yDeltaForObjects = y - player.transform.position.y;
        player.transform.position = new Vector3(player.transform.position.x, y, player.transform.position.z);
        foreach (GameObject item in objectsInside)
        {
            item.transform.position = new Vector3(item.transform.position.x, item.transform.position.y + yDeltaForObjects, item.transform.position.z);
        }
    }

    public void PlayerTransport(int floor, int currentFloor, List<GameObject> objectsInside)
    {
        switch (floor)
        {
            case 0:
                float yDeltaForObjects = 1.041501f - player.transform.position.y; // 1.041501f
                player.transform.position = new Vector3(player.transform.position.x - 0.02301f, 1.041501f, player.transform.position.z - 0.65f);
                foreach (GameObject item in objectsInside)
                {
                    item.transform.position = new Vector3(item.transform.position.x - 0.02301f, item.transform.position.y + yDeltaForObjects, item.transform.position.z - 0.65f);
                }
                break;
            case 1:
                if (currentFloor == 0)
                {
                    TransportFromGroundFloor(10.83747f, objectsInside);
                }
                else
                {
                    Transport(10.83747f, objectsInside);
                }
                break;
            case 2:
                if (currentFloor == 0)
                {
                    TransportFromGroundFloor(20.64416f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x + 0.02301f, 20.7241f, player.transform.position.z + 0.65f);
                }
                else
                {
                    Transport(20.64416f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x, 20.7241f, player.transform.position.z);
                }
                break;
            case 3:
                if (currentFloor == 0)
                {
                    TransportFromGroundFloor(30.44416f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x + 0.02301f, 30.5241f, player.transform.position.z + 0.65f);
                }
                else
                {
                    Transport(30.44416f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x, 30.5241f, player.transform.position.z);
                }
                break;
            case 4:
                if (currentFloor == 0)
                {
                    TransportFromGroundFloor(40.24094f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x + 0.02301f, 40.320845f, player.transform.position.z + 0.65f);
                }
                else
                {
                    Transport(40.24094f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x, 40.320845f, player.transform.position.z);
                }
                break;
            case 5:
                if (currentFloor == 0)
                {
                    TransportFromGroundFloor(50.05094f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x + 0.02301f, 50.130845f, player.transform.position.z + 0.65f);
                }
                else
                {
                    Transport(50.05094f, objectsInside);
                    //player.transform.position = new Vector3(player.transform.position.x, 50.130845f, player.transform.position.z);
                }
                break;
            default:
                break;
        }
    }
}
