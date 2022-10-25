using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class WaypointPatrolManager : MonoBehaviour
{
    public List<PatrolingNavMeshAgent> navMeshAgents;

    public WaypointSpawnManager waypointSpawnManager;

    public List<WaypointObject> Waypoints => waypointSpawnManager.waypoints;

    public Toggle loopToggle;

    private bool IsLooping => loopToggle.isOn;

    private void Awake()
    {
        waypointSpawnManager.Setup();

        foreach (var navMeshAgent in navMeshAgents)
        {
            navMeshAgent.OnWaypoint_Reached += NavMeshAgent_OnWaypoint_Reached;
            navMeshAgent.GoToWaypoint(waypointSpawnManager.waypoints[0]);
        }
    }

    private void NavMeshAgent_OnWaypoint_Reached(PatrolingNavMeshAgent patrolingNavMeshAgent)
    {
        if (IsLooping)
            SetNextWaypoint_Looping(patrolingNavMeshAgent);
        else
            SetNextWaypoint_PingPong(patrolingNavMeshAgent);
    }

    private void SetNextWaypoint_Looping(PatrolingNavMeshAgent patrolingNavMeshAgent)
    {
        if (Waypoints.Count <= patrolingNavMeshAgent.TargetWaypointIndex)
            patrolingNavMeshAgent.GoToWaypoint(waypointSpawnManager.GetWaypoint(1));
        else
            patrolingNavMeshAgent.GoToWaypoint(waypointSpawnManager.GetWaypoint(patrolingNavMeshAgent.TargetWaypointIndex + 1));
    }

    private void SetNextWaypoint_PingPong(PatrolingNavMeshAgent patrolingNavMeshAgent)
    {
        if (patrolingNavMeshAgent.PathDirection == WaypointPathDirection.Forward)
        {
            if (patrolingNavMeshAgent.TargetWaypointIndex >= Waypoints.Count)
                patrolingNavMeshAgent.GoToWaypoint(waypointSpawnManager.GetWaypoint(Waypoints.Count - 1), WaypointPathDirection.Backward);
            else
                patrolingNavMeshAgent.GoToWaypoint(waypointSpawnManager.GetWaypoint(patrolingNavMeshAgent.TargetWaypointIndex + 1));
        }
        else
        {
            if (patrolingNavMeshAgent.TargetWaypointIndex == 1)
                patrolingNavMeshAgent.GoToWaypoint(waypointSpawnManager.GetWaypoint(patrolingNavMeshAgent.TargetWaypointIndex + 1));
            else
                patrolingNavMeshAgent.GoToWaypoint(waypointSpawnManager.GetWaypoint(patrolingNavMeshAgent.TargetWaypointIndex - 1), WaypointPathDirection.Backward);
        }
    }
}
