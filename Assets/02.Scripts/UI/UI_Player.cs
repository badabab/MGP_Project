using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Player : MonoBehaviour
{
    public GameObject Player;

    public Slider HP_Slider;
    public Slider XP_Slider;

    public TextMeshProUGUI HP_Count;
    public TextMeshProUGUI XP_Count;

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        HP_Slider.value = Player.GetComponent<Player>().HP / (float)Player.GetComponent<Player>().MaxHP;
        XP_Slider.value = Player.GetComponent<Player>().XP / (float)Player.GetComponent<Player>().MaxXP;

        HP_Count.text = $"{Player.GetComponent<Player>().HP}/{Player.GetComponent<Player>().MaxHP}";
        XP_Count.text = $"{Player.GetComponent<Player>().XP}/{Player.GetComponent<Player>().MaxXP}";
    }
}