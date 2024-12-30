using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animal : MonoBehaviour
{
    protected NavMeshAgent agent;
    private float m_MoveInterval = 5f; 

    protected float timer;

    protected void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        MoveToRandomPosition();
    }

    protected void Update()
    {
        WaitAndMove();
    }

    protected virtual void WaitAndMove()
    {
        timer += Time.deltaTime;

        if (timer >= m_MoveInterval)
        {
            MoveToRandomPosition();
            timer = 0f;
        }
    }

    protected void MoveToRandomPosition()
    {
        // 랜덤한 방향으로 NavMesh 내 위치를 선택
        Vector3 randomDirection = Random.insideUnitSphere * 1f; // 반경 1
        randomDirection += transform.position;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, 5f, NavMesh.AllAreas))
        {
            agent.SetDestination(hit.position);
        }
    }
}
