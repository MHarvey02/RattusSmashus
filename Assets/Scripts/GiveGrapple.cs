using System.Collections.Generic;
using Enemy.Boss;
using UnityEngine;
using UnityEngine.Events;

public class GiveGrapple : MonoBehaviour
{
    public UnityEvent spawnGrapplePoints;

    [SerializeField]
    private BossContext _myBoss;

    [SerializeField]
    private List<GrapplePoint> _grapplePoints;

    public void Start()
    {
        gameObject.SetActive(false);

        for (int i = 0; i < _grapplePoints.Count; i++)
        {
            spawnGrapplePoints.AddListener(_grapplePoints[i].SetAwake);
            spawnGrapplePoints.AddListener(_myBoss.Despawn);
        }

    }

    public void Spawn()
    {
        gameObject.SetActive(true);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spawnGrapplePoints.Invoke();
            gameObject.SetActive(false);
        }
    }
}
