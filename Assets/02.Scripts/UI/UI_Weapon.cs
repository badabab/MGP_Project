using TMPro;
using UnityEngine;

public class UI_Weapon : MonoBehaviour
{
    public TextMeshProUGUI WindWeaponText;
    public TextMeshProUGUI FireWeaponText;
    public TextMeshProUGUI ArrowWeaponText;
    public GameObject Player;

    private void Update()
    {
        PlayerWeapon weapon = Player.GetComponent<PlayerWeapon>();
        int windLevel = weapon.WindWeaponLevel;
        int fireLevel = weapon.FireWeaponLevel;
        int arrowLevel = weapon.ArrowWeaponLevel;

        WindWeaponText.text = $"Lv.{windLevel}";
        FireWeaponText.text = $"Lv.{fireLevel}";
        ArrowWeaponText.text = $"Lv.{arrowLevel}";
    }
}