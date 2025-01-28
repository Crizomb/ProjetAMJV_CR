using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseUI : MonoBehaviour
{
    [SerializeField] GameUI gameUI;

    [SerializeField] TextMeshProUGUI time;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        time.text = gameUI.GetComponent<GameUI>().time.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Retry()
    {
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        SceneManager.LoadSceneAsync(0);
    }
}
