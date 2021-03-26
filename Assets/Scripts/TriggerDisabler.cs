using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDisabler : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision);
        collision.gameObject.SetActive(false);
    }
}
