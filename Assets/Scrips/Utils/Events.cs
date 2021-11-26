using UnityEngine;

public class Events : MonoBehaviour
{
    #region PlayerEvents
 
    #endregion
    #region AnthillEvents
 
    #endregion
    #region CardsManager
    public static SimpleEvent OnShowCards;
    public static SimpleEvent OnCardClicked;

    public static FloatEvent OnImproveAnthillLife;
    public static FloatEvent OnImprovePlayerLife;
    public static SimpleEvent OnImproveDamage;
    public static SimpleEvent OnImproveMagazine;
    public static SimpleEvent OnDecreaseAbilityCooldown;
    public static SimpleEvent OnDecreaseReloadDelay;
    #endregion
    #region PlayerSpecialAttack
    public static FloatEvent OnHealingPlayer;
    public static Vector3FloatFloatFloatEvent OnBombExplode;
    #endregion

    public delegate void SimpleEvent();
    public delegate void IntEvent(int i);
    public delegate void FloatEvent(float f);
    public delegate void BoolEvent(bool b);

    public delegate void Vector3FloatFloatFloatEvent( Vector3 v3,float f1,float f2, float f3);
}
