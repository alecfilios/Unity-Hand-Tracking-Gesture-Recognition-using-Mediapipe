using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
  [SerializeField]
  PowerManager _powerManager;

  [SerializeField]
  HandManager _handManager;

  public void StartPower(PowerType power)
  {
    switch (power)
    {
      case PowerType.Τelekinesis:
        _powerManager.StartPower(PowerType.Τelekinesis);
        break;

      default:
        break;
    }
  }
  public void StopPower(PowerType power)
  {
    //_powerManager.StopPower(power);
  }
}
