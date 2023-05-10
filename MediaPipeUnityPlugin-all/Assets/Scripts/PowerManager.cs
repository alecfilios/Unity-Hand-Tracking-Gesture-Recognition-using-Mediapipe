using System.Collections;
using UnityEngine;

public enum PowerType
{
  Τelekinesis
}


public class PowerManager : MonoBehaviour
{
  public void StartPower(PowerType type)
  {
    if(type == PowerType.Τelekinesis)
    {
      GetComponent<Telekinesis>().StartPower();
    }
  }

  public void StopPower(PowerType type)
  {
    if (type == PowerType.Τelekinesis)
    {
      GetComponent<Telekinesis>().StopPower();
    }
  }
}
