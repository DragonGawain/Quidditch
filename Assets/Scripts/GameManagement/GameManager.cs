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
    private TeamManager team1, team2;
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
        team1 = Team0.GetComponent<TeamManager>();
        team1.Name = "team1";
        team1.EnemyTeam = team2;
        team1.HasPlayer = true;
        team1.init();
        playerController.Team = team1;

        team2 = Team1.GetComponent<TeamManager>();
        team2.Name = "team2";
        team2.EnemyTeam = team1;
        team2.init();

    }

    public void playerRole(player.role pr)
    {
        team1.setPlayerRole(pr);
    }

    public void attachPlayerBody(GameObject player)
    {
        //set the camera and controller to control the player object
        Rigidbody rb = player.GetComponent<Rigidbody>();
        GameObject playerParent = GameObject.Find("PlayerParent");
        playerParent.GetComponent<PlayerMovementController>().Rb= rb;
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
