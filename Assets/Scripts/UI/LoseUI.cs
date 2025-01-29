using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    [SerializeField] private GameUI gameUI;

    [SerializeField] TextMeshProUGUI time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time.text = gameUI.GetComponent<GameUI>().time.ToString();
    }

    public void Retry()
    {
        GameManager.Instance.ReloadLevel();
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
