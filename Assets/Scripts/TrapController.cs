using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapController : MonoBehaviour
{
    [SerializeField] private GameObject CodeBlock;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            Debug.Log("Collided");
            CodeBlock.SetActive(true);

        }
    }
}
