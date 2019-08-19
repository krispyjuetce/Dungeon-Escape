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
    private Text _playerGemCountText;
    [SerializeField]
    private Image _selectionImage;
    [SerializeField]
    private Text _playerGemCountHudText;
    [SerializeField]
    private GameObject[] _healthUnit;
    [SerializeField]
    private Text[] _itemNamesText;
    [SerializeField]
    private Text[] _itemCostsText;

    public void UpdatePlayerGemCount(int playerGemCount)
    {
        _playerGemCountHudText.text = playerGemCount.ToString();
        
        
    }

    public void UpdatePlayerShopGemCount(int playerGemCount)
    {
        _playerGemCountText.text = playerGemCount.ToString() + "G";
    }

    public void OpenShop(int playerGemCount,Item[] _items)
    {
        _playerGemCountText.text = playerGemCount.ToString() + "G";

        for(int i = 0; i < _items.Length; i++)
        {
            _itemNamesText[i].text = _items[i].itemName;
            _itemCostsText[i].text = _items[i].itemCost.ToString()+"G";
        }
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
        if (livesRemaining >= 0)
        {
            _healthUnit[livesRemaining].SetActive(false);
        }

    }
}
