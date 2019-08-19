using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    //set up values of shop items here as configurables and then populate the shop UI based on items
    [SerializeField] private GameObject _shopPanel;
    private int _selectedItem;
    private int _itemPrice;
    private Player _player;
    [SerializeField]
    private Item[] _items;

    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _player = collision.GetComponent<Player>();

        if (_player != null)
        {
            UIManager.Instance.OpenShop(_player.diamonds,_items);
        }
        _shopPanel.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _shopPanel.SetActive(false);
    }

    public void ClosePopUp()
    {
        _shopPanel.SetActive(false);
    }

    public void SelectItem(int item_id)
    {
        _selectedItem = item_id;
        UIManager.Instance.UpdateSelection(item_id);

        for(int i=0; i < _items.Length; i++)
        {
            if (item_id == _items[i].itemId)
            {
                _itemPrice = _items[i].itemCost;
            }
        }
        
    }

    public void BuyItem()
    {
        
        if (_player.diamonds >= _itemPrice)
        {
            switch (_selectedItem)
            {
                case 1:
                    GameManager.Instance.HasFlameSword = true;
                    break;
                case 2:
                    GameManager.Instance.HasBootsOfFlight = true;
                    break;
                case 3:
                    GameManager.Instance.HasKeyToCastle = true;
                    break;
            }
            Debug.Log("Item bought with price : " + _itemPrice);
            _player.diamonds = _player.diamonds - _itemPrice;
            UIManager.Instance.UpdatePlayerGemCount(_player.diamonds);
            UIManager.Instance.UpdatePlayerShopGemCount(_player.diamonds);
            Debug.Log("Item Purchased: " + _selectedItem+" balance gems : "+_player.diamonds);
        }
        else
        {
            Debug.Log("CAT");
        }
    }
}
