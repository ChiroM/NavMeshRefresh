using UnityEngine;
using UnityEngine.AI;

public class PatrolingNavMeshAgent : MonoBehaviour
{
    public delegate void PatrolingNavMeshAgentEventHandler(PatrolingNavMeshAgent patrolingNavMeshAgent);
    public event PatrolingNavMeshAgentEventHandler OnWaypoint_Reached;

    public NavMeshAgent navMeshAgent;
    private WaypointObject _waypointObject;
    private Vector3 _lastWaypointPosition;

    public WaypointPathDirection PathDirection { get; private set; }
    public int TargetWaypointIndex { get; private set; }

    private void Update()
    {
        if (_waypointObject == null)
            OnWaypoint_Reached?.Invoke(this);

        if (_lastWaypointPosition != _waypointObject.transform.position)
            navMeshAgent.SetDestination(_waypointObject.transform.position);
    }

    public void GoToWaypoint(WaypointObject waypointObject, WaypointPathDirection pathDirection = WaypointPathDirection.Forward)
    {
        TargetWaypointIndex = waypointObject.Index;
        PathDirection = pathDirection;

        _waypointObject = waypointObject;
        _lastWaypointPosition = _waypointObject.transform.position;

        navMeshAgent.SetDestination(waypointObject.transform.position);

        waypointObject.OnPatrolingNavMeshAgent_Detected += WaypointObject_OnPatrolingNavMeshAgent_Detected;
    }

    private void WaypointObject_OnPatrolingNavMeshAgent_Detected(PatrolingNavMeshAgent patrolingNavMeshAgent, WaypointObject waypointObject)
    {
        if (patrolingNavMeshAgent.gameObject.GetInstanceID() != gameObject.GetInstanceID())
            return;

        waypointObject.OnPatrolingNavMeshAgent_Detected -= WaypointObject_OnPatrolingNavMeshAgent_Detected;

        OnWaypoint_Reached?.Invoke(this);
    }
}

public enum WaypointPathDirection
{
    None = 0,
    Forward,
    Backward
}
