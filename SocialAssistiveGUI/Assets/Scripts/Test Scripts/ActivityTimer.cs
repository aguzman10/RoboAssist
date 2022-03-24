// ActivityTimer for determining when to enter Not Playing state
public class ActivityTimer
{
    private bool _isRunning = false;
    public float elapsedTime = 0.0f;

    public void ResetTimer()
    {
        elapsedTime = 0.0f;
    }

    public void StartTimer()
    {
        _isRunning = true;
    }

    public void StopTimer()
    {
        _isRunning = false;
    }

    public bool IsRunning() { return _isRunning; }
    
}
