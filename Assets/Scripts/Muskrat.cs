using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muskrat : Animal
{
    private float m_Muskrat_MoveInterval = 4f; // �̵� ����
    public float MoveInterval
    {
        get { return m_Muskrat_MoveInterval; }
        set
        {
            if (value < 0.0f)
            {
                Debug.LogError("It's negative!");
            }
            else
            {
                m_Muskrat_MoveInterval = value;
            }
        }
    }

    protected override void WaitAndMove()
    {
        timer += Time.deltaTime;

        if (timer >= m_Muskrat_MoveInterval)
        {
            MoveToRandomPosition();
            timer = 0f;
        }
    }
}
