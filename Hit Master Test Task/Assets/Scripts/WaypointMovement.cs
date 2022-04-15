using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class WaypointMovement : MonoBehaviour
{
    public LevelManager levelManager;

    private Animator characterAnimator;
    private Waypoint activeWaypoint;
    private NavMeshAgent agent;
    private int waypointNumber;

    public List<Waypoint> waypoints;

    private void Start()
    {
        characterAnimator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        activeWaypoint = waypoints[waypointNumber];
    }

    void Update()
    {
        if (!levelManager.isGameStarted) return;

        agent.SetDestination(activeWaypoint.waypointPosition.position);

        if (Vector3.Distance(transform.position, activeWaypoint.waypointPosition.position) < 1 && 
            activeWaypoint.enemies.Count == 0)
        {
            if (waypointNumber < waypoints.Count - 1)
            {
                waypointNumber++;
                activeWaypoint = waypoints[waypointNumber];
            }
            else levelManager.FinishGame();
        }

        characterAnimator.SetBool("Run", Vector3.Distance(transform.position, activeWaypoint.waypointPosition.position) < 1 ? false : true);
    }
}
