using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyThirdPersonController : ThirdPersonController
{
    [SerializeField]
    private float rotationSpeedToward = 50;
    
    private bool isMovingToward;
    private Quaternion targetRotation;
    private Vector3 targetDirection;
    private float targetSpeed;
    private Vector3 targetPos;


    public void StartMovingTowards(RaycastHit hitInfo)
    {

        isMovingToward = true;
        targetPos = hitInfo.point;
        RotateTowards(targetPos);
    }

    private void RotateTowards(Vector3 targetPos)
    {
        targetDirection = (targetPos - transform.position).normalized;
        targetDirection.y = 0;
        targetRotation = Quaternion.LookRotation(targetDirection);
        targetSpeed = _input.sprint ? SprintSpeed : MoveSpeed;
    }

    protected new void Update()
    {
        if(_input.move!= Vector2.zero || hasReachedTarget())
        {
            isMovingToward = false;
        }

        base.Update();

        if (isMovingToward)
        {
            MoveToward();
        }
    }

    private void MoveToward()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeedToward);
        Vector2 moveDirection = new Vector2(targetDirection.x, targetDirection.z);
        Move(moveDirection);
        _animator.SetFloat(_animIDSpeed, targetSpeed);
    }
    private bool hasReachedTarget()
    {
        Vector3 currPos = transform.position;
        currPos.y = 0;
        Vector3 tempTargetPos = targetPos;
        tempTargetPos.y = 0;
        return (currPos - tempTargetPos).magnitude < 0.2f;
    }
}
