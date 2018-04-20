﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "PluggableAI/Actions/Run")]
public class RunAction : Action
{
    public override void Act(StateController controller)
    {
        Run(controller);
    }

    private void Run(StateController controller)
    {
        float endAccel;

        controller.stats.maxSpeed *= controller.stats.speedModifier;

        if (controller.rb2d.velocity.x < controller.stats.maxSpeed)
        {
            endAccel = controller.stats.accel;
        }
        else
        {
            endAccel = 0;
        }

        if (controller.rb2d.velocity.y < controller.stats.maxSpeed)
        {
            endAccel = controller.stats.accel;
        }
        else
        {
            endAccel = 0;
        }

        Vector3 moveDir = (controller.transform.position - controller.chaseTarget.position).normalized;

        controller.rb2d.MovePosition(controller.transform.position + moveDir * (endAccel * Time.fixedDeltaTime));

        Vector3 dir = controller.chaseTarget.position - controller.transform.position;
        //Rotate the rigidbody2d smoothly 
        controller.rb2d.MoveRotation(Mathf.LerpAngle(controller.rb2d.rotation, Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg, .1f));
    }
}