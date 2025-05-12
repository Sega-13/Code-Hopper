using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOver : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private GameObject LevelOverScreen;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerMovement>() != null)
        {
            //Die();
            LevelOverScreen.gameObject.SetActive(true);
        }

    }
    private void Die()
    {
        playerRigidBody.bodyType = RigidbodyType2D.Static;
    }
}
