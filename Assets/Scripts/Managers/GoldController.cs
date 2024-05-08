using System;
using UnityEngine;

public class GoldController : MonoBehaviour
{
  public Action<int> OnGoldCountChanged;

  private int _totalGold; 

  public void AddGold(int value)
  {
    if (value < 0)
    {
      return;
    }
      
    _totalGold += value;
    SaveGold();
  }

  public int GetTotalGold()
  {
    return _totalGold;
  }
    
  public void RemoveGold(int value)
  {
    if (value > _totalGold)
    {
      return;
    }

    _totalGold -= value;
    SaveGold();
  }

  private void SaveGold()
  {
    PlayerPrefs.SetInt("Gold", _totalGold);
    PlayerPrefs.Save();
      
    OnGoldCountChanged?.Invoke(_totalGold);
  }
  
  public void LoadGold()
  {
    _totalGold = PlayerPrefs.GetInt("Gold", 0);

    OnGoldCountChanged?.Invoke(_totalGold);
  }
}