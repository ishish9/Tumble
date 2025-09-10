using TMPro;
using UnityEngine;
using UnityEngine.UI; // Required for UI elements like Text

public class FPSDisplay : MonoBehaviour
{
    private TextMeshProUGUI fpsText; // Assign a UI Text element in the Inspector
    public float pollingTime = 0.5f; // How often to update the FPS display

    private float time;
    private int frameCount;

    private void Start()
    {
        fpsText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        // Increment frame count and elapsed time
        frameCount++;
        time += Time.unscaledDeltaTime;

        // Update FPS display at specified intervals
        if (time >= pollingTime)
        {
            int fps = Mathf.RoundToInt(frameCount / time);
            fpsText.text = fps.ToString();

            // Reset for next calculation
            time -= pollingTime;
            frameCount = 0;
        }
    }
}
