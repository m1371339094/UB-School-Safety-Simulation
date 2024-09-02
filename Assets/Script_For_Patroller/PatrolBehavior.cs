using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
public class PatrolBehavior : StateMachineBehaviour
{
    List<Transform> Checkpoints = new List<Transform>();

    //Target locations, added by zhuohan
    List<Transform> Targets = new List<Transform>();
    static int destPoint = 0; 
    NavMeshAgent agent;
    //static int destPoint = 0; //private & static does not matter
    bool isWaiting = false;

    // Transform attacker;
    float locdistance = 0;

    float pause = 0;
    //float chaseRange = 25;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
        

        Transform CheckpointsObject = GameObject.FindGameObjectWithTag("Checkpoints").transform;
        foreach (Transform t in CheckpointsObject)
            Checkpoints.Add(t);

        //Target location initalization added by Zhuohan 
        Transform TargetsObject = GameObject.FindGameObjectWithTag("Targets").transform;
        foreach (Transform t in TargetsObject)
            Targets.Add(t);
        agent = animator.GetComponent<NavMeshAgent>();
        agent.SetDestination(Checkpoints[destPoint].position);
        isWaiting = false;
    
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


        if (!isWaiting)
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                isWaiting = true;
                animator.SetBool("isPatrolling", false);
            }
        }
        
        if (isWaiting)
        {
            isWaiting = false;
            destPoint = (destPoint + 1) % Checkpoints.Count; 
            agent.SetDestination(Checkpoints[destPoint].position);
            // agent.SetDestination(Checkpoints[Random.Range(0, Checkpoints.Count)].position);
        }
        // float distance = Vector3.Distance(animator.transform.position, GameObject.FindGameObjectWithTag("Attacker").GetComponent<attacker_coord>().cur_coord);

        //Debug.Log(locdistance);
        /*if (locdistance < 0.6f)
            animator.SetBool("isChasing",true);*/
        // Check if the attacker is near a target, if an attacker is, change state to is chasing. Made by Zhuohan 
        for (int i = 0; i < Targets.Count; i++){
            locdistance = Vector3.Distance(GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().cur_coord, Targets[i].position);
        //the distance should be less than 0.6
            // Debug.Log(locdistance);
            if (GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().in_building){
                pause += Time.deltaTime;
                if(pause > 3 ){
                //5 second cool down to simulate the time(1 to 15 minutes) it takes for the patroller to get the call.
                /*if(pause > 5f) {
                    animator.SetBool("isChasing", true);
                }*/            
                animator.SetBool("isChasing", true);}
            }
        }


        //Have a chance of activating the patroller as they get closer to the attacker. 
        //Use the fact that as distance get smaller, Confront percent get larger. Only works if patroller is about 50 units from from attacker.
        //int ConfrontChance = Random.Range(1,100);
        //float Confrontpercent = 5f/distance;
        //Debug.Log(ConfrontChance);
        //if (distance <50 && Confrontpercent * 100f > ConfrontChance)
        //   animator.SetBool("isChasing", true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        agent.SetDestination(agent.transform.position);
    }
    

    

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
