using UnityEngine;

public class Events : MonoBehaviour
{
    #region PlayerEvents
    public static FloatEvent OnImprovePlayerLife;
    #endregion
    #region AnthillEvents
    public static FloatEvent OnImproveAnthillLife;
    #endregion

    #region CardsManager
    public static SimpleEvent OnShowCards;
    public static SimpleEvent OnCardClicked;
    #endregion

    public delegate void SimpleEvent();
    public delegate void IntEvent(int i);
    public delegate void FloatEvent(float s);
    public delegate void BoolEvent(bool b);
}
