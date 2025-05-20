using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public CollectibleData data;
    public CollectManager collectManager;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<PlayerMovement>() != null) 
        {
            ApplyCollectibleEffect();
            PlayCollectFeedback();
            Destroy(gameObject);
        }
    }

    void ApplyCollectibleEffect()
    {
        switch (data.type)
        {
            case CollectibleType.Crystal:
                collectManager.AddCrystal(data.value);
                break;

            case CollectibleType.Coin:
                collectManager.AddCoin(data.value);
                break;

            case CollectibleType.Reward:
                collectManager.GrantReward(data.collectibleName);
                break;
        }
    }

    void PlayCollectFeedback()
    {
        if (data.collectEffect)
            Instantiate(data.collectEffect, transform.position, Quaternion.identity);

        if (data.collectSound)
            AudioSource.PlayClipAtPoint(data.collectSound, transform.position);
    }
}


