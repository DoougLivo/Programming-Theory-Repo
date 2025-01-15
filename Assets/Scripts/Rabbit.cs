using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Rabbit : Animal
{
    private float m_Rabbit_MoveInterval = 3f;
    public float MoveInterval // 이동 간격
    {
        get { return m_Rabbit_MoveInterval; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("It's negative!");
            }
            else
            {
                m_Rabbit_MoveInterval = value;
            }
        }
    }

    protected override void WaitAndMove()
    {
        timer += Time.deltaTime;

        if (timer >= m_Rabbit_MoveInterval)
        {
            MoveToRandomPosition();
            timer = 0f;
        }
    }
}
