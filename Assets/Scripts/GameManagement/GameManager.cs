using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // EVENTS
    public delegate void SnitchCaught(GroupAI characterResponsible);

    public delegate void GoalScored(TeamManager owningTeam, GroupAI characterResponsible);

    [SerializeField] private GameObject snitch, quaffle;
    public GameObject Snitch {  get { return snitch; } }
    public GameObject Quaffle { get {  return quaffle; } }
    [SerializeField] private GameObject[] bludgers;
    public GameObject[] Bludgers { get {  return bludgers; } }

    [SerializeField] GameObject Team0, Team1;
    private TeamManager team0, team1;
    private GroupAI groupAI0, groupAI1;

    [SerializeField] PlayerController playerController;

    // Start is called before the first frame update
    void Start() {

        initTeams();
        //initGroupAI();
    }

    // Update is called once per frame
    void Update() { }

    private void initTeams()
    {
        //Team0.AddComponent<TeamManager>();
        //Team1.AddComponent<TeamManager>(); 
        team0 = Team0.GetComponent<TeamManager>();
        team0.EnemyTeam = team1;
        team0.HasPlayer = true;
        team0.init();
        playerController.Team = team0;

        team1 = Team1.GetComponent<TeamManager>();
        team1.EnemyTeam = team0;
        team1.init();

    }

    /*
    private void initGroupAI()
    {
        groupAI0 = new GroupAI();
        groupAI0.Team = team0;
        groupAI0.init();
        groupAI1= new GroupAI();
        groupAI1.Team = team1;
        groupAI1.init();
    }
    */
}
