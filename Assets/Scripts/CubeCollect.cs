using UnityEngine;

public class CubeCollect : MonoBehaviour
{
    [SerializeField] private int AddAmount;
    [SerializeField] private AudioClip CoinCollectedSound;
    public delegate void Collected(int AddCoinAmount);
    public static event Collected OnCubeCollect;

    void Update()
    {
        transform.Rotate(0, 200 * Time.deltaTime, 0, Space.Self);
    }

    private void OnTriggerEnter()
    {
        OnCubeCollect?.Invoke(AddAmount);
        //EventManager.instance.cubeCollected(1);
        AudioManager.Instance.PlaySoundEffects(CoinCollectedSound);
        
        gameObject.SetActive(false);       
    }
}
