
public class EnemyObjectPool : ObjectPool
{
    public static EnemyObjectPool SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }
}
