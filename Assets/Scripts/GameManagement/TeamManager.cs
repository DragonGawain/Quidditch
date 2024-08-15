using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using player;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;



public class TeamManager : MonoBehaviour
{    
    /*
     * Keep track of team info for group ai
     */

    [SerializeField] private GameObject[] goalposts;
    public GameObject[] Goalposts { get { return goalposts; } }
    public GameObject[] EnemyGoalposts { get { return EnemyTeam.Goalposts; } }

    [SerializeField] private Material beaterMat, chaserMat, keeperMat, seekerMat;
    [SerializeField] private GameObject beaterPrefab, chaserPrefab, keeperPrefab, seekerPrefab;
    [SerializeField] private GameObject[] chasers;
    public GameObject[] Chasers { get; }
    [SerializeField] private GameObject[] beaters;
    public GameObject[] Beaters { get; }
    [SerializeField] private GameObject keeper;
    public GameObject Keeper { get; }
    [SerializeField] private GameObject seeker;
    public GameObject Seeker { get; }

    private TeamManager enemyTeam;
    public TeamManager EnemyTeam { set { enemyTeam = value; } get { return enemyTeam; } }

    private bool hasPlayer = false;
    public bool HasPlayer { set { hasPlayer = value; } get { return hasPlayer; } }
    

    public void init()
    {
        //initialise the team members
        //add chasers from prefabs
        //for(int i = 0 ; i < Vars.roleNumber[(int)role.CHASER]; i++)
        //{
        //    Instantiate(chaserPrefab, new Vector3(0,0,0), q)
        //}

        recolour();
        GroupAI groupAI= gameObject.AddComponent<GroupAI>();

        //set as owner of goalposts
        foreach(GameObject goal in goalposts)
        {
            Goalpost gp = goal.GetComponent<Goalpost>();
            gp.Owner = this;
        }


    }

    public void recolour()
    {
        SkinnedMeshRenderer rend;
        //reload materials
        foreach (GameObject go in chasers)
        {
            if(chaserMat == null)
            {
                break;
            }
            rend = go.GetComponentInChildren<SkinnedMeshRenderer>();
            rend.material = chaserMat;
        }
        foreach(GameObject go in beaters)
        {
            if (beaterMat == null)
            {
                break;
            }
            rend = go.GetComponentInChildren<SkinnedMeshRenderer>();
            rend.material = beaterMat;

        }
        if(keeperMat !=null) {
            rend = keeper.GetComponentInChildren<SkinnedMeshRenderer>();
             rend.material = keeperMat; }
        if(seekerMat != null)
        {
            rend = seeker.GetComponentInChildren<SkinnedMeshRenderer>();
            rend.material = seekerMat;
        }

    }

    // Update is called once per frame
    void Update()
    {
    }
}
