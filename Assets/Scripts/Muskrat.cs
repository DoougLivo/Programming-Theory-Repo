using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muskrat : Animal
{
    private float m_MoveInterval = 4f; // 이동 간격
    public float MoveInterval
    {
        get { return m_MoveInterval; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("It's negative!");
            }
            else
            {
                m_MoveInterval = value;
            }
        }
    }

    protected override void WaitAndMove()
    {
        timer += Time.deltaTime;

        if (timer >= m_MoveInterval)
        {
            MoveToRandomPosition();
            timer = 0f;
        }
    }
}
