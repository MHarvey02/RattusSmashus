
public class PlayerObjectPool : ObjectPool
{
    public static PlayerObjectPool SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }
}
