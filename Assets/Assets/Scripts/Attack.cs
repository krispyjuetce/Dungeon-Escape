using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        

        IDamageable hit = collision.GetComponent<IDamageable>();

        if (hit!=null)
        {
            Debug.Log("Player has hit : " + collision.name);
            hit.Damage();
        }
        Debug.Log("The Tag is: " + transform.tag);
        if (transform.tag== "Bullet")
        {
            Debug.Log("Destroy");
            Destroy(gameObject, .1f);
        }
        
    }

}
