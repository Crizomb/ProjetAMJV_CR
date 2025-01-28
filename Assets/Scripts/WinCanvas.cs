using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinCanvas : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI time;
    [SerializeField] GameObject gameUI;

    void Start()
    {
        time.text = gameUI.GetComponent<GameUI>().time.ToString();
    }


    public void NextLevel()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
