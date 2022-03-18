using UnityEngine;

public class timerTest : MonoBehaviour
{
    private float timer = 0.0f;
    public float waitTime = 30.0f;


    private void Start()
    {
        print(System.DateTime.Now.ToString("HH:mm:ss:f")); // Example of how to get a timestamp
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime; // Time.deltaTime = seconds between last frame and current frame
        print(timer);
        if (timer > waitTime) // Reset timer when it exceeds the wait time
            timer = 0.0f;
    }
}
