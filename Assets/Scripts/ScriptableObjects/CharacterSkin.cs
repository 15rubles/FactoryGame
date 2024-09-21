using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSkin", menuName = "ScriptableObjects/CharacterSkin", order = 1)]
public class CharacterSkin : ScriptableObject
{
    public GameObject skinPrefab;
    public int price;
}
