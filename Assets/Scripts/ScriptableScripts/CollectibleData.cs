using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCollectibleData", menuName = "Collectibles/CollectibleData")]
public class CollectibleData : ScriptableObject
{
    public string collectibleName;
    public Sprite icon;
    public int value;

    public CollectibleType type;

    public AudioClip collectSound;
    public GameObject collectEffect;
}
