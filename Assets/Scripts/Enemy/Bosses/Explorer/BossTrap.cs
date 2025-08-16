using System.Collections;
using UnityEngine;
//Traps used in the second boss
//Needs to activate when the boss triggers them in his attack
public class BossTrap : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 3;

    [SerializeField]
    public FollowPath Path;

    [SerializeField]
    private TrapSounds _mySounds;

    [SerializeField]
    private SpriteRenderer _fanSprite;

    [SerializeField]
    private SpriteRenderer _dangerZoneSprite;

    [SerializeField]
    private bool _trapActive = false;

    [SerializeField]
    private float _timeBeforeTrapMoves = 2;

    //Draw danger zone and prepare attack
    public void PrepareAttack()
    {
        _dangerZoneSprite.enabled = true;
        StartCoroutine("StartMoving");
    }
    //Move to the other location
    private IEnumerator StartMoving()
    {
        yield return new WaitForSecondsRealtime(_timeBeforeTrapMoves);
        _trapActive = true;
    }
    //When the boss is dead, despawn the traps
    public void Despawn()
    {
        _dangerZoneSprite.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        _fanSprite.transform.Rotate(new Vector3(0, 0, _rotationSpeed));
        if (Path && _trapActive)
        {
            if (Path.BossMove())
            {
                _trapActive = false;
                _dangerZoneSprite.enabled = false;
            }
        }
    }
}
