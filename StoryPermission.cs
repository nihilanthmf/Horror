using TMPro;
using UnityEngine;

public class StoryPermission : MonoBehaviour
{
    [SerializeField] GameObject yesNoUI;
    [SerializeField] TMP_Text startShortStoryText;
    [SerializeField] Player player;
    PlayerInteraction playerInteraction;
    CameraController cameraController;

    private void Start()
    {
        cameraController = Camera.main.gameObject.GetComponent<CameraController>();
        playerInteraction = player.gameObject.GetComponent<PlayerInteraction>();
    }

    public void AskForStory(string yesNoText)
    {
        Cursor.lockState = CursorLockMode.Confined;
        player.toAllowMoving = false;
        cameraController.toAllowMoving = false;
        playerInteraction.isTalking = true;

        yesNoUI.SetActive(true);
        startShortStoryText.text = yesNoText;
    }

    public void Yes()
    {

    }

    public void No()
    {
        yesNoUI.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        player.toAllowMoving = true;
        cameraController.toAllowMoving = true;
        playerInteraction.isTalking = false;
    }
}
