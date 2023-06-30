using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] GameObject player;
    Animator cameraAnimator;

    [HideInInspector] public float mouseSensivity = 0.9f;
    float startSensivity;
    [HideInInspector] public bool toAllowMoving;
    public float xRotation { get; set; }

    float x;
    float y;

    Quaternion currentPlayerRotation;
    Quaternion currentCameraRotation;


    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraAnimator = transform.GetComponent<Animator>();
        startSensivity = mouseSensivity;

        toAllowMoving = true;
    }

    private void LateUpdate()
    {
        x = Input.GetAxis("Mouse X") * mouseSensivity;
        y = Input.GetAxis("Mouse Y") * mouseSensivity;

        xRotation = Mathf.Clamp(xRotation, -90, 80);

        if (toAllowMoving)
        {
            MovingCamera();

            cameraAnimator.enabled = true;
            cameraAnimator.SetFloat("Leaning", Input.GetAxisRaw("Horizontal"));

            currentPlayerRotation = player.transform.rotation;
            currentCameraRotation = transform.rotation;
        }
        else
        {
            cameraAnimator.SetFloat("Leaning", 0);
            cameraAnimator.enabled = false;

            player.transform.rotation = currentPlayerRotation;
            transform.rotation = currentCameraRotation;
        }
    }

    public void DisableCameraMoving()
    {
        mouseSensivity = 0;
    }

    public void EnableCameraMoving()
    {
        mouseSensivity = startSensivity;
    }


    void MovingCamera()
    {
        player.transform.Rotate(new Vector3(0, x, 0));
        transform.localRotation = Quaternion.Euler(xRotation, transform.localEulerAngles.y, transform.localEulerAngles.z);

        float toSubtract = y;
        xRotation -= toSubtract;
    }
}
