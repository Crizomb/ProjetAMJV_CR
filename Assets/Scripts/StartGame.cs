using UnityEngine;

public class StartGame : MonoBehaviour
{

    [SerializeField] GameObject ShopUI;
    [SerializeField] GameObject GameUI;

    public void Starting()
    {
        GameManager.Instance.StartFightForAll();
        GameUI.SetActive(true);
        GameUI.GetComponent<GameUI>().timerActive = true;
        ShopUI.SetActive(false);
    }

}
