using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  public float turnSpeed = 5f;

  private bool turning = false;
  private bool turnRight = true;

  /*
   * --------------------------------------------------------------------------------------------------------------------------------
   *                                              Horizontal
   * --------------------------------------------------------------------------------------------------------------------------------
   */

  public void StartTurning(bool turnRight)
  {
    // Set the turning flag and the turning direction
    this.turning = true;
    this.turnRight = turnRight;
  }

  public void StopTurning()
  {
    // Reset the turning flag
    this.turning = false;
  }

  /*
   * --------------------------------------------------------------------------------------------------------------------------------
   *                                              Vertical
   * --------------------------------------------------------------------------------------------------------------------------------
   */
  // Vertical
  [SerializeField] private float tiltSpeed = 45f; // degrees per second
  private bool _tilting = false;
  private bool _tiltUp = true;

  public void StartTilting(bool tiltUp)
  {
    // Set the tilting flag and the tilting direction
    this._tilting = true;
    this._tiltUp = tiltUp;
  }

  public void StopTilting()
  {
    // Reset the tilting flag
    this._tilting = false;
  }


  private void FixedUpdate()
  {
    float angleY = 0;
    float angleX = 0;

    if (turning)
    {
      // Calculate the rotation angle and direction
      angleY = turnSpeed * Time.fixedDeltaTime * (turnRight ? 1f : -1f);
    }

    if (_tilting && false)
    {
      // Calculate the rotation angle and direction
      angleX = tiltSpeed * Time.fixedDeltaTime * (_tiltUp ? 1f : -1f);
    }
    // Apply the rotation on global axes
    transform.eulerAngles = new Vector3(transform.eulerAngles.x + angleX, transform.eulerAngles.y + angleY, transform.eulerAngles.z);
    
  }

  //return angle in range -180 to 180
  float NormalizeAngle(float a)
  {
    return a - 180f * Mathf.Floor((a + 180f) / 180f);
  }


}
