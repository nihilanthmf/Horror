using UnityEngine;

public class JournalInstance : MonoBehaviour
{
    public string title;
    public string text;
    [SerializeField] JournalUI journalUI;

    public void Read()
    {
        journalUI.gameObject.SetActive(true);

        journalUI.SetJournal(title, text);
    }
}
