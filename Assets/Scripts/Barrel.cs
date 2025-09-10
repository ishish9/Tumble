using DinoFracture;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public delegate Transform GetTransform();
    public static event Action OnBarrelExplode;
    [SerializeField] public GameObject barrelExplodePrefab;

    private void OnTriggerEnter(Collider hitbox)
    {       
        if (hitbox.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.BarrelExplode);
            Instantiate(barrelExplodePrefab, transform.position,Quaternion.identity);
            hitbox.gameObject.GetComponent<FractureGeometry>().Fracture();
            OnBarrelExplode?.Invoke();
            gameObject.SetActive(false);
        }
    }
}
