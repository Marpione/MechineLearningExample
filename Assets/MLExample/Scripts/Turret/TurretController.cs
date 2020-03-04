using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;

public class TurretController : MonoBehaviour
{
    public Transform PartToRotate;

    public float RotateSpeed;
    public float FireRate;
    public float FireRange;
    Vector3 velocity = Vector3.zero;

    List<Agent> agents;
    Agent currentAgent;

    private void OnEnable()
    {
        agents = new List<Agent>(FindObjectsOfType<Agent>());
    }

    float closestDistance;

    private void Update()
    {
        for (int i = 0; i < agents.Count; i++)
        {
            float distanceToAgent = Vector3.Distance(agents[i].transform.position, transform.position);

            if (distanceToAgent > FireRange)
                return;

            if (closestDistance == 0)
            {
                closestDistance = distanceToAgent;
                currentAgent = agents[i];
            }

            if (distanceToAgent < closestDistance)
            {
                closestDistance = distanceToAgent;
                currentAgent = agents[i];
            }
        }

        if(Vector2.Distance(currentAgent.transform.position, transform.position) > FireRange)
        {
            currentAgent = null;
        }

        if(currentAgent != null)
        {
            var dir = transform.position - currentAgent.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = lookRotation.eulerAngles;
            PartToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            currentAgent.AddReward(-5f / currentAgent.maxStep);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, FireRange);
    }
}
