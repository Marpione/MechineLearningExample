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

    public Goal Goal;

    private List<BoxCollider> CheckPointColliders = new List<BoxCollider>();

    [HideInInspector]
    public float ObstaclePersantage;

    //[HideInInspector]
    public float GoalRedius;

    private void Awake()
    {
        Monitor.SetActive(true);
    }

    private void Start()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("CheckPoint");

        for (int i = 0; i < objs.Length; i++)
        {
            CheckPointColliders.Add(objs[i].GetComponent<BoxCollider>());
        }
    }

    private void Update()
    {
        CummilativeReward.SetText(RollingRobotAgent.GetCumulativeReward().ToString("0.00"));
    }


    public void ResetArea()
    {
        PlaceRobot();
        ResetCheckPoints();
        //GoalRedius = 6;
    }

    void ResetCheckPoints()
    {
        for (int i = 0; i < CheckPointColliders.Count; i++)
        {
            CheckPointColliders[i].enabled = true;
        }
    }

    protected void PlaceRobot()
    {
        RollingRobotAgent.transform.position = AreaUtilities.GiveRandomSpawnPosition(CenterSpawnPoint.position, MinAngle, MaxAngle, MinRadius, MaxRadius)+ Vector3.up * 0.5f;
        //RollingRobotAgent.transform.rotation = Quaternion.Euler(0, Random.Range(0f, 360f), 0f);
        RollingRobotAgent.transform.LookAt(Goal.transform);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(CenterSpawnPoint.position, MinRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(CenterSpawnPoint.position, MaxRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(Goal.transform.position, GoalRedius);
    }
}
