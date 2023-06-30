using UnityEngine;
using System.Collections;
using System.Net.NetworkInformation;
using System.Collections.Generic;

public class Elevator : MonoBehaviour
{
    public Animator doorsAnimator;

    [SerializeField] ElevatorManager elevatorManager;

    [SerializeField] GameObject elevatorCallingButton;

    [SerializeField] ElevatorCollider elevatorCollider;
    AudioSource bing;

    private void Start()
    {
        bing = doorsAnimator.gameObject.GetComponent<AudioSource>();
    }

    public IEnumerator OpenDoors(float timeBeforeOpeningDoors, Elevator elevator)
    {
        yield return new WaitForSeconds(timeBeforeOpeningDoors);
        bing.Play();
        elevatorCallingButton.tag = "Untagged";
        elevator.doorsAnimator.SetBool("ToOpen", true);
        elevator.doorsAnimator.SetBool("ToClose", false);

        yield return new WaitForSeconds(elevatorManager.timeForOpenDoor);
        CloseDoors(elevator);
        elevatorCallingButton.tag = "Elevator Calling Button";
    }

    public IEnumerator Departure(int floor, int currentFloor)
    {
        if (floor != currentFloor)
        {
            elevatorManager.MakeButtonsInactive();
            CloseDoors(this);

            yield return new WaitForSeconds(elevatorManager.timeToPassOneFloor);

            elevatorManager.PlayerTransport(floor, currentFloor, elevatorCollider.objectsInside);

            StartCoroutine(OpenDoors(0, elevatorManager.elevators[floor]));
            elevatorManager.MakeButtonsActive();
        }
        else
        {
            StartCoroutine(OpenDoors(0, this));
        }
    }

    void CloseDoors(Elevator elevator)
    {
        elevator.doorsAnimator.SetBool("ToClose", true);
        elevator.doorsAnimator.SetBool("ToOpen", false);
    }

    public List<GameObject> CheckForGoodsInside()
    {
        return elevatorCollider.objectsInside;
    }
}
