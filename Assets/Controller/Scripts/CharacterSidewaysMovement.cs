using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace SampleNamespace
{
    public class CharacterSidewaysMovement : CharacterSpeed
    {

        //[SerializeField] private LayerMask layerMask;

        private Vector3 moveDirection = Vector3.zero;

        protected float dive, propel, MLeft, MRight;

        private GameObject waterTrail;
        

        // Start is called before the first frame update
        void Start()
        {
            waterTrail = (GameObject)FindObjectOfType(typeof(GameObject));
           //waterTrail = GameObject.FindWithTag("Water") as GameObject;

            moveDirection = transform.forward;
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= Speed;

            UIManager.Instance.ResetScore();
            //UIManager.Instance.SetStatus(Constants.StatusClickToStart);

            GameManager.Instance.GameState = GameState.Start;

        }

        void OnTriggerEnter(Collider col)
        {

            if (col.gameObject.tag == "Water")
            {

                StartCoroutine(speedBoost());
                Destroy(col.gameObject);
            }
        }

        IEnumerator speedBoost()
        {

            MoveUpandDown moveupanddown = waterTrail.AddComponent<MoveUpandDown>() as MoveUpandDown;
            moveupanddown.MovePlayer();
            yield return new WaitForSeconds(7f);

            MoveLeftandRight moveleftandright = waterTrail.AddComponent<MoveLeftandRight>() as MoveLeftandRight;
            moveleftandright.MovePlayer();

            yield return new WaitForSeconds(3f);
        }

        public void DetectPropelDiveOrMoveLeftRight()
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
        public void Update()
        {
            SpeedUpdate();
            transform.Translate(0, forward * Time.deltaTime * Speed, 0);
           
            DetectPropelDiveOrMoveLeftRight();

        }

    }


    public class MoveLeftandRight : CharacterSidewaysMovement, IStrategyInterface
    {

        public void MovePlayer()
        {
            float boost = 100f;
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * boost * Time.deltaTime, ForceMode.Impulse);
            }
        }

    }
    public class MoveUpandDown : CharacterSidewaysMovement, IStrategyInterface
    {

        public void MovePlayer()
        {
            float boost = 500f;
            if (GetComponent<Rigidbody>() != null)
            {
                GetComponent<Rigidbody>().AddForce(GetComponent<Rigidbody>().velocity.normalized * boost * Time.deltaTime, ForceMode.Impulse);
            }

        }

    }

}