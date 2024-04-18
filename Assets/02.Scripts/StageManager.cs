using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    public static StageManager instance {  get; private set; }
    public TextMeshProUGUI PopupStageNum;
    public TextMeshProUGUI MinimapStageNum;
    public GameObject EnemySpawner;
    public GameObject MoveWall;

    public GameObject[] BossPrefab;

    public int StageNum = 0;
    public GameObject[] Backgrounds;
    public Image UI_Stat;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        PopupStageNum.gameObject.SetActive(false);
        //NextStage();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            EnemySpawner.GetComponent<EnemySpawner>().DestroyEnemies();
            Boss boss = GameObject.FindAnyObjectByType<Boss>();
            Destroy(boss.gameObject);
            NextStage();
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
        //GameObject boss = Instantiate(BossPrefab[StageNum % BossPrefab.Length - 1]);
        int index = (StageNum - 1) % BossPrefab.Length;
        if (index < 0)
        {
            index += BossPrefab.Length;
        }
        GameObject boss = Instantiate(BossPrefab[index]);
        boss.transform.position = new Vector2(20, -0.4f);
        ChangeBackground();
    }
    private IEnumerator PopupStageNum_Coroutine()
    {
        //yield return new WaitForSeconds(2.5f);
        PopupStageNum.gameObject.SetActive (true);
        yield return new WaitForSeconds(3);
        PopupStageNum.gameObject.SetActive (false);
    }

    private void ChangeBackground()
    {
        if (StageNum == 0)
        {
            return;
        }
        //int num = StageNum % Backgrounds.Length - 1;
        int num = (StageNum - 1) % Backgrounds.Length;
        if (num < 0)
        {
            num += Backgrounds.Length;
        }
        DisableBackGround();
        Backgrounds[num].SetActive(true);
        ChangeUIColor();
    }
    private void DisableBackGround()
    {
        foreach (GameObject g in Backgrounds)
        {
            g.SetActive (false);
        }
    }
    private void ChangeUIColor()
    {
        if (Backgrounds[0].activeInHierarchy)
        {
            UI_Stat.color = new Color32(171, 148, 122, 255);
        }
        else if (Backgrounds[1].activeInHierarchy)
        {
            UI_Stat.color = new Color32(151, 107, 107, 255);
        }
        else if (Backgrounds[2].activeInHierarchy)
        {
            UI_Stat.color = new Color32(98, 85, 101, 255);
        }
        else if (Backgrounds[3].activeInHierarchy)
        {
            UI_Stat.color = new Color32(127, 112, 138, 255);
        }
    }
}
