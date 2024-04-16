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
    public TextMeshProUGUI Level;

    private void Update()
    {
        Refresh();
    }

    public void Refresh()
    {
        Player player = Player.GetComponent<Player>();
        HP_Slider.value = player.HP / (float)player.MaxHP;
        XP_Slider.value = player.XP / (float)player.MaxXP;

        HP_Count.text = $"{player.HP}/{player.MaxHP}";
        XP_Count.text = $"{player.XP}/{player.MaxXP}";
        Level.text = $"Lv.{player.Level}";
    }
}