using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyTracker : MonoBehaviour
{
int currency = 0;
SaveData currencyData;

void Awake() {
    currencyData = SaveSystem.LoadCurrencyData();
    SaveSystem.SaveCurrency(this);
    currency = currencyData.currencyCount;
}
public void UpdateCurrencyCount(int amount){
    currency = currency + amount;
    FindObjectOfType<GameManager>().UpdateCurrencyText(currency);
}
public int ReturnCurrencyCount(){
    return currency;
}
}
