using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool isDestory = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Animal") && isDestory)
        {
            Destroy(collision.gameObject);
            isDestory = false;
        }
    }
}
