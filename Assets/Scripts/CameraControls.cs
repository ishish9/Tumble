using UnityEngine;

public class CameraControls : MonoBehaviour
{
    [SerializeField] private Transform ObjectToFollow;
    [SerializeField] private float smoothSpeed;
    [SerializeField] private Vector3 offset;

    void Update()
    {
        Vector3 desiredp = ObjectToFollow.position + offset;
        Vector3 smoothedp = Vector3.Lerp(transform.position, desiredp, smoothSpeed * Time.deltaTime);
        transform.position = smoothedp;
    }

    private void OnEnable()
    {
        Manager.OnSendPlayerTransform += UpdateCameraFollow;
    }

    private void OnDisable()
    {
        Manager.OnSendPlayerTransform -= UpdateCameraFollow;
    }

    public void SmoothSpeedStart()
    {
        smoothSpeed = 3;
    }

    public void UpdateCameraFollow(Transform newObjectTransform)
    {
        ObjectToFollow = newObjectTransform;
    }

}