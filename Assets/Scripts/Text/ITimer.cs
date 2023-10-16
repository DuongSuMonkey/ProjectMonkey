public interface ITimer
{
    void Sync();
    void Reset();
    bool CanSync();
    float TimeSyncFinal();
    void Reload();
}