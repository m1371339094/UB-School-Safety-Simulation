// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.AI;

// public class Attack : StateMachineBehaviour
// {
//     // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
//      NavMeshAgent agent;
//      Animator pat;
//      public GameObject[] respawns;

//     override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//        agent = animator.GetComponent<NavMeshAgent>();
//        pat = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
//        respawns= GameObject.FindGameObjectsWithTag("Player");
       
//     }

//     // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
//     override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
//         if (pat.GetBool("readytoshoot")){
//             animator.SetBool("handup",true);
//         }
       
//     }

//     // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
//     override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     {
       
//     }

//     // OnStateMove is called right after Animator.OnAnimatorMove()
//     //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     //{
//     //    // Implement code that processes and affects root motion
//     //}

//     // OnStateIK is called right after Animator.OnAnimatorIK()
//     //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
//     //{
//     //    // Implement code that sets up animation IK (inverse kinematics)
//     //}
// }
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Attack : StateMachineBehaviour
{
    UnityEngine.AI.NavMeshAgent agent;
    // bool stop=false;
    bool active=true;
    float timer=0f;
    GameObject door;
    GameObject cur_building;
    List<GameObject> doors;
    

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("ini_mbh")){
        int dr=animator.GetInteger("whichdoor");
        doors=GameObject.FindGameObjectsWithTag("Door").ToList();
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
        List <GameObject> buildings=GameObject.FindGameObjectsWithTag("building").ToList();
        foreach (GameObject building in buildings){
            if (door.transform.IsChildOf(building.transform)){
                cur_building=building;
                break;
            }
        }
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.GetBool("ini_mbh")){
        if (door.GetComponent<Script_For_Door>().Open&&!door.GetComponent<Script_For_Door>().animationa){
            animator.SetBool("ini_mbh",false);
            animator.SetBool("Stop",false);
            animator.SetBool("going_in",true);
            return ; 
        }
        if(door.GetComponent<Script_For_Door>().current_user==animator.gameObject){
            door.GetComponent<Script_For_Door>().Open=true;
        }
        }
        if(!animator.GetBool("ini_mbh")){
            timer = timer + Time.deltaTime;
            if (timer>5f){
                GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().in_building=true;
                
                animator.gameObject.SetActive(false);
                active=false;
                door.GetComponent<Script_For_Door>().current_user=null;
                foreach (GameObject dr in doors){
                    if (dr.transform.IsChildOf(cur_building.transform)){
                        GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().building=dr;
                        dr.GetComponent<Script_For_Door>().attacker_in=true;
                    }
                }

            }
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
