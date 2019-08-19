using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy, IDamageable
{
    public int Health { get; set; }

    public override void Init()
    {
        base.Init();
        Health = base.health;
        idleAnimationTriggerString = "IdleTrigger";
        idleAnimationName = "Skeleton_Idle";
        takingDamageAnimationName = "Skeleton_Hit";
        attackAnimationName = "Skeleton_Attack";
        deathAnimationName = "Skeleton_Death";
    }

    public void Damage()
    {
        Health--;
        
        myAnimator.SetTrigger("TakingDamageTrigger");
        myAnimator.SetBool("InCombat", true);
        
        if (Health < 1)
        {
            Death();
        }
        
    }
}
