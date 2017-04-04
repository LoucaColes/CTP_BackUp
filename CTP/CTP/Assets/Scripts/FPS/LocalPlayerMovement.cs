﻿using UnityEngine;
using System.Collections;

public class LocalPlayerMovement : MonoBehaviour {

    public float moveSpeed = 5.0f;
    public float jumpPower, scale;
    private Rigidbody rb;
    public bool grounded;
    public Transform rayPoint;
    public float maxDist;
    private float m_timer;
    public float m_timeInterval;
    private LayerMask layerMask;
    public LocalPlayer m_playerScript;
    private int m_playerId;

    // Use this for initialization
    void Start()
    {
        grounded = true;
        rb = GetComponent<Rigidbody>();
        layerMask = 1 << 8;
        layerMask = ~layerMask;
        m_playerId = m_playerScript.m_playerID;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal"+m_playerId);
        float moveVertical = Input.GetAxis("Vertical" + m_playerId);

        Vector3 movement = new Vector3(moveHorizontal, -0.5f, moveVertical);

        movement = transform.rotation * movement;

        rb.velocity = movement * moveSpeed;
        //rb.AddForce(movement * moveSpeed);
        
        if (!grounded)
        {
            if (m_timer > m_timeInterval)
            {
                FireRay();
                m_timer = 0;
            }

            m_timer += Time.deltaTime;
            
        }

        if (Input.GetAxis("Jump" + m_playerId) == 1 || Input.GetButtonDown("Jump"+m_playerId))
        {
            FireRay();
            if (grounded)
            {
                print("Jumping");
                Vector3 jumpVector = new Vector3(moveHorizontal, transform.up.y * jumpPower * scale, moveVertical);
                rb.velocity = jumpVector;
                //rb.AddForce(transform.up * jumpPower * scale);
                grounded = false;
            }
        }
    }

    void FireRay()
    {
        print("firing ray");
        Ray ray = new Ray();
        ray.origin = rayPoint.position;
        ray.direction = Vector3.down;
        bool hit = Physics.Raycast(rayPoint.position, Vector3.down, maxDist, layerMask);
        print(hit);
        if (hit)
        {
            print("hit object");
            grounded = true;
        }
        Debug.DrawRay(ray.origin, ray.direction * maxDist, Color.green, 99f);
    }
}
