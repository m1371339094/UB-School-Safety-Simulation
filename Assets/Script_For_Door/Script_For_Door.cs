
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
[SerializeField]
public class Script_For_Door : MonoBehaviour
{
    
    public Queue<GameObject> users = new Queue<GameObject>();
    public List<GameObject> outuser = new List<GameObject>();
    // public List<GameObject> uu;
    public bool override_user;
    [SerializeReference]
    public bool set_all_active=false;
    public GameObject current_user;
    public float openRot=90;
    public float closeRot;
    public float speed=5;
    public GameObject door;
    public bool animationa;
    public GameObject atk;

    public bool Open;
    public bool attacker_in;
    public GameObject pat;

    void Start()
    {
    atk = GameObject.FindGameObjectWithTag("Attacker");
    door=gameObject;  
    openRot=90;
    speed=5;
    override_user=false; animationa = false; Open = false;attacker_in=false;

    }
    void Update()
    {
        
        Vector3 currentRot = door.transform.localEulerAngles;
        // Debug.Log(gameObject.GetComponent<Script_For_Door>().openRot); bool dequeuedSuccessfully = 
        if (current_user==null){
            if (users.Count!=0){
                current_user=users.Dequeue();
                outuser.Add(current_user);
            }
            
            }
        
        
        if (!attacker_in){
            if (current_user!=null||override_user)
        {
            if (Open){
                opendoor(currentRot);        
            }
            else{
                closedoor(currentRot);
                
            }
        }
        }else{
            if (!set_all_active){
                foreach (GameObject npcs in outuser){
                    if(atk!=npcs){
                    npcs.SetActive(true);
                    npcs.GetComponent<Animator>().SetBool("isRunning",true);    
                    }            
                }
                set_all_active=true;
            }

            opendoor(currentRot);
        }
        
        
    }


    void opendoor(Vector3 currentRot)
    {
        //reverse (currentRot.y == 0||currentRot.y > 270) and -openrot
        // Debug.Log("open "+currentRot.y);
        if (currentRot.y <= openRot-1)
            {
                animationa=true;
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);
                return ;
            }
        animationa=false;
        return ;
    }
    void closedoor(Vector3 currentRot)
    {
        // Debug.Log("close "+currentRot.y);
        if (currentRot.y >= closeRot)
            {
                animationa=true;
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, -closeRot, currentRot.z), speed * Time.deltaTime);
                return ;
            }
        
        animationa=false;
        return ;
    }
    public static bool HasParameter(string paramName, Animator animator)
    {
    foreach (AnimatorControllerParameter param in animator.parameters)
    {
        if (param.name == paramName) return true;
    }
    return false;
    }



    
}