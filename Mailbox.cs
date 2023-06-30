using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Mailbox : MonoBehaviour
{
    bool opened;
    bool hasBeenOpened;
    Animator animator;
    [SerializeField] Dialog dialog;

    enum MailboxType {gameStart, player, cult};
    [SerializeField] MailboxType mailboxType;
    [SerializeField] GameObject cultMailBox;
    [SerializeField] StoryPermission storyPermission;

    List<string> dialogLinesGameStart;
    List<string> dialogLinesCultStory;

    private void Start()
    {
        animator = GetComponent<Animator>();
        dialogLinesGameStart = new List<string>() { "Ah, just some random advertisement", "I have plenty of time to kill, what should I do next?" };
        dialogLinesCultStory = new List<string>() { "This mailbox appears to be unlocked. But I really shouldn’t mess with other people’s mail boxes...",
                                                    "Yet I have this strong desire to do so" };
    }

    public void Interact()
    {
        if (mailboxType != MailboxType.cult)
        {
            if (opened)
            {
                animator.SetBool("ToOpen", false);
                opened = false;
            }
            else
            {
                animator.SetBool("ToOpen", true);
                opened = true;
            }
        }
        else // start Cult story
        {
            UnityAction askToStartCultShortStory = CultStartPermit;
            dialog.Say(dialogLinesCultStory, askToStartCultShortStory);
        }
    }

    public void ExecuteAfterOpen() // via animator
    {
        if (mailboxType == MailboxType.gameStart && !hasBeenOpened)
        {
            UnityAction nullAction = null;
            dialog.Say(dialogLinesGameStart, nullAction);

            cultMailBox.tag = "Mailbox";
            hasBeenOpened = true;
        }
    }

    void CultStartPermit()
    {
        storyPermission.AskForStory("Open the mail box?");
    }
}
