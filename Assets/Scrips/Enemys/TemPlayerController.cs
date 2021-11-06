using UnityEngine.UI;
using UnityEngine;

public class TemPlayerController : MonoBehaviour, Beatable
{
    public Image fillFife;
    public float maxLife = 50;
    public float life = 50;
    public void Hit(float value)
    {
        if (life <= 0) return;

        life -= value;

        if (life < 0)
            life = 0;

        fillFife.fillAmount = ((1 / maxLife) * life);
    }


}
