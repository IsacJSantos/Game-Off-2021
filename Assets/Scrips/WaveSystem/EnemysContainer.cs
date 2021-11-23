using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "ScriptableObjects/EnemyContainer", order = 1)]
public class EnemysContainer : ScriptableObject
{
    public GameObject[] enemyList;
}
