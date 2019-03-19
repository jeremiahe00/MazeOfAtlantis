using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.right, Time.deltaTime * rotateSpeed);
    }

    void OnTriggerEnter(Collider col)
    {
        UIManager.Instance.IncreaseScore(ScorePoints);
        Destroy(this.gameObject);
    }

    private int ScorePoints = 100;
    private float rotateSpeed = 50f;
}
