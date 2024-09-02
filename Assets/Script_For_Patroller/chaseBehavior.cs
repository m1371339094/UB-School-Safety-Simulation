using UnityEngine.AI;
using UnityEngine;
using System.Collections.Generic;
//Use to write to txt, added by Zhuohan 
using System.IO;
using TMPro;

//Added by Zhuohan 
using System.Linq;

public class chaseBehavior : StateMachineBehaviour
{

    NavMeshAgent agent;
    Transform attacker;
    float timer;
    public float ChaseTime;
    //Physical time for Cop to reach attacker position
    TextMeshProUGUI Ptime;
    //Normal response time base on FBI data
    TextMeshProUGUI Rtime;
    //Total Time
    TextMeshProUGUI Ttime;

    //Added by zhuohan to display target in txt file.
    List<Transform> Targets = new List<Transform>();

    List<Transform> OL= new List<Transform>();
    //Random response time chosen from a list of previous response time
    int rrp;
    float locdistance = 0;

    //string to store the attackig location
    string attackloc;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       timer = 0f;
       //Debug.Log(animator.GetFloat("Timer"));
       agent = animator.GetComponent<NavMeshAgent>();
       

       Transform TargetsObject = GameObject.FindGameObjectWithTag("Targets").transform;
       foreach (Transform t in TargetsObject)
            Targets.Add(t);


       Transform Overlay = GameObject.FindGameObjectWithTag("OLAY").transform;
       foreach (Transform t in Overlay)
            OL.Add(t);
       Ptime= OL[0].GetComponent<TextMeshProUGUI>();
       Rtime = OL[1].GetComponent<TextMeshProUGUI>();
       Ttime = OL[2].GetComponent<TextMeshProUGUI>();


        //Randomises the usual time before response to an active shooter. By Zhuohan 
        System.Random a = new System.Random();
        // This list use data from the FBI, each of these elements are Police response time to attackers.
        List<int> numberList = new List<int>() { 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 2, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 3, 4, 4, 4, 4, 4, 4, 4, 5, 5, 5, 5, 5, 5, 6, 6, 6, 6, 8, 8, 10, 15 };
        numberList = numberList.OrderBy(x => a.Next()).ToList();
        rrp = numberList[Random.Range(0, numberList.Count - 1)];
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //5 second pause to simulate the real world wait time until response;
        //Count the time passed until the attacker is reached.
        timer = timer + Time.deltaTime;
        if (GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().in_building){
        agent.SetDestination(GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().cur_coord);
        if (!agent.pathPending && agent.remainingDistance < 5f)
        {
            GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().building.GetComponent<Script_For_Door>().pat=animator.gameObject;
            animator.gameObject.SetActive(false);

        }
        
        }

        //Timer counts up, starting from the moment the patroller start chasing the attacker, and ends as the cop pointed his gun.
        string tt = "Time to reach target: " + string.Format("{0:N3}", timer) + " seconds";
         Ptime.SetText(tt);

        //Normal response time. 
        string rr = "Time until Response: " + string.Format("{0:N3}", rrp) + " minutes";
        Rtime.SetText(rr);

        ChaseTime = timer;

        //Debug.Log(timer);
        // if (!agent.pathPending && agent.remainingDistance < 5f)
        // {
        //         animator.SetBool("isChasing", false);
        // }
       

        
    }



    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        for (int i = 0; i < Targets.Count; i++){
            locdistance = Vector3.Distance(GameObject.FindGameObjectWithTag("attacker_script").GetComponent<attacker_coord>().cur_coord, Targets[i].position);
            if (locdistance < 0.60f)
                attackloc = Targets[i].gameObject.name;
         }
        
        //to include average response time
        //int minutes = ((int)timer / 60) + rrp; 
        int minutes = (int)timer / 60;
        float seconds = timer % 60f;
        string Tr = "Total Time: " + string.Format("{0:N3}", minutes) + " minutes," + string.Format("{0:N3}", seconds) +" Seconds";
        Ttime.SetText(Tr);


        //Write to Text file the time it takes the patroller to reach attacker.

        

        //StreamReader reader = new StreamReader(path, true);
        string path = Directory.GetCurrentDirectory();
        Debug.Log(path);

        // string simulationnum = File.ReadLines("Assets/Time.txt").Last();
        // Debug.Log(simulationnum);
        // string[] word = simulationnum.Split(",");
        // Debug.Log(word);

        // StreamWriter writer = new StreamWriter(path, true);

        // int sim = int.Parse(word[0])+1;


        // //to include average response time
        // //writer.WriteLine(sim+","+attackloc+","+timer + (60*rrp)+"seconds");
        // writer.WriteLine(sim+","+attackloc+","+timer +"seconds");
        // //writer.WriteLine(attackloc+","+timer + (60*rrp));

        // writer.Close();

        // animator.SetFloat("Timer", timer);
        
        
        agent.SetDestination(agent.transform.position);
        animator.SetBool("readytoshoot", true);
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
