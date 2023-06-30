using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject inGameUI;
    [SerializeField] GameObject settings;
    [SerializeField] CameraController cameraController;
    bool onPause;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if (onPause)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        pauseMenu.SetActive(true);
        settings.SetActive(false);
        inGameUI.SetActive(false);
        cameraController.DisableCameraMoving();
        Cursor.lockState = CursorLockMode.Confined;

        Time.timeScale = 0;
        onPause = true;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(false);
        inGameUI.SetActive(true);
        cameraController.EnableCameraMoving();
        Cursor.lockState = CursorLockMode.Locked;

        Time.timeScale = 1;
        onPause = false;
    }

    public void EnableSettings()
    {
        settings.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void DisableSettings()
    {
        settings.SetActive(false);
        pauseMenu.SetActive(true);
    }
}
