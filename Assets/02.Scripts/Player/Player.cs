using System.Collections;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP;
    public int MaxHP = 50;
    public int XP;
    public int MaxXP = 15;
    public int Level = 1;
    public int Score;

    public TextMeshProUGUI LevelUP;
   
    private void Start()
    { 
        Score = 0;
        Init();

        LevelUP.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (HP <= 0)
        {
            HP = 0;
            GameManager.Instance.GameOver();
        }
        if (XP >= MaxXP)
        {
            Level++;
            Init();
            StartCoroutine(LevelUP_Coroutine());
        }
    }

    public void Init()
    {
        MaxHP = MaxHP + 2 * (Level - 1);
        MaxXP = MaxXP + 5 * (Level - 1);
        HP = MaxHP;
        XP = 0;
    }  

    private IEnumerator LevelUP_Coroutine()
    {
        LevelUP.gameObject.SetActive(true);
        yield return new WaitForSeconds(2);
        LevelUP.gameObject.SetActive(false);
    }
}