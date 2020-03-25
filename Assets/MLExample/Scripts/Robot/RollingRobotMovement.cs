using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingRobotMovement : MonoBehaviour
{
    public float MoveSpeed;

    private Animator animator;
    public Animator Animator { get { return (animator == null) ? animator = GetComponentInChildren<Animator>() : animator; } }

    private Rigidbody rigidbody;
    private Rigidbody Rigidbody { get { return (rigidbody == null) ? rigidbody = GetComponent<Rigidbody>() : rigidbody; } }


    [HideInInspector]
    public bool canMove;

    private void Start()
    {
        Invoke("SetCanMove", 3.2f);
    }

    void SetCanMove()
    {
        canMove = true;
    }

    public void MoveRobot(float value)
    {
        if (!canMove)
            return;

        Rigidbody.velocity = transform.forward * MoveSpeed * value;
        Animator.SetFloat("VerticalSpeed", value);
    }

    public void TurnRobot(float value)
    {
        if (!canMove)
            return;

        transform.Rotate(Vector3.up * 5 * value);
        Animator.SetFloat("HorizantolSpeed", value);
    }
}
