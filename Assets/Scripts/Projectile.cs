using DinoFracture;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Transform playerTransform;
    private float Speed = 6f;
    private Rigidbody rb;
    public delegate Transform GetTransform();
    public static event GetTransform OnGetTransform;

    private void OnEnable()
    {
        //playerTransform = OnGetTransform();
        rb = GetComponent<Rigidbody>();
        if (playerTransform != null)
        {
            Vector3 direction = playerTransform.transform.position - transform.position;
            rb.linearVelocity = new Vector3(direction.x, direction.y, direction.z).normalized * Speed;
            Invoke("Deactivate", 6f);
        }
        else
        {
            Deactivate();
        }
            
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider hitbox)
    {
        if (hitbox.gameObject.CompareTag("Ground"))
        {
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.CannonBallImpact);
            GameObject bulletImpactPool = ObjectPool.instance.GetPooledObject2();
            if (bulletImpactPool != null)
            {
                bulletImpactPool.transform.position = transform.position;
                bulletImpactPool.SetActive(true);
            }
            gameObject.SetActive(false);
        }

        if (hitbox.gameObject.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.CannonBallImpact);
            GameObject bulletImpactPool = ObjectPool.instance.GetPooledObject2();
            if (bulletImpactPool != null)
            {
                bulletImpactPool.transform.position = transform.position;
                bulletImpactPool.SetActive(true);
            }
            gameObject.SetActive(false);
        }

        if (hitbox.gameObject.CompareTag("Player"))
        {
            hitbox.gameObject.GetComponent<FractureGeometry>().Fracture();
        }
    }

   
}
