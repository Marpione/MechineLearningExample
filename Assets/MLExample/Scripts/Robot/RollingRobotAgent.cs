using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MLAgents;
using MLAgents.Sensor;

public class RollingRobotAgent : Agent
{
    public Goal Goal;

    //private BaseArea currentArea;
    public BaseArea CurrentArea;// { get { return (currentArea == null) ? transform.root.GetComponent<BaseArea>() : currentArea; } }



    private RayPerceptionSensorComponent3D rayPerception;
    public RayPerceptionSensorComponent3D RayPerception3D { get { return (rayPerception == null) ? rayPerception = GetComponent<RayPerceptionSensorComponent3D>() : rayPerception; } }

    private RollingRobotMovement rollingRobotMovement;
    private RollingRobotMovement RollingRobotMovement { get { return (rollingRobotMovement == null) ? rollingRobotMovement = GetComponent<RollingRobotMovement>() : rollingRobotMovement; } }


    public override void AgentAction(float[] vectorAction)
    {

        //if (!RollingRobotMovement.canMove)
        //    return;

        base.AgentAction(vectorAction);
        var dirGo = 0f;
        var rotateDir = 0f;

        var action = Mathf.FloorToInt(vectorAction[0]);

        switch(action)
        {
            case 1:
                dirGo = 1f;
                break;
            //case 2:
            //    dirGo = -1f;
            //    break;

            case 3:
                rotateDir = 1f;
                break;

            case 4:
                rotateDir = -1f;
                break;
        }

        RollingRobotMovement.MoveRobot(dirGo);
        RollingRobotMovement.TurnRobot(rotateDir);

        //Tiny punishment for not moving
        AddReward(-1f / maxStep);
    }

    public override float[] Heuristic()
    {
        //if (Input.GetKey(KeyCode.D))
        //{
        //    return new float[] { 3 };
        //}
        if (Input.GetKey(KeyCode.W))
        {
            return new float[] { 1 };
        }
        //if (Input.GetKey(KeyCode.A))
        //{
        //    return new float[] { 4 };
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    return new float[] { 2 };
        //}

        return new float[] { 0 };
    }

    public override void AgentReset()
    {
        base.AgentReset();
        CurrentArea.ResetArea();
        CurrentArea.GoalRedius = 6f;
        AddReward(-1);
    }


    public override void CollectObservations()
    {
        //AddVectorObs(1);
        //DistanceToGoal
        AddVectorObs(Vector3.Distance(Goal.transform.position, transform.position));

        //DirectionToGoal
        AddVectorObs((Goal.transform.position - transform.position).normalized);


        //Direction Agent is faceging
        AddVectorObs(transform.forward);

        //Ray Perception (sight)
        //========================
        //Ray Distance: Max Distance a ray can reach
        //Ray Angles:  Angles to Raycast (0 is right, 90 is forward, 180 is left)
        //Detectable Objects: List of tags wicht corresponds to object types agent can see
        //StartOffset: Starting Height offset of ray from center of agent
        //End Offset: End Height offset of ray from center of agent

        //float rayDistance = 20f;
        //float[] rayAngles = { 30f, 60f, 90, 120f, 150f };
        //string[] detectableObjects = { "Obstacle", "Goal"};
        //AddVectorObs(RayPerceptionSensor.PerceiveStatic(rayDistance, rayAngles, detectableObjects, 0f, 0f, 0f, transform, RayPerceptionSensor.CastType.Cast3D, ));
    }

    private void Start()
    {
        Goal = CurrentArea.Goal;
    }

    private void FixedUpdate()
    {
        if(Vector3.Distance(transform.position, Goal.transform.position)< CurrentArea.GoalRedius)
        {
            Debug.Log("Goal Reached");
            CurrentArea.GoalRedius = CurrentArea.GoalRedius * 0.95f;
            CurrentArea.ResetArea();
            AddReward(1f);
        }
    }
    public float reward;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("CheckPoint"))
        {
            other.GetComponent<BoxCollider>().enabled = false;
            AddReward(1f / maxStep);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Obstacle"))
        {
            AddReward(-1f);
        }
        else if (collision.transform.CompareTag("Goal"))
        {
            AddReward(1f);
        }

        if (collision.transform.CompareTag("Border"))
        {
            AddReward(-1f);
            CurrentArea.ResetArea();
        }

    }
}