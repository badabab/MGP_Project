using UnityEngine;

public class Player : MonoBehaviour
{
    public int HP;
    public int MaxHP = 50;
    public int XP;
    public int MaxXP = 15;
   
    private void Start()
    {      
        Init();
    }

    public void Init()
    {
        HP = MaxHP;
        XP = MaxXP;
    }  
}