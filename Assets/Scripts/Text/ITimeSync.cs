public interface ITimeSync
{
    void Sync();
    void Reset();
    bool CanSync();
    float TimeSyncFinal();
    void Reload();
}