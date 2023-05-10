using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GestureType
{
  None,
  ClosedFist
}

public class Hand : MonoBehaviour
{
  public enum HandType
  {
    Right,
    Left
  }

  const int WRIST = 0;
  const int THUMB_CMC = 1;
  const int THUMB_MCP = 2;
  const int THUMB_IP = 3;
  const int THUMB_TIP = 4;
  const int INDEX_FINGER_MCP = 5;
  const int INDEX_FINGER_PIP = 6;
  const int INDEX_FINGER_DIP = 7;
  const int INDEX_FINGER_TIP = 8;
  const int MIDDLE_FINGER_MCP = 9;
  const int MIDDLE_FINGER_PIP = 10;
  const int MIDDLE_FINGER_DIP = 11;
  const int MIDDLE_FINGER_TIP = 12;
  const int RING_FINGER_MCP = 13;
  const int RING_FINGER_PIP = 14;
  const int RING_FINGER_DIP = 15;
  const int RING_FINGER_TIP = 16;
  const int PINKY_MCP = 17;
  const int PINKY_PIP = 18;
  const int PINKY_DIP = 19;
  const int PINKY_TIP = 20;

  [SerializeField]
  GestureType _gestureType;

  [SerializeField]
  HandType _handType;

  [Range(0f, 1f)]
  public float[] fingerThresholds = new float[5] { 0.6f, 0.6f, 0.6f, 0.6f, 0.6f };

  private void Update()
  {
    if (_knucklesList == null)
    {
      return;
    }
    if (CheckClosedFistGesture())
    {
      _gestureType = GestureType.ClosedFist;
      GestureDebugger("Closed Fist");
    }
    else
    {
      _gestureType = GestureType.None;
      //GestureDebugger("No gesture :(..");
    }
  }

  public GestureType GetGesture()
  {
    return _gestureType;
  }

  void GestureDebugger(string gesture)
  {
    Debug.Log("[" + _handType.ToString() + " Hand]: " + gesture + "!");
  }

  public bool thunb;
  public bool index;
  public bool middle;
  public bool ring;
  public bool pinky;

  public float thumbFingerTipDistance;
  public float indexFingerTipDistance;
  public float middleFingerTipDistance;
  public float ringFingerTipDistance; 
  public float pinkyFingerTipDistance;

  private bool CheckClosedFistGesture()
  {
    // Calculate the distance between the thumb and pinky MCP joints
    float thumbToPinkyDistance = Vector3.Distance(_knucklesList[THUMB_MCP].localPosition, _knucklesList[PINKY_MCP].localPosition);

    // Normalize the fingertip distances by dividing them by the thumb-to-pinky distance
    thumbFingerTipDistance = Vector3.Distance(_knucklesList[THUMB_TIP].localPosition, _knucklesList[PINKY_DIP].localPosition);
    indexFingerTipDistance = Vector3.Distance(_knucklesList[INDEX_FINGER_TIP].localPosition, _knucklesList[INDEX_FINGER_MCP].localPosition);
    middleFingerTipDistance = Vector3.Distance(_knucklesList[MIDDLE_FINGER_TIP].localPosition, _knucklesList[MIDDLE_FINGER_MCP].localPosition);
    ringFingerTipDistance = Vector3.Distance(_knucklesList[RING_FINGER_TIP].localPosition, _knucklesList[RING_FINGER_MCP].localPosition);
    pinkyFingerTipDistance = Vector3.Distance(_knucklesList[PINKY_TIP].localPosition, _knucklesList[PINKY_MCP].localPosition);

    thumbFingerTipDistance  /= thumbToPinkyDistance;
    indexFingerTipDistance  /= thumbToPinkyDistance;
    middleFingerTipDistance /= thumbToPinkyDistance;
    ringFingerTipDistance   /= thumbToPinkyDistance;
    pinkyFingerTipDistance  /= thumbToPinkyDistance;

    thunb = false;
    index = false;
    middle = false;
    ring = false;
    pinky = false;

    if (thumbFingerTipDistance < fingerThresholds[0])
    {
      thunb = true;
    }
    if( indexFingerTipDistance < fingerThresholds[1])
    {
      index = true;
    }
    if( middleFingerTipDistance < fingerThresholds[2])
    {
      middle = true;
    }
    if( ringFingerTipDistance < fingerThresholds[3])
    {
      ring = true;
    }
    if( pinkyFingerTipDistance < fingerThresholds[4])
    {
      pinky = true;
    }
    
    // Check if all the normalized distances are less than the corresponding threshold value for each finger
    if (thumbFingerTipDistance < fingerThresholds[0] &&
      indexFingerTipDistance < fingerThresholds[1] &&
      middleFingerTipDistance < fingerThresholds[2] &&
      ringFingerTipDistance < fingerThresholds[3] &&
      pinkyFingerTipDistance < fingerThresholds[4])
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  private List<Transform> _knucklesList;

  public void LoadKnucklesList(GameObject knucklesList)
  {
    _knucklesList = new List<Transform>();
    foreach (Transform knuckle in knucklesList.transform)
    {
      _knucklesList.Add(knuckle);
    }
    Debug.Log('[' + _handType.ToString() + "]:" + _knucklesList.Count + " knuckles.");
  }

}
