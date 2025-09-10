using UnityEngine;
using System;
public class KillPlayer : MonoBehaviour
{
    public static event Action OnFall;// Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnFall?.Invoke();
        }
    }
}
