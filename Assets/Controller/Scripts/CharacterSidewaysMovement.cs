using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSidewaysMovement : CharacterSpeed
{

    private Vector3 moveDirection = Vector3.zero;

    private float dive, propel, MLeft, MRight;


    // Start is called before the first frame update
    void Start()
    {
        moveDirection = transform.forward;
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= Speed;

        UIManager.Instance.ResetScore();
        //UIManager.Instance.SetStatus(Constants.StatusClickToStart);

        GameManager.Instance.GameState = GameState.Start;

    }


   private void DetectPropelDiveOrMoveLeftRight()
    {
        MLeft = MRight = Input.GetAxis("Horizontal");
        propel = dive = -Input.GetAxis("Vertical");

        transform.Translate(MLeft * Time.deltaTime * forward * SideWaysSpeed, 0, 0);

        transform.Translate(MRight * Time.deltaTime * forward * SideWaysSpeed, 0, 0);

        transform.Translate(0, 0, propel * Time.deltaTime * PDSpeed);

        transform.Translate(0, 0, dive * Time.deltaTime * PDSpeed);


        if (transform.position.y > 3.0)
        {
            Vector3 pos = new Vector3(transform.position.x, 3.0f, transform.position.z);
            transform.position = pos;
        }

        if (transform.position.y < 1.3)
        {
            Vector3 pos = new Vector3(transform.position.x, 1.3f, transform.position.z);
            transform.position = pos;
        }

    } 

    // Update is called once per frame
    void Update()
    {
        SpeedUpdate();
        transform.Translate(0, forward * Time.deltaTime * Speed, 0);
        DetectPropelDiveOrMoveLeftRight();

    } 

}
