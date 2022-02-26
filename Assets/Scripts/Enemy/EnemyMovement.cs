using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    int m_CurrentWaypointIndex;

    void Start (){
        navMeshAgent.SetDestination (waypoints[0].position);
 }

    void Update (){
        if(navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance){
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
			} }}

/* to get enemy movement working you need to ensure the level is marked as static so the NavMesh(has to be baked into the level)
that is set up knows where to allow the movement script to go. You then make empty gameobjects and
name them waypoint,waypoint(1),etc... then drag the waypoint game objects on to the waypoints array on the script 
the enemy also needs to be given a nav mesh agent
the script also has to be on the enemy
*/
