using UnityEngine;
using UnityEngine.UI;

public class EnemyUIManager : MonoBehaviour
{

    public Slider HPBar;
    public Slider DecontaminationBar;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void setHPValue(float value)
    {
        HPBar.value = value;
    }
    public void setDecontaminationValue(float value)
    {
        DecontaminationBar.value = value;
    }
}
