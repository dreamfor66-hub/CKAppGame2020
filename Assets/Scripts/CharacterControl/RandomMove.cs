﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RandomMove : MonoBehaviour
{
    public NavMeshAgent nav;
    public GameObject target;
    [SerializeField]
    private Vector3 targetPostion;
    [SerializeField]
    private float speed;
    [SerializeField]
    private bool isArrive;
    [SerializeField]
    private float setTimer = 0.5f;
    private float timer;

    public bool In1StFloor = true;

    // Start is called before the first frame update
    void Start()
    {
        //targetPostion = target.transform.position;

        targetPostion.x = Random.Range(0, 10);
        targetPostion.z = Random.Range(0, 10);

        if (gameObject.GetComponent<NavMeshAgent>().enabled)
        {
            nav.SetDestination(targetPostion);
        }

        timer = setTimer;
    }

    // Update is called once per frame
    void Update()
    {
        SetRnadomizeDestination();
    }

    private void SetRnadomizeDestination()
    {
        if (Vector3.Distance(transform.position, nav.destination) <= 1f)
        {
            isArrive = true;
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (In1StFloor)
                {
                    targetPostion.x = Random.Range(0, 10);
                    targetPostion.z = Random.Range(0, 10);
                }
                else
                {
                    targetPostion.x = Random.Range(-10, 0);
                    targetPostion.z = Random.Range(-10, 0);
                }
                nav.SetDestination(targetPostion);
                timer = setTimer;
            }
        }
        else
        {
            isArrive = false;
        }
    }

    private void MoveCheck()
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(nav.destination, 0.1f);
    }

    public bool Arrived
    {
        get { return isArrive; }
        //set { isArrive = value; }
    }

    public Vector3 GetTargetPos()
    {
        return targetPostion;
    }

    public void SetTargerFloor(int floor)
    {
        if (floor == 2)
        {
            targetPostion.y = 5.7f;
        }
        else
        {
            targetPostion.y = 0f;
        }
    }
}
