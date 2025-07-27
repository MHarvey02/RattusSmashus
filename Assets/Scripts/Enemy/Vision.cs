using UnityEngine;

public class Vision : MonoBehaviour
{

    [SerializeField]
    private float _rayCastDistanceFloor;

    [SerializeField]
    private float _boxCastDistance;

    [SerializeField]
    public float direction;

    [SerializeField]
    private LayerMask _playerLayer;

    [SerializeField]
    private float _defaultBoxCastDistance;

    [SerializeField]
    private float _chaseBoxCastDistance;
    // draw box 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _boxCastDistance = _defaultBoxCastDistance;
    }
    //This will be used for the bosses
#nullable enable
    public GameObject? DrawBoxCast()
    {
        RaycastHit2D boxCast = Physics2D.BoxCast(transform.localPosition + new Vector3(_boxCastDistance * direction, 0, 0),
         new Vector2(5, 10),
         0,
         new Vector2(direction, 0),
         _boxCastDistance,
         _playerLayer);


        DebugBoxCast.SimpleDrawBoxCast(transform.localPosition + new Vector3(_boxCastDistance, 0, 0), new Vector2(5, 10), new Quaternion(0, 0, 0, 0), new Vector2(direction, 0), _boxCastDistance, Color.red);

        if (boxCast)
        {
            _boxCastDistance = _chaseBoxCastDistance;
            return boxCast.collider.gameObject;
        }
        _boxCastDistance = _defaultBoxCastDistance;
        return null;
    }


    //This will be used for the basic melee enemies
    public RaycastHit2D? DrawLineCast()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, _rayCastDistanceFloor, _playerLayer);
        Debug.DrawRay(transform.position, Vector2.down * _rayCastDistanceFloor, Color.green);
        if (hit)
        {
            return hit;
        }
        return null;
    }
    
#nullable disable

}
