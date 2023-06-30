using UnityEngine;

public class Superintendent : MonoBehaviour
{
    [SerializeField] GameObject superintendent2ndDialog;
    
    public void ChangeSuperintendent()
    {
        gameObject.SetActive(false);
        superintendent2ndDialog.SetActive(true);
    }
}
