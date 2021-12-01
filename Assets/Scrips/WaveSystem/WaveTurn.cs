using UnityEngine;
[System.Serializable]
public class WaveTurn
{
    public EnemySpawn[] EnemyTypes;
}

[System.Serializable]
public class EnemySpawn
{
    public int amout;
    public EnemyChoose enemyType;
}
[System.Serializable]
public enum EnemyChoose
{
    Enemy1,
    Enemy2,
    Enemy3,
    Enemy4,
    Enemy5,
    Enemy6
}
