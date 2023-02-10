using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Analytics;
using UnityEngine;
using UnityEngine.AI;

public class MoveCharacter : MonoBehaviour
{
    NavMeshAgent agent;
    private Animator anim;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            AnalyticsService.Instance.CustomData("tapOnScreen", new Dictionary<string, object>());
        }

        if (Input.GetMouseButton(0) || Input.touchCount > 0)
        {
            RaycastHit hit;

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100))
            {
                agent.destination = hit.point;
            }
        }

        anim.SetFloat("Speed", agent.velocity.magnitude);
    }
}