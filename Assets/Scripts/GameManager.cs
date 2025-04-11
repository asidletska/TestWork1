using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject loseUI;
    public Text killCounterText;

    private int killCount = 0;
    private bool gameEnded = false;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        killCount = 0;
        UpdateKillUI();
        loseUI.SetActive(false);
    }

    public void AddKill()
    {
        killCount++;
        UpdateKillUI();
    }

    void UpdateKillUI()
    {
        killCounterText.text = "Kills: " + killCount;
    }

    public void OnPlayerKilled()
    {
        if (gameEnded) return;

        gameEnded = true;
        loseUI.SetActive(true);
    }


}
