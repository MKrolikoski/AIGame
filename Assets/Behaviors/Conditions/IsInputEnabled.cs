using UnityEngine;

using Pada1.BBCore;           // Code attributes
using Pada1.BBCore.Tasks;     // TaskStatus
using Pada1.BBCore.Framework; // ConditionBase


[Condition("Custom/Conditions/IsInputEnabled")]
[Help("Check whether inputs are enabled and player can make decisions")]
public class IsInputEnabled : ConditionBase
{

    public override bool Check()
    {
        //Debug.Log("Inputs enabled: " + InputController.instance.inputEnabled);


        return InputController.instance.inputEnabled;
    }

    //public override TaskStatus MonitorCompleteWhenTrue()
    //{
    //    if (Check())
    //        return TaskStatus.COMPLETED;
    //    else
    //    {
    //        if (InputController.instance != null)
    //        {
    //            InputController.instance.OnInputChange += OnInputChange;
    //        }
    //        Debug.Log("MonitorCompleteWhenTrue returning suspend");

    //        return TaskStatus.SUSPENDED;
    //    }
    //}



    //public override TaskStatus MonitorFailWhenFalse()
    //{
    //    if (!Check())
    //        return TaskStatus.FAILED;
    //    else
    //    {
    //        InputController.instance.OnInputChange += OnInputChange;
    //        Debug.Log("MonitorFailWhenFalse returning suspend");
    //        return TaskStatus.SUSPENDED;
    //    }
    //}

    //private void OnInputChange(bool inputEnabled)
    //{
    //    InputController.instance.OnInputChange -= OnInputChange;
    //    Debug.Log("Inside OnInputChange");
    //    if (inputEnabled)
    //    {
    //        Debug.Log("True");

    //        EndMonitorWithSuccess();
    //    }
    //    else
    //    {
    //        Debug.Log("False");

    //        EndMonitorWithFailure();
    //    }
    //}

    //public override void OnAbort()
    //{
    //    Debug.Log("Aborting");
    //    InputController.instance.OnInputChange -= OnInputChange;

    //    base.OnAbort();
    //}
}