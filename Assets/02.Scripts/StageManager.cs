using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    public static StageManager instance {  get; private set; }
    public TextMeshProUGUI PopupStageNum;
    public TextMeshProUGUI MinimapStageNum;
    public GameObject EnemySpawner;
    public GameObject MoveWall;

    public GameObject[] BossPrefab;

    public int StageNum = 0;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PopupStageNum.gameObject.SetActive(false);
        NextStage();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            GameObject bossdeath = GameObject.Find("BossDeath");
            bossdeath.GetComponent<BossDeath>().CreateWeaponItem();
        }
    }

    public void NextStage()
    {
        EnemySpawner.GetComponent<EnemySpawner>().DestroyEnemies();
        MoveWall.transform.position = new Vector2(22, 0);
        StageNum++;
        PopupStageNum.text = $"STAGE.{StageNum}";
        MinimapStageNum.text = $"STAGE.{StageNum}";

        StartCoroutine(PopupStageNum_Coroutine());
        
        EnemySpawner.GetComponent<EnemySpawner>().SpawnEnemies();
        GameObject boss = Instantiate(BossPrefab[StageNum - 1]);
        boss.transform.position = new Vector2(20, -0.4f);
    }
    private IEnumerator PopupStageNum_Coroutine()
    {
        yield return new WaitForSeconds(2.5f);
        PopupStageNum.gameObject.SetActive (true);
        yield return new WaitForSeconds(3);
        PopupStageNum.gameObject.SetActive (false);
    }
}
