using UnityEngine;

public class Vision : MonoBehaviour
{

    [SerializeField]
    private float _rayCastDistanceFloor;

    [SerializeField]
    private LayerMask _playerLayer;
    // draw box 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    //This will be used for the bosses
    public RaycastHit2D? DrawBoxCast()
    {
        RaycastHit2D bocCast = Physics2D.BoxCast(transform.localPosition, new Vector2(10, 10), 0, new Vector2(1, 0));

        return null;
    }

    //This will be used for the basic melee enemies
    public RaycastHit2D? DrawLineCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _rayCastDistanceFloor, _playerLayer);
        Debug.DrawRay(transform.position, Vector2.down * _rayCastDistanceFloor, Color.green );
        if (hit)
        {
            return hit;
        }
        return null;
    }

}
