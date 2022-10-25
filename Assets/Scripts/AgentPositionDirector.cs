using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentPositionDirector : MonoBehaviour
{
    public List<NavMeshAgent> agents;
    public GameObject pointMarker;

    private RaycastHit _hit;

    private void Update()
    {
        HandleClick();
    }

    private void HandleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UnityHelper.GetRaycastHitFromCamera(out _hit, GlobalVariable.NavMeshClickTargetTag))
                SendAgentsToPosition(_hit.point);

        }
    }

    public void SendAgentsToPosition(Vector3 position)
    {
        for (int i = 0; i < agents.Count; i++)
        {
            agents[i].SetDestination(position);
        }

        pointMarker.transform.position = position;
    }
}
