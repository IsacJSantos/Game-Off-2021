using UnityEngine;
public class PlayerLife : BaseLifeSystem
{
    public override void Awake()
    {
        Events.OnImprovePlayerLife += ImproveMaxLife;
    }
    public override void OnDestroy()
    {
        Events.OnImprovePlayerLife -= ImproveMaxLife;
    }

}
