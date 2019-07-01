using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatetrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GetComponent<Animator>().Play("Move");
    }
}
