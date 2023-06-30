using System.Collections.Generic;
using UnityEngine;

public class ElevatorCollider : MonoBehaviour
{
    public List<GameObject> objectsInside {  get; private set; }

    private void Start()
    {
        objectsInside = new List<GameObject>();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            if (!objectsInside.Contains(other.gameObject))
            {
                objectsInside.Add(other.gameObject);
            }
        }
    }
}
