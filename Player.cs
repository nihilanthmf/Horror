using UnityEngine;
using UnityEngine.Android;

public class Player : MonoBehaviour
{
    // some basic X-Z movement stuff
    [HideInInspector] public float velocity;
    float startDefaultSpeed;
    public float startVelocity { get; private set; }
    Vector3 movement;
    Vector3 gravityVector;

    // movement values
    [SerializeField] float defaultSpeed;
    float gravity = 45;
    public float verticalExplosionForceMultiplier { get; set; } = 0.5f;
    float startGravity;

    // Axis
    float horizontal, vertical;

    [SerializeField] Transform groundCheck;
    [SerializeField] Transform ceilingCheck;

    CharacterController character;
    [SerializeField] LayerMask groundMask;

    Animator animator;

    float crouchTime = 9f;
    float desiredHeight;

    [HideInInspector] public bool toAllowMoving;

    private void Start()
    {
        character = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();

        velocity = defaultSpeed;
        startVelocity = velocity;
        startDefaultSpeed = defaultSpeed;
        startGravity = gravity;

        toAllowMoving = true;   
    }

    /// <summary>
    /// All the movement management happens here
    /// </summary>
    void Movement()
    {
        // this variable represents the speed of the player atm (its cant be == 0, cuz the speed == 0 when the Input.GetAxis("Vertical") == 0)
        // this is the speed only when the wlking button is pressed
        velocity = defaultSpeed;

        movement = transform.right * horizontal + transform.forward * vertical + gravityVector;

        Crouching();

        character.Move(movement * Time.deltaTime);
    }

    /// <summary>
    /// Applying gravity forces to a player
    /// </summary>
    void Gravity()
    {
        if (character.isGrounded && movement.y <= -0.1f)
        {
            gravity = 0;
            movement.y = -1f;
        }
        else
        {
            gravity = startGravity;
        }

        gravityVector.y -= gravity * Time.deltaTime;

        gravityVector = Vector3.MoveTowards(gravityVector, new Vector3(0, gravityVector.y, 0), Time.deltaTime * 15);
    }

    void Crouching()
    {
        character.height = Mathf.MoveTowards(character.height, desiredHeight, crouchTime * Time.deltaTime);

        if (character.height != 1 && character.height != 2)
        {
            gravity = 0;
        }

        defaultSpeed = desiredHeight == 1 ? startDefaultSpeed / 2.3f : startDefaultSpeed; // if you crouch, the default Speed will be divided by X once

        // assigning value to the variable for the height of the player making sure he doesnt stand up inside the wall when the ceiling is too low 
        float maxCheckerRayDistance = 0.35f;
        if (Physics.Raycast(ceilingCheck.position, Vector3.up, maxCheckerRayDistance, LayerMask.GetMask("Default")))
        {
            desiredHeight = 1;
        }
        else
        {
            desiredHeight = Input.GetKey(KeyCode.LeftControl) ? 1 : 2;
        }
    }

    private void Update()
    {
        horizontal = Input.GetAxis("Horizontal") * velocity;
        vertical = Input.GetAxis("Vertical") * velocity;

        if (toAllowMoving)
        {
            character.enabled = true;
            Movement();

            // This line shakes camera by aniamtion; its being played if the velocity fo played is greater then 0
            animator.SetFloat("toWalk", Mathf.Abs(Input.GetAxis("Horizontal")) + Mathf.Abs(Input.GetAxis("Vertical")));
        }
        else
        {
            animator.SetFloat("toWalk", 0);
            character.enabled = false;
        }
        Gravity();
    }
}
