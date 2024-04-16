using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP;
    public int MaxHP = 50;
    public int XP;
    public int MaxXP = 15;
    public int Level = 1;
    public int Score;
   
    private void Start()
    { 
        Init();
    }

    private void Update()
    {
        if (HP <= 0)
        {
            GameManager.Instance.GameOver();
        }
        if (XP >= MaxXP)
        {
            Level++;
            Init();
        }
    }

    public void Init()
    {
        MaxHP = MaxHP + 2 * (Level - 1);
        MaxXP = MaxXP + 5 * (Level - 1);
        HP = MaxHP;
        XP = 0;
        Score = 0;
    }  
}