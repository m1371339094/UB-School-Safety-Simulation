using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class npclist : MonoBehaviour

{
    [SerializeField]
    public List<GameObject> player;
    public List<GameObject> runners;
    public List<GameObject> invisible;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.FindGameObjectsWithTag("NPC").ToList();
        player.Add(GameObject.Find("Patroller"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
