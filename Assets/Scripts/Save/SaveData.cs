using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SaveData
{
public int[] cosmeticsOwned;
public int currencyCount;
public SaveData (CosmeticsCatalogue cosmetics)
{
    cosmeticsOwned = cosmetics.cosmeticsOwned;
}
public SaveData (CurrencyTracker currency)
{
    currencyCount = currency.ReturnCurrencyCount();
}
}
