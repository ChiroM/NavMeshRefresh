using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaypointSpawnManager : MonoBehaviour
{
    private readonly Vector2 minmaxWaypoints = new Vector2(2, 5);
    private RaycastHit _hit;

    public List<WaypointObject> waypoints;

    public void Setup()
    {
        for (int i = 0; i < waypoints.Count; i++)
        {
            waypoints[i].Setup(i + 1);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && UnityHelper.GetRaycastHitFromCamera(out _hit, GlobalVariable.NavMeshClickTargetTag))
            AddWaypoint(_hit.point);
    }

    public void AddWaypoint(Vector3 point)
    {
        if (waypoints.Count == minmaxWaypoints.y)
            return;

        WaypointObject newWaypoint = UnityHelper.CloneObject(waypoints.Last());

        newWaypoint.transform.position = point;

        waypoints.Add(newWaypoint);

        newWaypoint.Setup(waypoints.Count);
    }

    public void RemoveWaypoint()
    {
        if (waypoints.Count == minmaxWaypoints.x)
            return;

        WaypointObject removedWaypoint = waypoints.Last();
        waypoints.Remove(removedWaypoint);

        DestroyImmediate(removedWaypoint.gameObject);
    }

    public WaypointObject GetWaypoint(int index) => waypoints.FirstOrDefault(a => a.Index == index);
}
