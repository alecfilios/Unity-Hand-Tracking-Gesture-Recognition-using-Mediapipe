using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Telekinesis : Power
{
  [SerializeField]
  GameObject itemParent;

  [SerializeField]
  float floatHeight = 2f;

  [SerializeField]
  float floatSpeed = 0.2f;

  [SerializeField]
  float rotationSpeed = 30f;

  [SerializeField]
  float duration = 5f;

  [SerializeField]
  bool isFloating = false;

  [SerializeField]
  float elapsedDuration = 0f;

  [SerializeField]
  List<Transform> itemTransforms = new List<Transform>();

  [SerializeField]
  float levitationRange = 0.001f;

  [SerializeField]
  Material itemMaterial;


  void Start()
  {
    itemMaterial.SetColor("_EmissionColor", new Vector4(0.0003293505f, 0.000225949756f, 0.000731464475f, -0.400000006f));
    foreach (Transform child in itemParent.transform)
    {
      itemTransforms.Add(child);
    }
  }

  public override void StartPower()
  {

    if (!isFloating)
    {
      isFloating = true;
      StartCoroutine(FloatItemsSmoothly());
    }
  }

  void GravityTrigger(bool trigger)
  {
    // Disable gravity for all items
    foreach (Transform item in itemTransforms)
    {
      Rigidbody rigidbody = item.GetComponent<Rigidbody>();
      if (rigidbody != null)
      {
        rigidbody.useGravity = trigger;
      }
    }
  }


  // Define the start and end colors
  public Color color1 = new Vector4(0.892080545f, 0.616352081f, 2f, -0.400000006f);
  public Color color2 = new Vector4(0.0003293505f, 0.000225949756f, 0.000731464475f, -0.400000006f);
  IEnumerator ChangeMaterialEmissionColor(bool trigger)
  {
    Color startColor, endColor;



    if (trigger)
    {
      startColor = color1;
      endColor = color2;
    }
    else
    {
      startColor = color2;
      endColor = color1;
    }

    // Define the duration over which to interpolate the color
    float colorDuration = 2f; // or any other value you want

    // Store the current time
    float startTime = Time.time;

    while (Time.time - startTime < colorDuration)
    {
      // Calculate the current color based on the current time and duration
      float t = (Time.time - startTime) / colorDuration;
      Color lerpedColor = Color.Lerp(startColor, endColor, t);

      // Set the material's emission color to the current color
      itemMaterial.SetColor("_EmissionColor", lerpedColor);

      yield return null;
    }
  }
  Dictionary<GameObject, float> targetHeight = new Dictionary<GameObject, float>();
  IEnumerator FloatItemsSmoothly()
  {
    GravityTrigger(false);

    StartCoroutine(ChangeMaterialEmissionColor(false));

    // Set a timer for floating duration
    float floatingDuration = 5f; // Change this to the desired duration in seconds
    float elapsedTime = 0f;

    // Set a different target height for each item
    foreach (Transform child in itemTransforms)
    {
      GameObject item = child.gameObject;
      targetHeight[item] = floatHeight + Random.Range(0, 10f);
      
    }
    // Reach that height
    while (elapsedTime < floatingDuration)
    {
      elapsedTime += Time.deltaTime;

      // Float items smoothly by lerping their positions
      foreach (Transform item in itemTransforms)
      {

         item.position = Vector3.Lerp(item.position, new Vector3(item.position.x, targetHeight[item.gameObject], item.position.z), floatSpeed * Time.deltaTime);
        
      }

      yield return null;
    }

    // Start levitating and orbiting the items
    StartCoroutine(LevitateAndOrbitItems());

    // Start the duration timer
    elapsedDuration = 0f;
    while (elapsedDuration < duration)
    {
      elapsedDuration += Time.deltaTime;
      yield return null;
    }

    // Stop the power when the duration ends
    StopPower();
  }




  IEnumerator LevitateAndOrbitItems()
  {
    // Levitate and orbit the items asynchronously
    while (isFloating)
    {
      for (int i = 0; i < itemTransforms.Count; i++)
      {
        Transform item = itemTransforms[i];

        // Levitate the item up and down
        Vector3 positionOffset = new Vector3(0f, Mathf.Sin(Time.time + i) * levitationRange, 0f);
        item.position += positionOffset;

        // Orbit the item around itself
        //item.rotation *= Quaternion.AngleAxis(rotationSpeed * Time.deltaTime, item.up);
      }

      yield return null;
    }
  }



  public override void StopPower()
  {
    isFloating = false;
    StopCoroutine(LevitateAndOrbitItems());
    StartCoroutine(ChangeMaterialEmissionColor(true));
    // Restore gravity for all items
    foreach (Transform item in itemTransforms)
    {
      Rigidbody rigidbody = item.GetComponent<Rigidbody>();
      if (rigidbody != null)
      {
        rigidbody.useGravity = true;
      }
    }
  }

}
