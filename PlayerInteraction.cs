using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    float interactionDistance = 3;
    float maxHoldingDistance = 4;

    [HideInInspector] public bool isHolding;
    [HideInInspector] public bool isTalking;

    [HideInInspector] public PickableObject heldObject; // the one that is being held right now

    [SerializeField] LayerMask almostEverything;

    [SerializeField] TMP_Text interactableName;

    Player player;

    Camera mainCamera;
    public Rigidbody objectHolder;
    public bool seated;

    private void Start()
    {
        mainCamera = Camera.main;
        player = GetComponent<Player>();
    }

    private void Update()
    {
        Interaction();
        ReleaseManager();
    }

    void CheckForTaskStuff(GameObject objectToCheck)
    {
        TaskObject taskObject = null;
        objectToCheck.TryGetComponent(out taskObject);
        if (taskObject != null && taskObject.clickToComplete)
        {
            taskObject.TryCompletingTask();
        }
    }

    void Interaction()
    {
        RaycastHit hit;
        if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactionDistance, almostEverything))
        {
            if (!isHolding && !isTalking)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    CheckForTaskStuff(hit.transform.gameObject);
                }

                if (hit.transform.gameObject.layer == 6) 
                {
                    interactableName.text = "Pick";

                    if (Input.GetMouseButtonDown(0))
                    {
                        PickableObject pickableObject = hit.transform.gameObject.GetComponent<PickableObject>();
                        if (pickableObject != null)
                        {
                            Take(pickableObject);
                        }
                    }
                }
                else if (hit.transform.gameObject.tag == "Elevator Calling Button")
                {
                    interactableName.text = "Call elevator";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Elevator elevator = hit.transform.parent.GetComponent<Elevator>();
                        StartCoroutine(elevator.OpenDoors(0, elevator));
                    }
                }
                else if (hit.transform.gameObject.tag == "Elevator Floor Button")
                {
                    ElevatorButton elevatorButton = hit.transform.GetComponent<ElevatorButton>();

                    string prefixToFloorNumber = "th";
                    if (elevatorButton.floor == 1) prefixToFloorNumber = "st";
                    if (elevatorButton.floor == 2) prefixToFloorNumber = "nd";
                    if (elevatorButton.floor == 3) prefixToFloorNumber = "rd";

                    if (elevatorButton.floor == 0)
                    {
                        interactableName.text = "Ground floor";
                    }
                    else
                    {
                        interactableName.text = elevatorButton.floor + prefixToFloorNumber + " floor";
                    }

                    if (Input.GetMouseButtonDown(0))
                    {
                        if (elevatorButton)
                        {
                            Elevator elevator = hit.transform.parent.parent.GetComponent<Elevator>();
                            int currentFloor = 0;
                            switch (elevator.tag)
                            {
                                case "Ground Floor Elevator":
                                    currentFloor = 0;
                                    StartCoroutine(elevator.Departure(elevatorButton.floor, currentFloor));
                                    break;
                                case "1st Floor Elevator":
                                    currentFloor = 1;
                                    StartCoroutine(elevator.Departure(elevatorButton.floor, currentFloor));
                                    break;
                                case "2nd Floor Elevator":
                                    currentFloor = 2;
                                    StartCoroutine(elevator.Departure(elevatorButton.floor, currentFloor));
                                    break;
                                case "3rd Floor Elevator":
                                    currentFloor = 3;
                                    StartCoroutine(elevator.Departure(elevatorButton.floor, currentFloor));
                                    break;
                                case "4th Floor Elevator":
                                    currentFloor = 4;
                                    StartCoroutine(elevator.Departure(elevatorButton.floor, currentFloor));
                                    break;
                                case "5th Floor Elevator":
                                    currentFloor = 5;
                                    StartCoroutine(elevator.Departure(elevatorButton.floor, currentFloor));
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                    
                }
                else if (hit.transform.gameObject.tag == "NPC" && player.toAllowMoving)
                {
                    interactableName.text = "Talk";
                    if (Input.GetMouseButtonDown(0))
                    {
                        NPC npc = hit.transform.GetComponent<NPC>();
                        npc.StartConversation();
                    }
                }
                else if (hit.transform.gameObject.tag == "Journal")
                {
                    interactableName.text = "Read";
                    if (Input.GetMouseButtonDown(0))
                    {
                        JournalInstance journalInstance = hit.transform.GetComponent<JournalInstance>();
                        journalInstance.Read();
                    }
                }
                else if (hit.transform.gameObject.tag == "Radio")
                {
                    interactableName.text = "Radio";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Radio radio = hit.transform.GetComponent<Radio>();
                        if (radio.muted)
                        {
                            radio.UnMute();
                        }
                        else
                        {
                            radio.Mute();
                        }
                    }
                }
                else if (hit.transform.gameObject.tag == "Coffee")
                {
                    interactableName.text = "Drink";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Coffee coffee = hit.transform.GetComponent<Coffee>();
                        coffee.Drink();
                    }
                }
                else if (hit.transform.gameObject.tag == "Edible")
                {
                    interactableName.text = "Eat";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Edible edible = hit.transform.GetComponent<Edible>();
                        edible.Eat();
                    }
                }
                else if (hit.transform.gameObject.tag == "Chair" && !seated)
                {
                    interactableName.text = "Seat";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Chair chair = hit.transform.GetComponent<Chair>();
                        chair.Seat();
                    }
                }
                else if (hit.transform.gameObject.tag == "Wardrobe")
                {
                    interactableName.text = "Use wardrobe";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Wardrobe wardrobe = hit.transform.GetComponent<Wardrobe>();
                        wardrobe.OpenClose();
                    }
                }
                else if (hit.transform.gameObject.tag == "Mailbox")
                {
                    interactableName.text = "Check mailbox";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Mailbox mailbox = hit.transform.GetComponent<Mailbox>();
                        mailbox.Interact();
                    }
                }
                else if (hit.transform.gameObject.tag == "Toilet")
                {
                    interactableName.text = "Flush";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Toilet toilet = hit.transform.parent.GetComponent<Toilet>(); //PARENT!!!
                        toilet.Flush();
                    }
                }
                else if (hit.transform.gameObject.tag == "Fridge")
                {
                    interactableName.text = "Open fridge";
                    if (Input.GetMouseButtonDown(0))
                    {
                        Fridge fridge = hit.transform.GetComponent<Fridge>();
                        fridge.Interact();
                    }
                }
                else
                {
                    interactableName.text = "";
                }
            }
        }
        else
        {
            interactableName.text = "";
        }
    }

    void ReleaseManager()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (heldObject != null)
            {
                Release(heldObject);
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (heldObject != null)
            {
                Throw(heldObject, 35);
            }
        }

        if (heldObject != null && Vector3.Distance(mainCamera.transform.position, heldObject.transform.position) > maxHoldingDistance)
        {
            Release(heldObject);
        }
    }



    void Take(PickableObject pickableObject)
    {
        heldObject = pickableObject;

        pickableObject.SetPlayerController(this);

        Rigidbody productRB = pickableObject.gameObject.GetComponent<Rigidbody>();
        productRB.useGravity = false;

        pickableObject.transform.SetParent(null);

        pickableObject.isHeld = true;
        isHolding = true;
    }

    public void Release(PickableObject pickableObject)
    {
        Rigidbody productRb = pickableObject.gameObject.GetComponent<Rigidbody>();
        productRb.isKinematic = false;
        productRb.useGravity = true;
        productRb.freezeRotation = false;

        pickableObject.isHeld = false;
        isHolding = false;
        heldObject = null;
    }

    void Throw(PickableObject pickableObject, float force) // for products and body parts
    {
        Release(pickableObject);
        pickableObject.GetComponent<Rigidbody>().AddForce(mainCamera.transform.forward * force, ForceMode.Impulse);
    }
}
