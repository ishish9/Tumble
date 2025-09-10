using System.Collections;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField] private ParticleSystem cannonSmoke;
    [SerializeField] private int FireAmount;
    [SerializeField] private int timeBetweenFire;
    [SerializeField] private int beginFireDistance;
    [SerializeField] private bool cannon;
    [SerializeField] private bool totem;
    private bool beginFire = true;
    private LookAt lookAt;

    private void Awake()
    {
        lookAt = GetComponent<LookAt>();
    }
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, lookAt.ReturnPlayerTransform().position);
        if (distance < beginFireDistance && beginFire)
        {
            beginFire = false;
            StartCoroutine(w1());
            return;          
        }
        else if (distance > beginFireDistance)
        {
            
        }
    }

    IEnumerator w1()
    {
        if (cannon)
        {
            for (int j = 0; j < FireAmount; j++)
            {
                yield return new WaitForSeconds(timeBetweenFire);
                GameObject pooledObj = ObjectPool.instance.GetPooledObject1();

                if (pooledObj != null)
                {
                    pooledObj.transform.position = gameObject.transform.position;
                    pooledObj.SetActive(true);
                    AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.CannonFire);
                    cannonSmoke.Play();
                }
            }
        }
        if (totem)
        {
            for (int j = 0; j < FireAmount; j++)
            {
                yield return new WaitForSeconds(timeBetweenFire);
                GameObject pooledObj = ObjectPool.instance.GetPooledObject3();
                pooledObj.GetComponent<Projectile>().playerTransform = lookAt.ReturnPlayerTransform();
                if (pooledObj != null)
                {
                    pooledObj.transform.position = gameObject.transform.position;
                    pooledObj.transform.rotation = transform.rotation;
                    pooledObj.SetActive(true);
                    AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.Dart);
                    cannonSmoke.Play();
                }
            }
        }
    }
}
