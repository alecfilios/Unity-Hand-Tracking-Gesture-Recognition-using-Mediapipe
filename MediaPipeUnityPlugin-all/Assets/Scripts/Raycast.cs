using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
  bool _loaded;
  Vector3 _startPos;
  Vector3 _direction;
  Transform _wristTransform;
  Transform _middleFingerMcpTransform;

  public float RayDistance = 100f;
    // Update is called once per frame
  void Update()
  {
    if (!_loaded)
    {
      return;
    }
    _startPos = _wristTransform.position;
    _direction = (_middleFingerMcpTransform.position - _startPos).normalized;

    bool rayCheck = Physics.Raycast(_startPos, _direction, out RaycastHit hitInfo, RayDistance);
    if (rayCheck)
    {
      Debug.Log("Hit Something..!");
      
    }
    else
    {
      Debug.Log("Hit Nothing...");
    }
    Debug.DrawRay(_startPos, _direction * RayDistance, Color.magenta, 0, false);

  }

  public void LoadData( Transform wristTransform, Transform middleFingerMcpTransform)
  {
    _wristTransform = wristTransform;
    _middleFingerMcpTransform = middleFingerMcpTransform;
    _loaded = true;
  }
}
