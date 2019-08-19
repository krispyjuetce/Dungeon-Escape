using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mossgiant : Enemy, IDamageable
{
    public int Health { get; set; }
    
    public override void Init()
    {
        base.Init();
        Health = base.health;
        idleAnimationTriggerString = "IdleTrigger";
        idleAnimationName = "Moss_Giant_idle";
        takingDamageAnimationName = "Moss_Giant_Hit";
        attackAnimationName = "Moss_Giant_Attack";
        deathAnimationName = "Moss_Giant_Death";

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
