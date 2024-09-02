using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timescalei : MonoBehaviour
{
    // Start is called before the first frame update
    [Range(.1f,5)]
    public float mod;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale=mod;
    }
}
