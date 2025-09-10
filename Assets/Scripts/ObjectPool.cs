using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool instance;
    [SerializeField] private GameObject pool1;
    [SerializeField] private int poolAmount1;

    [SerializeField] private GameObject pool2;
    [SerializeField] private int poolAmount2;

    [SerializeField] private GameObject pool3;
    [SerializeField] private int poolAmount3;

    private List<GameObject> pooledObjects1 = new List<GameObject>();
    private List<GameObject> pooledObjects2 = new List<GameObject>();
    private List<GameObject> pooledObjects3 = new List<GameObject>();
    [SerializeField] private bool activePool2;
    [SerializeField] private bool activatePool3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }   
    }

    void Start()
    {
        for (int i = 0; i < poolAmount1; i++)
        {
            GameObject obj = Instantiate(pool1);
            obj.SetActive(false);
            pooledObjects1.Add(obj);
        }

        if (activePool2)
        {
            for (int i = 0; i < poolAmount2; i++)
            {
                GameObject obj = Instantiate(pool2);
                obj.SetActive(false);
                pooledObjects2.Add(obj);
            }
        }

        if (activatePool3)
        {
            for (int i = 0; i < poolAmount3; i++)
            {
                GameObject obj = Instantiate(pool3);
                obj.SetActive(false);
                pooledObjects3.Add(obj);
            }
        }
    }

    public GameObject GetPooledObject1()
    {
        for (int i = 0; i < pooledObjects1.Count; i++)
        {
            if (!pooledObjects1[i].activeInHierarchy)
            {
                return pooledObjects1[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObject2()
    {
        for (int i = 0; i < pooledObjects2.Count; i++)
        {
            if (!pooledObjects2[i].activeInHierarchy)
            {
                return pooledObjects2[i];
            }
        }
        return null;
    }

    public GameObject GetPooledObject3()
    {
        for (int i = 0; i < pooledObjects3.Count; i++)
        {
            if (!pooledObjects3[i].activeInHierarchy)
            {
                return pooledObjects3[i];
            }
        }
        return null;
    }
}
