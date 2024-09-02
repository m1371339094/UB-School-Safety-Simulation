using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;

public class MovingBh : StateMachineBehaviour
{
    List<Transform> NPC_Move = new List<Transform>();
    [SerializeReference]
    float timer=0f;
    [SerializeReference]
    List<GameObject> npc_file = new List<GameObject>();
    [SerializeReference]
    GameObject door;
    UnityEngine.AI.NavMeshAgent agent;
    // GameObject attacker;
    [SerializeReference]
    Transform last_one;
    [SerializeReference]
    List<GameObject> away;
    [SerializeReference]
    List<GameObject> doors;
    [SerializeReference]
    Vector3 ini;
    [SerializeReference]
    Vector3 ins;
    
    bool door_open;
    Animator pat;

    int rrp;
    //OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
       if (animator.GetBool("ini_mbh")){
        // attacker = GameObject.FindGameObjectWithTag("Attacker");
        doors=GameObject.FindGameObjectsWithTag("Door").ToList();
        List<GameObject> buildings=GameObject.FindGameObjectsWithTag("building").ToList();
        int d_amt=doors.Count;
        away=GameObject.FindGameObjectsWithTag("esc").ToList();
        int a_amt=away.Count;
        pat = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        // foreach (GameObject building in buildings){
        //     foreach (GameObject door in doors){
        //         if (door.transform.IsChildOf(building.transform)){
        //             Debug.Log("true");
        //         }
        //         else{Debug.Log("oooooooo");}
        //     }
        // }
        // this function prove that under the tag building of door the building can detect door
        agent = animator.GetComponent<UnityEngine.AI.NavMeshAgent>();
        last_one = agent.transform;
        
        int r = Random.Range(0,d_amt-1);
        Regex regex = new Regex(@"\d+");
        // Debug.Log(doors[r].name);
        Match match = regex.Match(doors[r].name);
        animator.SetInteger("whichdoor",int.Parse(match.Value));
        // door= doors[r];
        door= doors[r];
        ini=door.transform.GetChild(0).position;
        agent.SetDestination(ini);
        // Debug.Log(door.transform.GetChild(0));
        ins=door.transform.GetChild(1).position;
        }else{
            agent.SetDestination(ins);
        }
    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!agent.pathPending&&agent.remainingDistance < 0.9f&&!animator.GetBool("ini_mbh")){
                agent.SetDestination(agent.transform.position);
                animator.SetBool("going_in",false);
                door.GetComponent<Script_For_Door>().Open=false;
                animator.SetBool("Stop",true); 
                return ;
                
        }

        if (!agent.pathPending&&agent.remainingDistance < 0.5f&&animator.GetBool("ini_mbh"))
            {
                if (!animator.GetBool("Stop")){
                Queue<GameObject> users=door.GetComponent<Script_For_Door>().users;
                // door.GetComponent<Script_For_Door>().Open=true;
                users.Enqueue(animator.gameObject);
                }
                // agent.SetDestination(ini);
                // stop=true;
                agent.SetDestination(agent.transform.position);
                // animator.transform.LookAt(door.transform);  
               
                animator.SetBool("Stop",true); 
                return ;
        }

        // if (pat.GetBool("readytoshoot")){
        //     animator.SetBool("handup",true);
        // }
        

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // agent.SetDestination(agent.transform.position);
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
