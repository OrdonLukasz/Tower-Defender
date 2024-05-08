using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoSingleton<ShopManager>
{
    public static ShopManager _Instance { get; private set; }
    
    [SerializeField] private Text cashText;

    private float currentCash = 100f;

    public void AddCash(float earnedCash)
    {
        currentCash += earnedCash;
        cashText.text = $"{currentCash}$";
    }

    public void SubtractCash(float turretCost)
    {
        currentCash -= turretCost;
        cashText.text = $"{currentCash}$";
    }

    public float getCurrentCash()
    {
        return currentCash;
    }
}
