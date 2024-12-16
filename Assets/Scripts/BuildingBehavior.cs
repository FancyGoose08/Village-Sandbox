using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform)
        {
            if (child.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            else
            {
                Debug.LogError("Object does not have a rigidbody : " +  child.name);
            }
        }
    }
}
