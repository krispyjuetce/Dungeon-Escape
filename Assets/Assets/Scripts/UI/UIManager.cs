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
        Debug.Log("On awake instance created for UIManager");
        _instance = this;
    }
    [SerializeField]
    private Text _playerGemCountText;
    [SerializeField]
    private Image _selectionImage;
    [SerializeField]
    private Text _playerGemCountHudText;
    [SerializeField]
    private GameObject[] _healthUnit;
    [SerializeField]
    private Text[] _itemNames;
    [SerializeField]
    private Text[] _itemCosts;

    public void UpdatePlayerGemCount(int playerGemCount)
    {
        _playerGemCountHudText.text = playerGemCount.ToString();
        
        
    }

    public void UpdatePlayerShopGemCount(int playerGemCount)
    {
        _playerGemCountText.text = playerGemCount.ToString() + "G";
    }

    public void OpenShop(int playerGemCount)
    {
        _playerGemCountText.text = playerGemCount.ToString() + "G";
        _itemNames[0].text = "Flame Sword";
        _itemNames[1].text = "Boots of Flight";
        _itemNames[2].text = "Key to Castle";

        _itemCosts[0].text = "20";
        _itemCosts[1].text = "30";
        _itemCosts[2].text = "10";

    }

    public void UpdateSelection(int item_id)
    {
        switch (item_id)
        {
            case 1:
                _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, 77f);//bad code to hard code this. change later
                break;
            case 2:
                _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, -39f);
                break;
            case 3:
                _selectionImage.rectTransform.anchoredPosition = new Vector2(_selectionImage.rectTransform.anchoredPosition.x, -146f);
                break;
        }
        
    }

    public void UpdateLives(int livesRemaining)
    {
        Debug.Log("Inside Update Lives");
        
        if (livesRemaining >= 0)
        {
            _healthUnit[livesRemaining].SetActive(false);
        }

    }
}
