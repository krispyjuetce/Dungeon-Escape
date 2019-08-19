using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour {

    //cached components
    private Animator _myAnimator;
    private Animator _SwordAnimator;
	// Use this for initialization
	void Start () {

        _myAnimator = GetComponentInChildren<Animator>();
        _SwordAnimator = transform.GetChild(1).GetComponent<Animator>();
    }
	
	public void PlayRunAnimation(float playerVelocity)
    {

        bool playerIsRunning = Mathf.Abs(playerVelocity) > Mathf.Epsilon;
        if (playerIsRunning)    //flips the sprite in the right direction
        {
            transform.localScale = new Vector2(Mathf.Sign(playerVelocity), 1f);
        }
        _myAnimator.SetBool("IsRunning", playerIsRunning);
    }

    public void PlayJumpAnimation(bool isJumping)
    {
        _myAnimator.SetBool("IsJumping", isJumping);
    }

    public void PlayAttackAnimation()
    {
        _myAnimator.SetTrigger("AttackTrigger");
        _SwordAnimator.SetTrigger("SwordAnimationTrigger");
    }

    public void PlayHitAnimation()
    {
        _myAnimator.SetTrigger("TakingDamageTrigger");
    }

    public void PlayDeathAnimation()
    {
        Debug.Log("inside set death trigger");
        _myAnimator.SetTrigger("DeathTrigger");
    }
}
