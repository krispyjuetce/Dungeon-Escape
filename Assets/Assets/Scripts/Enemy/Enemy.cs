using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour {

    //configuration variables
    [SerializeField]
    protected int health;
    [SerializeField]
    protected float speed;
    [SerializeField]
    protected int gems;
    [SerializeField]
    protected Transform[] wayPoints;
    [SerializeField]
    protected GameObject diamondPrefab;

    protected Vector3 currentTarget;
    protected Animator myAnimator;

    protected string idleAnimationTriggerString;
    protected string idleAnimationName;
    protected string takingDamageAnimationName;
    protected string attackAnimationName;
    protected string deathAnimationName;
    protected Player player;

    protected float distance;
    private Animator _playerAnimator;
    protected BoxCollider2D myCollider2D;

    private Vector2 _diamondSpawnPosition;
    private Vector2 _offset;
    [SerializeField]
    protected float diamondSpawnOffsetX = 2f;

    public virtual void Init()
    {
        myAnimator = GetComponentInChildren<Animator>();
        currentTarget = wayPoints[1].position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _playerAnimator = GetComponentInChildren<Animator>();
        myCollider2D = GetComponent<BoxCollider2D>();
    }

    // Use this for initialization using Init
    public void Start()
    {
        Init();
    }

    public virtual void Update()
    {
        DistanceCalcualtionDuringCombat();
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName(idleAnimationName) ||
            myAnimator.GetCurrentAnimatorStateInfo(0).IsName(takingDamageAnimationName) ||
            myAnimator.GetCurrentAnimatorStateInfo(0).IsName(attackAnimationName)||
            myAnimator.GetCurrentAnimatorStateInfo(0).IsName(deathAnimationName)) { return; }
        Movement();
    }

    public virtual void DistanceCalcualtionDuringCombat()
    {
        if (myAnimator.GetBool("InCombat"))
        {
            transform.localScale = new Vector2(Mathf.Sign(player.transform.position.x- transform.position.x), 1f);
            distance = Vector3.Distance(transform.position, player.transform.position);
            
            if (distance > 2.0f||player.Health==0)
            {
                myAnimator.SetBool("InCombat", false);
            }
        }
        
    }

    public virtual void Movement()
    {
        if (transform.position == wayPoints[0].position)
        {
            currentTarget = wayPoints[1].position;
            myAnimator.SetTrigger(idleAnimationTriggerString);
            transform.localScale = new Vector2(Mathf.Sign(currentTarget.x - transform.position.x), 1f);


        }
        else if (transform.position == wayPoints[1].position)
        {
            currentTarget = wayPoints[0].position;
            myAnimator.SetTrigger(idleAnimationTriggerString);
            transform.localScale = new Vector2(Mathf.Sign(currentTarget.x - transform.position.x), 1f);

        }
        //pull out the tranform .localscale code here sa an if . logic is if they are not in the same direction, flip
        
        if ((Mathf.Abs(Mathf.Sign(transform.localScale.x)-Mathf.Sign(currentTarget.x - transform.position.x)) < Mathf.Epsilon)!=true)
        {
            //if due to attack the skeleton is facing the opposite direction of the target waypoint, flip its direction
            transform.localScale = new Vector2(transform.localScale.x * -1,1f);


        }
        transform.position = Vector3.MoveTowards(transform.position, currentTarget, speed * Time.deltaTime);
   
    }

    public virtual void Death()
    {
        
        myAnimator.SetTrigger("DeathTrigger");
        myAnimator.SetBool("InCombat", false);
        myCollider2D.enabled = false;
        
        _diamondSpawnPosition = transform.position;
        _offset = new Vector2(diamondSpawnOffsetX, 0f);
        //instantiate diamond prefab
        for (int i = 0; i < gems; i++)
        {
            GameObject diamond = Instantiate(diamondPrefab, _diamondSpawnPosition, Quaternion.identity);
            _diamondSpawnPosition = _diamondSpawnPosition + _offset;

        }
    }

    
}
