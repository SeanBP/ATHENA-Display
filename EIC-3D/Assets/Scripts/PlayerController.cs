using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Vector3 moveDirection;
    private bool looping = false;
    public CharacterController controller;
    public GameObject player;
    public float jumpForce;
    public float gravityScale;
    private bool menagerie = false;
    public float omega = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        //theRB = GetComponent<Rigidbody>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float height = 0f;
        float radius = 0f;
        if (menagerie)
        {
            height = 3.7f;
            radius = 8f;
        }
        else
        {
            height = 2f;
            radius = 6f;
        }
        if (looping == false)
        {

            moveDirection = (transform.forward * Input.GetAxis("Vertical")) + (transform.right * Input.GetAxis("Horizontal")) + (transform.up * Input.GetAxis("Jump") * 0.5f);

            if (moveDirection.magnitude > 1)
            {
                moveDirection = moveDirection.normalized * moveSpeed;
            }
            else
            {
                moveDirection = moveDirection * moveSpeed;
            }

            controller.Move(moveDirection * Time.deltaTime);

        }
        else
        {
            player.transform.position = new Vector3(radius * (float)Math.Cos(omega * Time.time), height, radius * (float)Math.Sin(omega * Time.time));
        }
    }
    public void StopLooping()
    {
        if (looping)
        {
            Vector3 target = new Vector3(0f, player.transform.position.y, 0f);
            Vector3 _direction = (target - player.transform.position).normalized;
            Quaternion _lookRotation = Quaternion.LookRotation(_direction);
            player.transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, 1);
        }
        looping = false;
    }
    public void toggleMenagerie()
    {
        if (menagerie)
        {
            menagerie = false;
        }
        else
        {
            menagerie = true;
        }



    }
    public void Looping()
    {
        if (looping == false)
        {
            looping = true;
        }
        else
        {
            StopLooping();
            
        }
    }
}
