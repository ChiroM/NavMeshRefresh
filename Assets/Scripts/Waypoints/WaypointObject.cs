using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaypointObject : MonoBehaviour
{
    public delegate void WaypointObjectEventHandler(PatrolingNavMeshAgent patrolingNavMeshAgent, WaypointObject waypointObject);
    public event WaypointObjectEventHandler OnPatrolingNavMeshAgent_Detected;

    public TextMeshPro text;
    public int Index { get; private set; }

    public void Setup(int waypointNumber)
    {
        Index = waypointNumber;

        text.text = waypointNumber.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        PatrolingNavMeshAgent navMeshAgent = other.gameObject.GetComponent<PatrolingNavMeshAgent>();

        if (navMeshAgent != null)
            OnPatrolingNavMeshAgent_Detected?.Invoke(navMeshAgent, this);
    }
}
