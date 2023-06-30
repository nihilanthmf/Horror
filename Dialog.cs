using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Dialog : MonoBehaviour
{
    [SerializeField] TMP_Text dialogText;
    [SerializeField] Transform answersParent;
    [SerializeField] Answer answerSample;
    [SerializeField] Player player;
    PlayerInteraction playerInteraction;
    [SerializeField] CameraController cameraController;

    List<string> currentDialogLines;
    List<string> currentAnswers;

    [HideInInspector] public bool startTextAppearing;
    [HideInInspector] public bool toAllowSkip;

    Transform currentNpcForPlayerToLookAt;

    float timeToDrawTheNextCharacter = 0; // 0.035f;

    [HideInInspector] public NPC currentNPC;

    UnityAction executeAfterDialog;

    int currentLineIndex = 0;
    int currentAnswerIndex;

    bool hasAnswers;
    bool allowMovingOnce;

    private void Start()
    {
        playerInteraction = player.GetComponent<PlayerInteraction>();
    }

    public void Say(List<string> dialogLines, List<string> answers, NPC npc)
    {
        currentDialogLines = dialogLines;
        currentAnswers = answers;
        startTextAppearing = true;
        playerInteraction.isTalking = true;

        npc.tag = "Untagged";

        currentNPC = npc;
        currentNpcForPlayerToLookAt = npc.forPlayerToLookAt;

        CheckForLineWithAnswers();

        StartCoroutine(TextAppearing(currentDialogLines[currentLineIndex]));
    }

    /// <summary>
    /// Without answers
    /// </summary>
    /// <param name="dialogLines"></param>
    /// <param name="npc"></param>
    public void Say(List<string> dialogLines, NPC npc)
    {
        currentDialogLines = dialogLines;
        currentAnswers = null;
        startTextAppearing = true;
        playerInteraction.isTalking = true;

        npc.tag = "Untagged";

        currentNPC = npc;
        currentNpcForPlayerToLookAt = npc.forPlayerToLookAt;

        CheckForLineWithAnswers();

        StartCoroutine(TextAppearing(currentDialogLines[currentLineIndex]));
    }
    /// <summary>
    /// Method for the player thoughts
    /// </summary>
    /// <param name="dialogLines"></param>
    public void Say(List<string> dialogLines, UnityAction methodAfterDialogEnds)
    {
        currentDialogLines = dialogLines;
        currentAnswers = null;
        startTextAppearing = true;
        playerInteraction.isTalking = true;

        currentNpcForPlayerToLookAt = null;
        executeAfterDialog = methodAfterDialogEnds;

        CheckForLineWithAnswers();

        StartCoroutine(TextAppearing(currentDialogLines[currentLineIndex]));
    }

    public void Skip()
    {
        // Clearing the previous stuff
        dialogText.text = "";

        for (int i = 1; i < answersParent.childCount; i++)
        {
            Destroy(answersParent.GetChild(i).gameObject);
        }

        currentLineIndex += 1;

        // After the sequence of lines is finished and the player presses button to exit the dialog
        if (currentLineIndex >= currentDialogLines.Count)
        {
            startTextAppearing = false;
            currentLineIndex = 0;
        }
        // if the dialog is still going
        else
        {
            StartCoroutine(TextAppearing(currentDialogLines[currentLineIndex]));
            CheckForLineWithAnswers();
        }
    }

    void CheckForLineWithAnswers()
    {
        if (currentLineIndex == currentDialogLines.Count - 1 && currentAnswers != null)
        {
            hasAnswers = true;

            for (int i = 0; i < currentAnswers.Count; i++)
            {
                Answer answerInstance = Instantiate(answerSample, answersParent);

                answerInstance.index = i;
                answerInstance.npc = currentNPC;
                answerInstance.dialog = this;
                answerInstance.GetComponentInChildren<TMP_Text>().text = currentAnswers[i];
                answerInstance.gameObject.SetActive(true);
            }

            if (currentAnswerIndex < currentAnswers.Count - 1)
            {
                currentAnswerIndex += 1;
            }
        }
        else
        {
            hasAnswers = false;
        }
    }

    public void ChangeSayingDueToAnswer(int asnwerIndex, NPC npc)
    {
        if (npc.differentSayingAfterAnswer.Length > 0)
        {
            List<string> saying = new List<string>  { npc.differentSayingAfterAnswer[asnwerIndex] };

            Say(saying, currentNPC);
        }
    }

    private void Update()
    {
        if (startTextAppearing)
        {
            Cursor.lockState = CursorLockMode.Confined;
            player.toAllowMoving = false;
            cameraController.toAllowMoving = false;

            if (currentNpcForPlayerToLookAt != null)
            {
                cameraController.transform.LookAt(currentNpcForPlayerToLookAt);
            }

            // After the player presses the button to skip to the next dialog line
            // Checking if it's not a question not to skip it
            if (toAllowSkip && !hasAnswers && 
                (Input.GetKeyDown(KeyCode.F) || Input.GetMouseButtonDown(0)))
            {
                Skip();
            }
            allowMovingOnce = false;
        }
        else
        {
            if (!allowMovingOnce)
            {
                Cursor.lockState = CursorLockMode.Locked;
                player.toAllowMoving = true;
                playerInteraction.isTalking = false;
                cameraController.toAllowMoving = true;
                if (currentNPC != null)
                {
                    currentNPC.hasFinishedDialog = true;
                }

                allowMovingOnce = true;

                if (executeAfterDialog != null)
                {
                    executeAfterDialog.Invoke();
                    executeAfterDialog = null; // reseting it 
                }
            }
        }
    }

    IEnumerator TextAppearing(string str)
    {
        toAllowSkip = false;

        for (int i = 0; i < str.Length; i++)
        {
            dialogText.text += str[i];
            yield return new WaitForSeconds(timeToDrawTheNextCharacter);
        }
        yield return new WaitForSeconds(0.25f);

        toAllowSkip = true;
    }
}
