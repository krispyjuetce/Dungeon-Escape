using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour {

    private Player _player;
    private int _diamondValue=1;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_player != null)
        {
            _player.AddGems(_diamondValue);
            Destroy(gameObject);

        }
        
    }
}
