using UnityEngine;
using TMPro;
using YG;
using UnityEngine.SceneManagement;

public class ManagerUI : MonoBehaviour
{

    public int language;
    [SerializeField] private LeaderboardYG leaderboard;
    [SerializeField] private GameObject LeaderBoard;

    public int Money;

    private int Score;
    private int record;
    private bool _active;

    [SerializeField] private TextMeshProUGUI textMoney;
    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textRecord;

    int count = 0;
    private void Awake()
    {
        LoadSaveCloud();
    }
    private void Start()
    {
        Money = 2;

        if (language == 0)
        {
            textMoney.text = "Монеты: " + Money;
        }

        if (language == 1)
        {
            textMoney.text = "Coin: " + Money;
        }
    }
    private void OnEnable()
    {
        if (YandexGame.SDKEnabled == true)
        {
            LoadSaveCloud();
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            LeaderBoard.SetActive(!_active);

            _active = !_active;

            if (_active)
            {
                Time.timeScale = 0;

                if (Score >= record)
                {
                    MySave();
                }
            }
            else
                Time.timeScale = 1;
        }
    }
    public void AddMoney()
    {
        count++;
        Money += 1;

        if (count == 10)
        {
            count = 0;
            Money += 1;
        }
        if (language == 0)
        {
            textMoney.text = "Монеты: " + Money;
        }
        if (language == 1)
        {
            textMoney.text = "Coin: " + Money;
        }
    }
    public void RemoveMovey(int prise)
    {
        Money -= prise;

        if (language == 0)
        {
            textMoney.text = "Монеты: " + Money;
        }

        if (language == 1)
        {
            textMoney.text = "Coin: " + Money;
        }

    }
    public void AddScore(int score)
    {
        Score += score;

        if (Score > record)
        {
            record = Score;

            if (language == 0)
            {
                textRecord.text = "Рекорд: " + record.ToString();
            }
            if (language == 1)
            {
                textRecord.text = "Record: " + record.ToString();
            }

        }

        textScore.text = Score.ToString();
    }
    public void LoadSaveCloud()
    {
        language = YandexGame.savesData.lg;
        record = YandexGame.savesData.record;       

        if (language == 0)
        {
            textRecord.text = "Рекорд: " + record.ToString();
        }

        if (language == 1)
        {
            textRecord.text = "Record: " + record.ToString();
        }
    }

    public void MySave()
    {
        if (Score >= record)
        {
            record = Score;

            YandexGame.savesData.record = record;
            YandexGame.NewLeaderboardScores("leader", record);
            YandexGame.SaveProgress();
            if (language == 0)
            {
                textRecord.text = "Рекорд: " + record.ToString();
            }
            if (language == 1)
            {
                textRecord.text = "Record: " + record.ToString();
            }

        }
    }
    public void RussianLg()
    {
        language = 0;
        YandexGame.savesData.lg = language;
        SceneManager.LoadScene(0);
    }
    public void EnglishLg()
    {
        language = 1;
        YandexGame.savesData.lg = language;
        SceneManager.LoadScene(0);
    }

    public void ResetYn()
    {
            YandexGame.savesData.record = 0;
        SceneManager.LoadScene(0);

    }
}

