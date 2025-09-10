using UnityEngine;

public class LookAt : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float lookSpeed;
    [SerializeField] private int beginLookingDistance;
    private Cannon cannon;

    void Update()
    {
        float dist = Vector3.Distance(transform.position, target.position);
        Quaternion lookDirection = Quaternion.LookRotation(target.position - transform.position);
        Quaternion lookDirection2 = Quaternion.LookRotation(Vector3.right - transform.position);             
        if (dist < beginLookingDistance)
        {
            //lookDirection = Quaternion.Euler(0, lookDirection.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(lookDirection, transform.rotation, lookSpeed * Time.deltaTime);
           
        }
        else if (dist > beginLookingDistance)
        {
            lookDirection2 = Quaternion.Euler(lookDirection2.eulerAngles.x, 0, 0);
            transform.rotation = Quaternion.Slerp(lookDirection2, transform.rotation, lookSpeed * Time.deltaTime);
        }             
    }

    private void Awake()
    {
        cannon = GetComponent<Cannon>();
    }

    void OnEnable()
    {
        Manager.OnSendTransform += SetPlayerTransform;
    }

    void OnDisable()
    {
        Manager.OnSendTransform -= SetPlayerTransform;
    }

    public void SetPlayerTransform(Transform player)
    {
        target = player;
    }

    public Transform ReturnPlayerTransform()
    {
        return target;

    }
}
