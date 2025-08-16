//Version of the object pool for the enemies
public class EnemyObjectPool : ObjectPool
{
    public static EnemyObjectPool SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }
}
