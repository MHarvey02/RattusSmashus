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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetLocations();
        GetNextLocation();
    }

    private void GetLocations()
    {
        foreach (Transform child in Waypoints)
        {
            locations.Add(child);
        }
    }

    private void GetNextLocation()
    {
        NextLoc = locations[nextIndex].transform.position;
        nextIndex++;

        if (nextIndex >= locations.Count)
        {
            nextIndex = 0;
        }

    }
    // Update is called once per frame
    public void Move()
    {
        transform.position = Vector2.MoveTowards(transform.position, NextLoc, MoveSpeed * Time.deltaTime);

        if (Mathf.Approximately(transform.position.x,NextLoc.x) && Mathf.Approximately(transform.position.y, NextLoc.y))
        {
            GetNextLocation();
        }
    }
}
