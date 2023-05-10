using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HandManager : MonoBehaviour
{
  private Camera _mainCamera;

  [SerializeField]
  PlayerController _playerController;
  
  [SerializeField]
  private GameObject _handLandmarks;

  [SerializeField]
  private Color _rightHandColor;
  [SerializeField]
  private Color _leftHandColor;

  private GameObject _rightHandLandmarks;
  private GameObject _leftHandLandmarks;

  [SerializeField]
  Hand _rightHand;
  [SerializeField]
  Hand _leftHand;

  [SerializeField]
  TextFader _rightHandTextFader;

  [SerializeField]
  TextFader _leftHandTextFader;

  private void Awake()
  {
    _mainCamera = Camera.main;
  }

  // Start is called before the first frame update
  private void Start()
  {
    _handLandmarks.GetComponent<Mediapipe.Unity.MultiHandLandmarkListAnnotation>().SetRightLandmarkColor(_rightHandColor);
    _handLandmarks.GetComponent<Mediapipe.Unity.MultiHandLandmarkListAnnotation>().SetLeftLandmarkColor(_leftHandColor);
  }

  // Update is called once per frame
  private void Update()
  {
    DetectHands();
    if (_loaded)
    {
      CheckHandPosition();
    }
  }

  void CheckHandPosition()
  {
    if (_loaded)
    {
      GameObject rightWrist = _rightHandLandmarks.transform.GetChild(0).gameObject;
      GameObject leftWrist = _leftHandLandmarks.transform.GetChild(0).gameObject;

      // Horizontal
      if (IsOnRightSide(rightWrist) && IsOnRightSide(leftWrist))
      {
        _playerController.StartTurning(true);
      }
      else if(!IsOnRightSide(rightWrist) && !IsOnRightSide(leftWrist))
      {
        _playerController.StartTurning(false);
      }
      else
      {
        _playerController.StopTurning();
      }

      // Vertical
      if (IsOnTopSide(rightWrist) && IsOnTopSide(leftWrist))
      {
        _playerController.StartTilting(false);
      }
      else if (!IsOnTopSide(rightWrist) && !IsOnTopSide(leftWrist))
      {
        _playerController.StartTilting(true);
      }
      else
      {
        _playerController.StopTilting();
      }
    }
  }

  public bool IsOnRightSide(GameObject obj)
  {
    Vector3 screenPos = _mainCamera.WorldToScreenPoint(obj.transform.position);
    return screenPos.x > Screen.width / 2f;
  }

  public bool IsOnTopSide(GameObject obj)
  {
    Vector3 screenPos = _mainCamera.WorldToScreenPoint(obj.transform.position);
    return screenPos.y > Screen.height / 2f;
  }

  private void DetectHands()
  {
    /*
     * Case A: Both hands are missing:
     * 1) The parent is not active at all
     * 2) The number of children is 0
     * 3) The first child is not active (which indicates that the second is inactive as well)
     */
    if (!_handLandmarks.gameObject.activeSelf || _handLandmarks.transform.childCount == 0 || !_handLandmarks.transform.GetChild(0).gameObject.activeSelf)
    {
      _loaded = false;
      // Wait for both hands to enter
      //Debug.Log("Both hands are not detected: Raise your hands..!");
      //TODO: make a UI message about it
      _rightHandTextFader.isPeriodicallyFading = true;
      _leftHandTextFader.isPeriodicallyFading =true;
    }
    /*
     * Case B: One hand is missing:
     * 1) The number of children is 1
     * 2) The second child is not active
     */
    else if (_handLandmarks.transform.childCount == 1 || !_handLandmarks.transform.GetChild(1).gameObject.activeSelf)
    {
      _loaded = false;
      // Check which hand is detected
      if (IsRightHand())
      {
        // Notify about the other one
        _rightHandTextFader.isPeriodicallyFading = false;
        _rightHandTextFader.CallFadeOut();
        _leftHandTextFader.isPeriodicallyFading = true;
      }
      else
      {
        _leftHandTextFader.isPeriodicallyFading = false;
        _leftHandTextFader.CallFadeOut();
        _rightHandTextFader.isPeriodicallyFading = true;
      }
    }
    /*
     * Case C: Both hands are detected:
     * 1) The parent is not active at all
     * 2) The number of children is 0
     * 3) The first child is not active (which indicates that the second is inactive as well)
     */
    else
    {
      if (!_loaded)
      {
        _leftHandTextFader.isPeriodicallyFading = false;
        _leftHandTextFader.CallFadeOut();
        _rightHandTextFader.isPeriodicallyFading = false;
        _rightHandTextFader.CallFadeOut();
        CheckWhichHandIsWhich();
      }
        
    }
  }

  private bool IsRightHand()
  {
    Color color = _handLandmarks.transform.GetChild(0).GetChild(0).GetComponent<Mediapipe.Unity.PointListAnnotation>().GetColor();
    if (color == _rightHandColor)
    {
      return true;
    }
    return false;
  }

  bool _loaded;
  private void CheckWhichHandIsWhich()
  {
    // In case the first child is the right hand
    if(IsRightHand())
    {
      _rightHandLandmarks = _handLandmarks.transform.GetChild(0).GetChild(0).gameObject;
      _leftHandLandmarks = _handLandmarks.transform.GetChild(1).GetChild(0).gameObject;

    }
    // In case the second child is the right hand
    else
    {
      _rightHandLandmarks = _handLandmarks.transform.GetChild(1).GetChild(0).gameObject;
      _leftHandLandmarks = _handLandmarks.transform.GetChild(0).GetChild(0).gameObject;
    }
    // Load the landmarks
    _rightHand.LoadKnucklesList(_rightHandLandmarks);
    _leftHand.LoadKnucklesList(_leftHandLandmarks);

    _loaded = true;
  }


}
