using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PositionTrackingAgentManager : MonoBehaviour
{
    public ThirdPersonPlayerController playerController;
    public List<NavMeshAgent> navMeshAgents;

    private void Awake()
    {
        playerController.OnPlayer_Moved += PlayerController_OnPlayer_Moved;
    }

    private void PlayerController_OnPlayer_Moved(ThirdPersonPlayerController playerController)
    {
        for (int i = 0; i < navMeshAgents.Count; i++)
        {
            navMeshAgents[i].SetDestination(playerController.transform.position);
        }
    }
}
