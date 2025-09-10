using DinoFracture;
using System;
using TMPro;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Manager manager;
    public static event Action <int, Transform> OnCheckPointTriggered;
    [SerializeField] private GameObject checkPointCube;
    [SerializeField] private Transform checkPointLocation;
    [SerializeField] private int checkPointCost;
    private TextMeshPro checkPointDisplay;
    private float x;
    private float y;
    private float z;
    public float xHeight;
    public float yHeight;
    public float zHeight;
    public float xSpeed;
    public float ySpeed;
    public float zSpeed;
    public float Hight;
    private bool checkpointActivated;

    private void Start()
    {
         checkPointDisplay = gameObject.transform.GetChild(1).GetComponent<TextMeshPro>();
        // checkPointDisplay.text = "CheckPoint = " + checkPointCost.ToString() + "Coins";
    }

    void Update()
    {
        x = Mathf.Sin(Time.time * xSpeed) * Hight;
        y = Mathf.Sin(Time.time * ySpeed) * Hight;
        z = Mathf.Sin(Time.time * zSpeed) * Hight;
        
        transform.position = new Vector3(x + xHeight,y + yHeight,z + zHeight);    
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            if (checkpointActivated)
            {
                AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.CheckPointRespawn);

            }
            else
            {
                if (manager.GetCoinAmount() >= checkPointCost )
                {
                    checkpointActivated = true;
                    checkPointDisplay.color = Color.green;
                    OnCheckPointTriggered(checkPointCost, gameObject.transform);
                    checkPointCube.GetComponent<FractureGeometry>().Fracture();

                    AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.CheckPointGood);
                }
                else
                {
                    AudioManager.Instance.PlaySoundEffects(AudioManager.Instance.audioClips.CheckPointBad);
                }
            }           
        }
    } 
}
