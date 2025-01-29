using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI time;
    private GameUI gameUI;

    void Start()
    {
        gameUI = GameObject.FindWithTag("GameUI").GetComponent<GameUI>();
        time.text = gameUI.GetComponent<GameUI>().time.ToString();
    }


    public void NextLevel()
    {
        GameManager.Instance.GoNextLevel();
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
