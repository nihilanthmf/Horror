using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    Animator inventoryAnimator;
    List<InventoryObject> inventoryList;
    [SerializeField] Transform[] imageParents;
    [SerializeField] PlayerInteraction playerInteraction;

    private void Start()
    {
        inventoryAnimator = GetComponent<Animator>();
        inventoryList = new List<InventoryObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            InventoryAppear();
        }
        if (Input.GetKeyUp(KeyCode.Tab))
        {
            InventoryDisappear();
        }
        ActivatingObjects();
    }

    void ActivatingObjects()
    {
        for (int i = 0; i < 9; i++)
        {
            if (Input.GetKeyDown((i + 1).ToString()) && inventoryList[i] != null)
            {
                if (!inventoryList[i].isActive)
                {
                    inventoryList[i].gameObject.SetActive(true);
                    inventoryList[i].isActive = true;

                    playerInteraction.Release(playerInteraction.heldObject);
                }
                else
                {
                    inventoryList[i].gameObject.SetActive(false);
                    inventoryList[i].isActive = false;
                }
            }
        }
    }

    public void AddObject(InventoryObject obj)
    {
        inventoryList.Add(obj);
        obj.correspondingImage.transform.SetParent(imageParents[imageParents.Length - 1]);
        obj.correspondingImage.transform.localPosition = Vector3.zero;

        obj.transform.SetParent(playerInteraction.objectHolder.transform);
        obj.transform.localPosition = Vector3.zero;
        obj.gameObject.SetActive(false);
    }

    void InventoryAppear()
    {
        inventoryAnimator.SetBool("ToAppear", true);
    }

    void InventoryDisappear()
    {
        inventoryAnimator.SetBool("ToAppear", false);
    }
}
