using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SampleNamespace;

public class Points : MonoBehaviour
{

    private GameObject player;
    protected CharacterSidewaysMovement playerController;

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.right, Time.deltaTime * rotateSpeed);
    }
    void Start()
    {
        player = GameManager.Instance.gameObject;
        //GameManager.Instance.RegisterPowerUp();
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject == player)
        {
            Destroy(this.gameObject);
        }
        UIManager.Instance.IncreaseScore(ScorePoints);
        //Destroy(this.gameObject);
    }

   
    private int ScorePoints = 100;
    private float rotateSpeed = 50f;
}
