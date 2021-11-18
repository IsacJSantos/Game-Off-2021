using UnityEngine.UI;
using UnityEngine;

public class TempPlayerController : MonoBehaviour, Beatable
{
    public Image fillLife;
    public BaseLifeSystem playerLife;

    public void Hit(float value)
    {
        if (playerLife.Life <= 0) return;

        playerLife.Life  -= value;

        if (playerLife.Life < 0)
            playerLife.Life = 0;

        fillLife.fillAmount = ((1 / playerLife.MaxLife) * playerLife.Life);
    }


}
