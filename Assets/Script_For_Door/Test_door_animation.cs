using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[SerializeField]
public class Dr : MonoBehaviour
{
    [SerializeReference]
    public GameObject door;
    public float openRot, closeRot, speed;
    public bool opening;
    void Update()
    {
        Vector3 currentRot = door.transform.localEulerAngles;
        if (opening)
        {
            if (currentRot.y < openRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, -openRot, currentRot.z), speed * Time.deltaTime);
            }
        }
        else
        {
            if (currentRot.y > closeRot)
            {
                door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, -closeRot, currentRot.z), speed * Time.deltaTime);
            }
        }
    }


    
}