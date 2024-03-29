using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Sprite Config", menuName = "ScriptableObjects/Sprite")]
public class SpriteSO : ScriptableObject
{
    //Scriptable Objects act as serializable data containers
    public Sprite sprite = null;
    public float scale = 1f;
    public Color color = Color.white;
}
