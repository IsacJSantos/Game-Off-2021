using UnityEngine;
using UnityEngine.UI;

public class TempAntHillController : MonoBehaviour, Beatable
{

    public Image fillLife;
    public BaseLifeSystem anthillLife;
    public void Hit(float value)
    {
        if (anthillLife.Life <= 0) return;

        anthillLife.Life -= value;

        if (anthillLife.Life < 0)
            anthillLife.Life = 0;

        fillLife.fillAmount = ((1 / anthillLife.MaxLife) * anthillLife.Life);
    }


}
