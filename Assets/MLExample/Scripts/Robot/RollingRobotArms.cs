using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRobotArms : MonoBehaviour
{
    RollingRobotAgent robotAgent;
    RollingRobotAgent RollingRobotAgent { get { return (robotAgent == null) ? robotAgent = transform.root.GetComponentInChildren<RollingRobotAgent>() : robotAgent; } }

    float LastTimeFeetOnTheGround;

    float MaxTimeFeetCanStayOnAir = 0.4f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Ground"))
        {
            //if(LastTimeFeetOnTheGround > Time.time + MaxTimeFeetCanStayOnAir)
            //    RollingRobotAgent.AddReward(-0.1f);

            LastTimeFeetOnTheGround = Time.time;
            RollingRobotAgent.AddReward(1f);
        }
    }

    //private void Update()
    //{
    //    if (LastTimeFeetOnTheGround > Time.time + MaxTimeFeetCanStayOnAir)
    //        RollingRobotAgent.AddReward(-0.1f);
    //}
}
