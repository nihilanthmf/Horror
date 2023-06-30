using System.Collections.Generic;
using UnityEngine;

public class NPC : MonoBehaviour
{
    public string[] dialogLines;
    public string[] answers;
    public string[] differentSayingAfterAnswer;

    [SerializeField] Dialog dialog;
    public Transform forPlayerToLookAt;
    [HideInInspector] public bool hasFinishedDialog;
    [HideInInspector] public bool toCheckToStartNewDialogImmediatelyAfterAnswers;

    List<string>[] dividedAnswers;
    List<string>[] dividedDialogLines;

    [HideInInspector] public string[] startDialogLines;
    [HideInInspector] public string[] startAnswers;

    int currentAnswerIndex = 1;
    int currentDialogIndex = 1;
    string startTag;

    [HideInInspector] public bool dialogEnded;
    bool dialogHasEverStarted;

    [HideInInspector] public int indexAnswerPlayerChoseInDialog;

    /* HOW TO ADD DIALOGS
     *  1. Add spaces where answers will be
     *  2. Separate answer groups with space
     */


    public void AfterResetingDialogRelatedValues()
    {
        int spacesInDialogLines = 0;
        foreach (var item in dialogLines)
        {
            if (item == "")
            {
                spacesInDialogLines++;
            }
        }
        int spacesInAnswers = 0;
        foreach (var item in answers)
        {
            if (item == "")
            {
                spacesInAnswers++;
            }
        }

        dividedDialogLines = new List<string>[spacesInDialogLines + 1];
        for (int i = 0; i < dividedDialogLines.Length; i++)
        {
            dividedDialogLines[i] = new List<string>();
        }
        dividedAnswers = new List<string>[spacesInAnswers + 1];
        for (int i = 0; i < dividedAnswers.Length; i++)
        {
            dividedAnswers[i] = new List<string>();
        }

        // Putting all the lines into the array
        int firstIndex = 0;
        for (int i = 0; i < dialogLines.Length; i++)
        {
            if (dialogLines[i] == "")
            {
                firstIndex++;
            }
            else
            {
                dividedDialogLines[firstIndex].Add(dialogLines[i]);
            }
        }

        firstIndex = 0;
        for (int i = 0; i < answers.Length; i++)
        {
            if (answers[i] == "")
            {
                firstIndex++;
            }
            else
            {
                dividedAnswers[firstIndex].Add(answers[i]);
            }
        }
    }

    private void Start()
    {
        startDialogLines = dialogLines;
        startAnswers = answers;
        startTag = gameObject.tag;

        AfterResetingDialogRelatedValues();
    }

    // The first dialog
    public void StartConversation()
    {
        dialogEnded = false;
        dialogHasEverStarted = true;
        toCheckToStartNewDialogImmediatelyAfterAnswers = false;
        if (answers.Length != 0)
        {
            dialog.Say(dividedDialogLines[0], dividedAnswers[0], this);
        }
        else
        {
            dialog.Say(dividedDialogLines[0], this);
        }
    }

    // The following dialogs
    void StartConversation(List<string> dialogLine, List<string> currentAnswers)
    {
        toCheckToStartNewDialogImmediatelyAfterAnswers = false;
        dialog.Say(dialogLine, currentAnswers, this);
    }

    // Without answers
    void StartConversation(List<string> dialogLine)
    {
        toCheckToStartNewDialogImmediatelyAfterAnswers = false;
        dialog.Say(dialogLine, this);
    }

    public void AfterAnswer()
    {
        CafeAnswer cafeAnswer;
        TryGetComponent(out cafeAnswer);
        if (cafeAnswer)
        {
            cafeAnswer.SpawnFood();
        }
    }

    private void Update()
    {
        if (toCheckToStartNewDialogImmediatelyAfterAnswers)
        {
            if (currentDialogIndex < dividedDialogLines.Length) // !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! currentAnswerIndex == dividedAnswers.Length
            {
                if (currentAnswerIndex < dividedAnswers.Length)
                {
                    StartConversation(dividedDialogLines[currentDialogIndex], dividedAnswers[currentAnswerIndex]);
                }
                else
                {
                    StartConversation(dividedDialogLines[currentDialogIndex]);
                }
                currentAnswerIndex++;
                currentDialogIndex++;
            }
            if (currentDialogIndex >= dividedDialogLines.Length)
            {
                currentAnswerIndex = 1;
                currentDialogIndex = 1;

                toCheckToStartNewDialogImmediatelyAfterAnswers = false;
            }
        }

        // The end of the dialog
        if (dividedDialogLines != null && currentDialogIndex >= dividedDialogLines.Length)
        {
            gameObject.tag = startTag;
            dialogEnded = true;
        }

        if (dialogEnded && dialogHasEverStarted)
        {
            Superintendent superintendent;
            TryGetComponent(out superintendent);
            if (superintendent)
            {
                superintendent.ChangeSuperintendent();
            }
        }
    }
}
