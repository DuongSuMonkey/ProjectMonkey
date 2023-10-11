public interface ITimer
{
    void Sync();
    void ResetTime();
    void UpdateTimeSync();
    bool CanSync();
    void IncreateIndex();
    float TimeSyncFinal();
    void Reload();
}