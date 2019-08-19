using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    private static UIManager _instance;
    public static UIManager Instance
    {
        get
        {
            if (_instance == null)
            {
                throw new UnityException("_instance is null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    [SerializeField]
    private Text playerGemCountText;
    [SerializeField]
    private Image selectionImage;
    [SerializeField]
    private Text playerGemCountHudText;
    [SerializeField]
    private GameObject[] healthUnit;
    

    public void UpdatePlayerGemCount(int playerGemCount)
    {
        playerGemCountHudText.text = playerGemCount.ToString();
    }

    public void OpenShop(int playerGemCount)
    {
        playerGemCountText.text = playerGemCount.ToString() + "G";
        
    }

    public void UpdateSelection(int item_id)
    {
        switch (item_id)
        {
            case 1:
                selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, 77f);//bad code to hard code this. change later
                break;
            case 2:
                selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, -39f);
                break;
            case 3:
                selectionImage.rectTransform.anchoredPosition = new Vector2(selectionImage.rectTransform.anchoredPosition.x, -146f);
                break;
        }
        
    }

    public void UpdateLives(int livesRemaining)
    {
        Debug.Log("Inside Update Lives");
        
        if (livesRemaining >= 0)
        {
            healthUnit[livesRemaining].SetActive(false);
        }

    }
}
