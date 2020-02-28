using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using TMPro;

public class BaseArea : MonoBehaviour
{
    public RollingRobotAgent RollingRobotAgent;
    public TextMeshProUGUI CummilativeReward;

    public Transform CenterSpawnPoint;
    public float MinAngle = 0f;
    public float MaxAngle = 360f;
    public float MinRadius = 1f;
    public float MaxRadius = 10;


    public void ResetArea()
    {
        PlaceRobot();
    }

    private void PlaceRobot()
    {
        RollingRobotAgent.transform.position = AreaUtilities.GiveRandomSpawnPosition(CenterSpawnPoint.position, MinAngle, MaxAngle, MinRadius, MaxRadius)+ Vector3.up * 0.5f;
        RollingRobotAgent.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0f);
    }
}
