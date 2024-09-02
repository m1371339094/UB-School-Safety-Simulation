using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;


public class NPCAI : StateMachineBehaviour
{
    [SerializeReference]
    float timer=0f;
    [SerializeReference]
    List<GameObject> npc_file = new List<GameObject>();
    [SerializeReference]
    GameObject door;

    
    
    UnityEngine.AI.NavMeshAgent agent;
    
    // float distance2;
    // bool b;
    //static int destPoint = 0; //private & static does not matter
    //bool isWaiting = false;

    //Transform attacker;
    //float chaseRange = 100;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    // bool isWaiting = false;
    [SerializeReference]
    // GameObject attacker;
    // [SerializeReference]
    Transform last_one;
    [SerializeReference]
    List<GameObject> away;
    [SerializeReference]
    List<GameObject> doors;
    [SerializeReference]
    Vector3 ini;
    [SerializeReference]
    Vector3 ins;
    bool stop=false;
    bool door_open;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Transform Move = GameObject.FindGameObjectWithTag("temp").transform;
        // Debug.Log("Start movingbh");
        // if(ins!=null){
        //     Debug.Log(ins);
        // }

        if (animator.GetBool("ini_mbh")){
        // attacker = GameObject.FindGameObjectWithTag("Attacker");
        doors=GameObject.FindGameObjectsWithTag("Door").ToList();
        List<GameObject> buildings=GameObject.FindGameObjectsWithTag("building").ToList();
        int d_amt=doors.Count;
        away=GameObject.FindGameObjectsWithTag("esc").ToList();
        int a_amt=away.Count;
        // GameObject vic1 = GameObject.FindGameObjectWithTag("vic1");
        // GameObject vic2 = GameObject.FindGameObjectWithTag("vic2");
        // GameObject vic3 = GameObject.FindGameObjectWithTag("vic3");
        // GameObject vic4 = GameObject.FindGameObjectWithTag("vic4");
        // GameObject vic5 = GameObject.FindGameObjectWithTag("vic5");
        // GameObject vic6 = GameObject.FindGameObjectWithTag("vic6");
        // GameObject vic7 = GameObject.FindGameObjectWithTag("vic7");
        // GameObject vic8 = GameObject.FindGameObjectWithTag("vic8");
        // GameObject vic9 = GameObject.FindGameObjectWithTag("vic9");
        // npc_file.Add(vic1);
        // npc_file.Add(vic2);
        // npc_file.Add(vic3);
        // npc_file.Add(vic4);
        // npc_file.Add(vic5);
        // npc_file.Add(vic6);
        // npc_file.Add(vic7);
        // npc_file.Add(vic8);
        // npc_file.Add(vic9);
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
        
        ins=door.transform.GetChild(1).position;
        }else{
            agent.SetDestination(ins);
        }
        
    
          

    }

    

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        
        // float distance = Vector3.Distance(agent.transform.position, attacker.transform.position);
        if (!agent.pathPending&&agent.remainingDistance < 0.5f&&!animator.GetBool("ini_mbh")){
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

        
        // if (stop&&door.GetComponent<Animator>().GetBool("nobodyelse")){
        //     animator.transform.LookAt(door.transform);
        //     door.GetComponent<Animator>().SetBool("nobodyelse",false);
        //     door_open=true;
        // }

        // if(door_open&&door.GetComponent<Animator>().GetBool("door_open")){
        //         timer = timer + Time.deltaTime;
        //         if (timer>=2f){
        //         agent.SetDestination(ins);
        //         // Debug.Log(agent.remainingDistance);
        //         if (!agent.pathPending&&agent.remainingDistance < 0.5f){
        //         door.GetComponent<Animator>().SetBool("open",true);
        //         animator.SetBool("behind", true);
        //         }
        //         }
                
        // }

        //  foreach (GameObject t in npc_file){
        //     distance2 = Vector3.Distance(t.transform.position, agent.transform.position);
        //     UnityEngine.Animator a = t.GetComponent<Animator>();
        //     b = a.GetBool("isRunning");
        //     if(distance2 > 0.2 && distance2 < 15 && a){
        //         agent.SetDestination(away[animator.GetInteger("ext")].transform.position);
        //         animator.SetBool("isRunning", true);
        //     }
        // }
        
        // if(distance < 20&& attacker.GetComponent<Animator>().GetBool("atk")){
        //     agent.SetDestination(last_one.position);
        //     animator.SetBool("isRunning", true);
            
        // }
        




    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // animator.SetBool("ini_mbh", true);
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