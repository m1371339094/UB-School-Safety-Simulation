using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attacker_coord : MonoBehaviour
{
    public GameObject attacker;
    public Vector3 cur_coord;
    public AudioSource aud;
    public AudioSource aud2;
    public bool play;
    public bool in_building=false;
    public GameObject building;
    public int frame=0;
    // Start is called before the first frame update
    void Start()
    {
     cur_coord=attacker.transform.position;  
     play=false; 
    }

    // // Update is called once per frame
    void Update()
    {
    
     cur_coord=attacker.transform.position;
     
     if (!attacker.activeSelf&&!play){
        Debug.Log("playing");
        play=true;
        
        aud.Play();
     } 
     if (play)  {
        frame+=1;
     }
     if (frame==3){
        Debug.Log("scream");
        aud2.Play();
     }
        
    }
}
