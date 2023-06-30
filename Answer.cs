using UnityEngine;

public class Answer : MonoBehaviour
{
    [HideInInspector] public Dialog dialog;
    [SerializeField] CafeAnswer cafeAnswer;
    [HideInInspector] public NPC npc;
    [HideInInspector] public int index;

    public void OnClick()
    {
        if (dialog.toAllowSkip)
        {
            dialog.Skip();
            dialog.currentNPC.toCheckToStartNewDialogImmediatelyAfterAnswers = true;

            CheckForAnswerEventComponents();
        }
    }

    void CheckForAnswerEventComponents()
    {
        dialog.ChangeSayingDueToAnswer(index, npc);
        npc.indexAnswerPlayerChoseInDialog = index;

        npc.AfterAnswer();
    }
}
