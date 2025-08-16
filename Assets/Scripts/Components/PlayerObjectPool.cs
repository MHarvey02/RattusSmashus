//Version of the object pool for the player
public class PlayerObjectPool : ObjectPool
{
    public static PlayerObjectPool SharedInstance;

    void Awake()
    {
        SharedInstance = this;
    }
}
