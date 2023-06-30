using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerGameStart : MonoBehaviour
{
    [SerializeField] Dialog dialog;
    [SerializeField] RuntimeAnimatorController defaultController;
    [SerializeField] TaskList taskList;

    [Header("Task stuff")]
    [SerializeField] GameObject foldedClothes;
    [SerializeField] Collider foldedClothesPlace;
    [SerializeField] GameObject cafeClerk;
    [SerializeField] GameObject mailbox;

    List<string> dialogLines;
    Animator animator;

    private void Start()
    {
        dialogLines = new List<string>() { "How long have I slept?", "I guess I got totally drained from all that unpacking done in the morning", "Well, just one more box and I’m done" };
        animator = GetComponent<Animator>();
        animator.Play("WakingUp");
    }

    void TriggerStartDialog() // via Animator
    {
        animator.runtimeAnimatorController = defaultController;
        UnityAction action = SetTaskFoldClothes;
        dialog.Say(dialogLines, action);
    }

    void SetTaskFoldClothes()
    {
        UnityAction executeWhenCompleted = SetTaskBuyFood;
        taskList.SetTaskObjectPlacement("Put folded clothes inside the wardrobe", foldedClothes, foldedClothesPlace, executeWhenCompleted);
    }

    void SetTaskBuyFood()
    {
        UnityAction executeWhenCompleted = SetTaskCheckMailbox;
        taskList.SetTaskClick("Talk to cafe clerk", cafeClerk, executeWhenCompleted);
    }

    void SetTaskCheckMailbox()
    {
        mailbox.tag = "Mailbox";
        taskList.SetTaskClick("Check mailbox", mailbox, null);
    }
}
