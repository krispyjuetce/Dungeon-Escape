using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new UnityException("Gamemanager doesnt exist");
            }
            else
            {
                return _instance;
            }
        }
    }

    public bool HasKeyToCastle { get; set; }
    public bool HasBootsOfFlight { get; set; }
    public bool HasFlameSword { get; set; }

    private void Awake()
    {
        _instance = this;
    }
}
