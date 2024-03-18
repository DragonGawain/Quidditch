using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goalpost : MonoBehaviour
{
    // TODO: add colliders for the various scoring zones, handle collision detection.
    // TODO: add waypoints that the keeper and chasers will use.

    // VARIABLES
    [SerializeField] private Team owningTeam = Team.NONE;
    public Team OwningTeam { get { return owningTeam; } }

    // METHODS
}
