using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartUp : MonoBehaviour
{
    public GameObject[] BigVegitation;
    public GameObject[] SmallVegitation;
    public GameObject[] BigBounds;
    public GameObject[] SmallBounds;
    public int numTrees;
    public int numGrass;

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < BigBounds.Length; i++)
        {
            BoxCollider collider = BigBounds[i].GetComponent<BoxCollider>();

            for (int j = 0; j < numTrees; j++)
            {
                Instantiate(BigVegitation[RandomIndex(BigVegitation)], 
                            RandomPosition(collider), 
                            RandomRotation());
            }
        }

        for (int i = 0;i < SmallBounds.Length; i++)
        {
            BoxCollider collider = SmallBounds[i].GetComponent<BoxCollider>();

            for (int j = 0;j < numGrass; j++)
            {
                Instantiate(SmallVegitation[RandomIndex(SmallVegitation)],
                            RandomPosition(collider),
                            RandomRotation());
            }
        }

    }

    int RandomIndex(GameObject[] list)
    {
        return Random.Range(0, list.Length);
    }

    Vector3 RandomPosition(BoxCollider area)
    {
        Bounds bounds = area.bounds;
        float x = Random.Range(bounds.min.x, bounds.max.x);
        float z = Random.Range(bounds.min.z, bounds.max.z);

        return new Vector3(x, 0 ,z);
    }

    Quaternion RandomRotation()
    {
        return Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
    }

}
