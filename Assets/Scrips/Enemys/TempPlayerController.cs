using UnityEngine.UI;
using UnityEngine;

public class TempPlayerController : MonoBehaviour, Beatable
{
    public Image fillFife;
    public PlayerLife playerLife;

    public void Hit(float value)
    {
        if (playerLife.Life <= 0) return;

        playerLife.Life  -= value;

        if (playerLife.Life < 0)
            playerLife.Life = 0;

        fillFife.fillAmount = ((1 / playerLife.MaxLife) * playerLife.Life);
    }


}
