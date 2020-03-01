using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class PenaltryArea : MonoBehaviour
{
    Agent Agent;

    public float EnterReward;
    public float Punshment;
    public float ExitReward;

    private void OnTriggerEnter(Collider other)
    {
        Agent = other.GetComponent<Agent>();

        if (Agent != null)
        {
            Agent.AddReward(EnterReward);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(Agent != null)
        {
            Agent.AddReward(Punshment / Agent.maxStep);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(Agent != null)
        {
            Agent.AddReward(ExitReward);
        }
    }

}
