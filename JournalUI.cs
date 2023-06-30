using TMPro;
using UnityEngine;

public class JournalUI : MonoBehaviour
{
    [SerializeField] TMP_Text title, note;
    [SerializeField] Player player;
    [SerializeField] CameraController cameraController;

    public void SetJournal(string title, string note)
    {
        player.toAllowMoving = false;
        cameraController.toAllowMoving = false;

        this.title.text = title;

        SeparatingLines(note);
    }

    void SeparatingLines(string txt)
    {
        note.text = "";
        string[] splitArray = txt.Split(char.Parse("@"));
        for (int i = 0; i < splitArray.Length; i++)
        {
            note.text += splitArray[i] + "\n";
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            gameObject.SetActive(false);

            player.toAllowMoving = true;
            cameraController.toAllowMoving = true;
        }
    }
}
