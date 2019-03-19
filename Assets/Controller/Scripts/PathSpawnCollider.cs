using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawnCollider : MonoBehaviour
{
    
    public Transform PathSpawnPoints;
    public GameObject Path;
    

    void OnTriggerEnter(Collider hit)
    {
        //player has hit the collider
        if (hit.gameObject.tag == Constants.PlayerTag)
        {
            Vector3 rotation = PathSpawnPoints.rotation.eulerAngles;
           
            Vector3 position = PathSpawnPoints.position;
             
            Instantiate<GameObject>(Path, position, Quaternion.Euler(rotation));
        }
    }
}


