using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestureManager : MonoBehaviour
{
  [SerializeField]
  Hand _rightHand;

  [SerializeField]
  Hand _leftHand;

  GameManager _gameManager;

  private void Awake()
  {
    _gameManager = FindAnyObjectByType<GameManager>();
  }

  private void Update()
  {
    // If both hands have their fist closed
    if(_rightHand.GetGesture() == GestureType.ClosedFist && _leftHand.GetGesture() == GestureType.ClosedFist)
    {
      _gameManager.StartPower(PowerType.Τelekinesis);
    }
    else
    {
      _gameManager.StopPower(PowerType.Τelekinesis);
    }
  }

}
