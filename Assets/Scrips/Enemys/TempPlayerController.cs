using UnityEngine.UI;
using UnityEngine;

public class TempPlayerController : MonoBehaviour, Beatable
{
    public Image fillFife;
    public LifeManager lifeManager;

    public void Hit(float value)
    {
        if (lifeManager.Life <= 0) return;

        lifeManager.Life  -= value;

        if (lifeManager.Life < 0)
            lifeManager.Life = 0;

        fillFife.fillAmount = ((1 / lifeManager.MaxLife) * lifeManager.Life);
    }


}
