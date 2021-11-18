using UnityEngine;

public class Events : MonoBehaviour
{
    #region PlayerEvents
    public static FloatEvent OnImprovePlayerLife;
    #endregion

    public delegate void SimpleEvent();
    public delegate void IntEvent(int i);
    public delegate void FloatEvent(float s);
    public delegate void BoolEvent(bool b);
}
