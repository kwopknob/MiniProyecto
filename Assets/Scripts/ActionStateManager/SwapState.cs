using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwapState : ActionBaseState
{
    public override void EnterState(ActionStateManager action)
    {
        action.anim.SetTrigger("SwapWeapon");
        action.lHandK.weight = 0;
        action.rHandAim.weight = 0;
    }

    public override void UpdateState(ActionStateManager action)
    {
        
    }
}
