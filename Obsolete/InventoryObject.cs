using UnityEngine;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    //[SerializeField] Objects obj;
    enum Objects { girlfriendDoorKey, knife };

    public Image correspondingImage;

    [HideInInspector] public bool isActive;


}
