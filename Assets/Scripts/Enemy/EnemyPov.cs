using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPov : MonoBehaviour
{
    public Transform player;
    PlayerController playerControl;
    public Gate gameEnder;

    bool m_IsPlayerInRange = false;


		// if it collides with  the player object set the inrange to be true
    void OnTriggerEnter (Collider other){

        playerControl = other.gameObject.GetComponent<PlayerController>();

        if (player != null) {
            m_IsPlayerInRange = true;
        }
		}//end OnTriggerEnter

    void OnTriggerExit (Collider other){
        playerControl = other.gameObject.GetComponent<PlayerController>();

        if (player != null)
        {
            m_IsPlayerInRange = false;
        }
    }// end OnTriggerEnter

    void Update (){
        if (m_IsPlayerInRange){
			//gets the direction from Pov game object to player object
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;
			// if the raycast(which is the size of the pov box collider because thats what the script is set to) hits the player than it calls the script that ends the game.
            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player && !PlayerController.m_isInvisible)
                {
					gameEnder.CaughtPlayer ();
                }
            }
        }//end big if
    }// end update
}// end class