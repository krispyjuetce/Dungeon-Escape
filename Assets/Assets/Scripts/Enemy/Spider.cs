using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spider : Enemy, IDamageable
{
    [SerializeField] private float _spiderFieldOfView = 5.0f;
    [SerializeField] private float _acidSpeed = 2.0f;
    [SerializeField] private float _acidDestroyTimeInSeconds = 2.0f;
    public int Health { get; set; }
    [SerializeField] private GameObject _acidPrefab;

    public void Damage()
    {
        Health--;
        Debug.Log(Health);
        if (Health < 1)
        {
            Debug.Log("inside halth less than 1");
            //myAnimator.SetTrigger("DeathTrigger");
            //myCollider2D.enabled = false;
            Death();
        }
    }

    public override void Init()
    {
        base.Init();
        Health = base.health;
        idleAnimationTriggerString = "IdleTrigger";
        idleAnimationName = "Spider_Idle";
        attackAnimationName = "Spider_Attack";
        deathAnimationName = "Spider_Death";
    }

    public void Attack()
    {
        Debug.Log("Attack is called");
        GameObject acid = Instantiate(_acidPrefab, transform.position,Quaternion.identity);
        Rigidbody2D rb = acid.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(_acidSpeed* Mathf.Sign(transform.localScale.x), rb.velocity.y);
        Destroy(acid, _acidDestroyTimeInSeconds); //Destroy object after 5 seconds
    }

    public override void DistanceCalcualtionDuringCombat()
    {
        
        if (!myAnimator.GetBool("InCombat"))
        {
            
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance < _spiderFieldOfView&& !myAnimator.GetCurrentAnimatorStateInfo(0).IsName(deathAnimationName))
            {
                myAnimator.SetBool("InCombat", true);
            }
        }
        else
        {
            
            transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x - transform.position.x), 1f);
            distance = Vector3.Distance(transform.position, player.transform.position);
            if (distance > _spiderFieldOfView || myAnimator.GetCurrentAnimatorStateInfo(0).IsName(deathAnimationName))
            {
                myAnimator.SetBool("InCombat", false);
            }
        }
    }

}
