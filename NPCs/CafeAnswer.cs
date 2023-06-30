using UnityEngine;

public class CafeAnswer : MonoBehaviour
{
    [SerializeField] GameObject custardTartGameObject, chocolateFilledCroissantGameObject, donutGameObject;
    [SerializeField] NPC cafeClerkNPC;
    [SerializeField] CafeAnswer cafeClerkAnswer;
    GameObject currentFoodGameObject;
    [HideInInspector] public GameObject currentFoodInstance;
    [SerializeField] Player player;

    public void SpawnFood()
    {
        switch (cafeClerkNPC.indexAnswerPlayerChoseInDialog)
        {
            case 0:
                currentFoodGameObject = custardTartGameObject;
                break;
            case 1:
                currentFoodGameObject = chocolateFilledCroissantGameObject;
                break;
            case 2:
                currentFoodGameObject = donutGameObject;
                break;
            default:
                break;
        }

        currentFoodInstance = Instantiate(currentFoodGameObject, custardTartGameObject.transform.parent);
        currentFoodInstance.SetActive(true);
    }

    private void Update() // Ñהוכאעü קעמבû AfterResetingDialogRelatedValues() גûחûגאככמסב ÎÄÈÍ ÐÀÇ א םו ךאזהûי פנויל
    {
        // If the player has NOT eaten the food, DO NOT allow him to buy again
        if (player.toAllowMoving)
        {
            if (cafeClerkAnswer.currentFoodInstance != null)
            {
                cafeClerkNPC.dialogLines = new string[] { "Enjoying your meal mate?" };
                cafeClerkNPC.answers = new string[0];
                cafeClerkNPC.AfterResetingDialogRelatedValues();
                //cafeClerkNPCNoMoney.gameObject.SetActive(true);
                //cafeClerkNPC.gameObject.SetActive(false);
            }
            else // If the player has eaten the food, allow him to buy again
            {
                cafeClerkNPC.dialogLines = cafeClerkNPC.startDialogLines;
                cafeClerkNPC.answers = cafeClerkNPC.startAnswers;
                cafeClerkNPC.AfterResetingDialogRelatedValues();
                //cafeClerkNPC.gameObject.SetActive(true);
                //cafeClerkNPCNoMoney.gameObject.SetActive(false);
            }
        }
    }
}
