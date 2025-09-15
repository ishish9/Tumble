using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using System.Collections.Generic;

public class CoinSpinManager : MonoBehaviour
{
    [SerializeField] private List <Transform> coinArray = new List<Transform>();
    private int index;
    private int childCount;
    void Start()
    {
        childCount = gameObject.transform.childCount;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            coinArray.Add(gameObject.transform.GetChild(i).GetComponent<Transform>());
        }
    }

    void Update()
    {
        
        if (index <= childCount -1)
        {
           
            //Mathf.Clamp(index, 0, childCount);
            coinArray[index].transform.Rotate(Vector3.up * 600 * Time.deltaTime);
            index++;
        }
        else
        {
            index = 0;
        }


    }
}
