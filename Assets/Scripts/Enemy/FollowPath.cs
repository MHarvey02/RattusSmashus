using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

public class FollowPath : MonoBehaviour
{

    [SerializeField]
    Transform Waypoints;

    [SerializeField]
    List<Transform> locations;

    [SerializeField]
    float MoveSpeed;

    Vector3 NextLoc;
    int nextIndex = 0;

    void Start()
    {
        GetLocations();
        GetNextLocation();
    }

    //Get the transforms of the waypoint the game object will move between
    private void GetLocations()
    {
        foreach (Transform child in Waypoints)
        {
            locations.Add(child);
        }
    }
    // Gets the location the game object needs to move towards
    private void GetNextLocation()
    {
        NextLoc = locations[nextIndex].transform.position;
        nextIndex++;

        if (nextIndex >= locations.Count)
        {
            nextIndex = 0;
        }

    }

    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, NextLoc, MoveSpeed * Time.deltaTime);
        //Check if the game object is near the current waypoint to get the next location if needed
        if (Mathf.Approximately(transform.position.x, NextLoc.x) && Mathf.Approximately(transform.position.y, NextLoc.y))
        {
            GetNextLocation();
        }
    }

    public bool BossMove()
    {
        transform.position = Vector2.MoveTowards(transform.position, NextLoc, MoveSpeed * Time.deltaTime);
        //Check if the game object is near the current waypoint to get the next location if needed
        if (Mathf.Approximately(transform.position.x, NextLoc.x) && Mathf.Approximately(transform.position.y, NextLoc.y))
        {
            GetNextLocation();
            return true;
        }
        return false;
    }
}
