using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rules : MonoBehaviour
{

    public TMP_InputField money;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void SetMoney()
    {
        GlobalsVariable.money = int.Parse(money.text);
    }

    public void LaunchLevel(int level)
    {
        SceneManager.LoadSceneAsync(level);
    }
}
