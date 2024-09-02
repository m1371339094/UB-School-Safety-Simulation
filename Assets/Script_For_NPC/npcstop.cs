using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class npcstop : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    bool myturn=false;
    bool active=true;
    float timer=0f;
    GameObject door;
    Transform esc_pt;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("ini_mbh")){
        int dr=animator.GetInteger("whichdoor");
        List<GameObject> doors=GameObject.FindGameObjectsWithTag("Door").ToList();
        static GameObject match_doors(List<GameObject> drs, int tar){
            foreach (GameObject game in drs){
                if (game.name.Contains(tar.ToString())){
                    return game;
                }
            }
            Debug.Log("error");
            return null;
        }

        door=match_doors(doors,dr);
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Transform escp=GameObject.FindGameObjectWithTag("esc").transform;
        List<Transform> esc=new List<Transform>();
        foreach (Transform mm in escp){
            esc.Add(mm);
        }
        int r = UnityEngine.Random.Range(0,esc.Count-1);
        esc_pt=esc[r];
        
        
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("ini_mbh")){
        if (door.GetComponent<Script_For_Door>().Open&&!door.GetComponent<Script_For_Door>().animationa&&myturn){
            animator.SetBool("ini_mbh",false);
            animator.SetBool("Stop",false);
            animator.SetBool("going_in",true);
            return ; 
        }
        if(door.GetComponent<Script_For_Door>().current_user==animator.gameObject){
            door.GetComponent<Script_For_Door>().Open=true;
            myturn=true;
        }
        }
        if(!animator.GetBool("ini_mbh")){
            timer = timer + Time.deltaTime;
            if (timer>2f&&active){
                animator.SetBool("isRunning",true);
                animator.gameObject.SetActive(false);
                GameObject.Find("Playerlist").GetComponent<npclist>().player.Remove(animator.gameObject); 
                GameObject.Find("Playerlist").GetComponent<npclist>().invisible.Add(animator.gameObject);

                active=false;
                door.GetComponent<Script_For_Door>().current_user=null;
                
            }else if(timer>2.1f){
                // agent.SetDestination(esc_pt.position);
                animator.SetBool("isRunning",true);

            }

            // else if(door.GetComponent<Script_For_Door>().attacker_in){
            //     if(!animator.GetBool("isRunning")){
            //     List<Transform> esc=new List<Transform>();
            //     foreach (Transform t in GameObject.FindGameObjectWithTag("esc").transform){
            //         esc.Add(t);
            //     }
            //     int r = UnityEngine.Random.Range(0,esc.Count);
            //     animator.gameObject.SetActive(true);
            //     agent.SetDestination(esc[r].position);
            //     animator.SetBool("isRunning",true);
            //     }


            // }
        }
        // if(!active&&door.GetComponent<Script_For_Door>().attacker_in){
        //     if(door.GetComponent<Script_For_Door>().Open&&!door.GetComponent<Script_For_Door>().animationa){
        //         animator.gameObject.SetActive(true);
        //         active=true;
        //     }
        // }

        


        // timer = timer + Time.deltaTime;
            
        // if (timer>1f){
        //     // if (!stop&&!agent.pathPending&&agent.remainingDistance < 0.5f)
        //     // {
        //     //     stop=true;
        //     //     agent.SetDestination(agent.transform.position);
        //     //     door.GetComponent<Animator>().SetBool("open",false);
        //     // }
            
        // }
        // // stop&&
        // if (active){
        //     if (timer>2f){
        //         animator.gameObject.SetActive(false);
        //     }
        // }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

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
