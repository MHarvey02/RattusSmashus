using UnityEngine;

public class Trap : MonoBehaviour
{

    [SerializeField]
    private float _rotationSpeed = 3;

    [SerializeField]
    public FollowPath Path;
    
    [SerializeField]
    private bool _bossLevelMove = false;

    [SerializeField]
    private Transform _nextBossLoc;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Awake()
    {

    }

  public void BossMove(Transform nextLoc)
  {
    _nextBossLoc.position = nextLoc.position;
    _bossLevelMove = true;
  }



  // Update is called once per frame
  void Update()
  {
    gameObject.transform.Rotate(new Vector3(0, 0, _rotationSpeed));
    if (Path)
    {
      Path.Move();
    }

    if (_bossLevelMove)
    {
      transform.position = Vector2.MoveTowards(transform.position, _nextBossLoc.position, 5 * Time.deltaTime);    
    }
        
        

    }
}
