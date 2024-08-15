using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;

using player;

namespace player
{
    public enum role
    {
        CHASER,
        BEATER,
        KEEPER,
        SEEKER
    }

    public static class Vars
    {
        //speed for each role (for balancing)
        public static readonly float[] speed = {10.0f,  //chaser
                                                10.0f,  //beater
                                                10.0f,  //keeper
                                                10.0f}; //seeker

        //string representation of each role
        public static readonly string[] roleString ={"chaser",
                                                    "beater",
                                                    "keeper",
                                                    "seeker"
                                                    };
        //number of positions in that role
        public static readonly int[] roleNumber ={  3,
                                                    2,
                                                    1,
                                                    1};


    }


}
public class Player : MonoBehaviour
{
    /*can be AI or human player*/

    //Used by GroupAI
    protected bool inFormation;
    public bool InFormation
    {
        get { return inFormation; }
        set { inFormation = value; }
    }

    public bool isHuman;
    
    private Rigidbody body;
    private role role;
    public role Role { get { return role; } set { role = value; } }

    private TeamManager team;
    public TeamManager Team { get { return team; } }

    private bool hasQuaffle = false;
    public bool HasQuaffle
    {
        get { return hasQuaffle; }
    }

    void Awake()
    {

        body = GetComponent<Rigidbody>();
        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
       // currentMousePos = inputs.Player.MousePosition.ReadValue<Vector2>();
       // inputs.Player.Fire.performed += Fire;
    }

    // Start is called before the first frame update
    void Start()
    {
        team = GetComponentInParent<TeamManager>();
        if(team == null)
        {
            Debug.Log("ERROR - Player has null team");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public bool hasRole(player.role r)
    {
        return role == r;
    }
}
