using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_running : StateMachineBehaviour
{


    //List<Transform> NPC_Move = new List<Transform>();
    // UnityEngine.AI.NavMeshAgent agent;
    // // int index = 0;
    // // int size = 0;
    // Transform attacker;
    UnityEngine.AI.NavMeshAgent agent;

    // //bool run = false;
    // Transform closestAgent = null;
    // float closestDis = 999999999;
    // //bool stop = false;
    Transform esc_pt;
    // // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Transform escp=GameObject.FindGameObjectWithTag("esc").transform;
        List<Transform> esc=new List<Transform>();
        foreach (Transform mm in escp){
            esc.Add(mm);
        }
        int r = UnityEngine.Random.Range(0,esc.Count-1);
        esc_pt=esc[r];
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        GameObject.Find("Playerlist").GetComponent<npclist>().invisible.Remove(animator.gameObject); 
        GameObject.Find("Playerlist").GetComponent<npclist>().runners.Add(animator.gameObject);
    //     agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
    //     attacker = GameObject.FindGameObjectWithTag("Attacker").transform;
    //     Transform Move = GameObject.FindGameObjectWithTag("park_t").transform;
    //     Transform build = GameObject.FindGameObjectWithTag("building").transform;
    //     foreach (Transform k in Move){
    //         float NPC_dis = Vector3.Distance(k.position, agent.transform.position);
    //         float attacker_dis = Vector3.Distance(k.position, attacker.transform.position);
    //         if(NPC_dis < attacker_dis){
    //             if(closestAgent == null){
    //                 closestAgent = k;
    //                 closestDis = NPC_dis;
    //             }else if(NPC_dis < closestDis){
    //                 closestAgent = k;
    //                 closestDis = NPC_dis;
    //             }
    //         }
    //     }
        agent.SetDestination(esc_pt.position);
        
            
        
        

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // if(!stop){
        
        //     if (!run)
        //     {
                // if (!agent.pathPending && agent.remainingDistance < 1.7f)
                // {
                //     run = true;
                // }
            // }

        //     Transform Move = GameObject.FindGameObjectWithTag("park_t").transform;
        //     foreach (Transform t in Move){
        //         NPC_Move.Add(t);  
        //         size++;
        //     }


        //     int all_index =0;
            
        //     float minDistance = float.MaxValue;

        //     foreach (Transform t in NPC_Move)
        //     {
        //         float distance = Vector3.Distance(t.position, agent.transform.position);

        //         if (distance < minDistance)
        //         {
        //             minDistance = distance;
        //             closestAgent = t;
        //             index = all_index;
        //         }
        //         all_index++;
        //     }
            
        //     if (run)
        //     {

        //         run = false;
        //         float distance = Vector3.Distance(closestAgent.position, agent.transform.position);
        //         if(distance < 3.0f){
        //             // Get the NavMeshAgent component
        //             // GameObject newPrefab = Resources.Load<GameObject>("Assets/None.prefab");
                
        //             // GameObject newAgentObject = Instantiate(newPrefab, agent.transform.position, agent.transform.rotation);
        //             //agent.enabled = false;
        //             // Vector3 newPosition = new Vector3(1.0f, 0.0f, 100.0f);
        //             // agent.transform.position = newPosition;
                    
                
        //             stop = true;

                
        //         }else if(!agent.pathPending){
        //             agent.SetDestination(closestAgent.position);
        //         }
        //     }
        // }



        
       
    }

    // void SetAgentVisibility(bool isVisible)
    // {
    //     // Disable or enable the renderer based on visibility
    //     if (agentRenderer != null)
    //     {
    //         agentRenderer.enabled = isVisible;
    //     }
    // }

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
