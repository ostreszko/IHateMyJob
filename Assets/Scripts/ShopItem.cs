using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem : MonoBehaviour, IInventoryItem
{
    public string ObjectTag => gameObject.tag;

    public Sprite _Image = null;
    public Sprite Image => _Image;
}
