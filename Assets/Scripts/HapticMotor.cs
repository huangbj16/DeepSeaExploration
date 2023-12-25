using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using System;

[Serializable]
public class HapticMotor : MonoBehaviour
{

  public bool isTriggered = false;

  private float endTime;

  public int intensity = 1;

  private VisualEffect visualEffect;

  public int flag = 0;

  private GameObject senderObject;
  private TcpSender sender;
  public Dictionary<string, int> command;

  public int motor_id = 0;

  private Material highlightMaterial;
  private Material defaultMaterial;


    void Start()
  {
    visualEffect = GetComponent<VisualEffect>();
    senderObject = GameObject.Find("TCPSenderObject");
    sender = senderObject.GetComponent<TcpSender>();
    highlightMaterial = Resources.Load<Material>("Materials/Material_Red");
    defaultMaterial = GetComponent<Renderer>().material;
    command = new Dictionary<string, int>()
        {
            { "addr", motor_id },
            { "mode", 0 },
            { "duty", 15 },
            { "freq", 2 },
            { "wave", 0 }
        };
  }

  public string DictionaryToString(Dictionary<string, int> dictionary)
  {
    string dictionaryString = "{";
    foreach (KeyValuePair<string, int> keyValues in dictionary)
    {
      dictionaryString += "\"" + keyValues.Key + "\": " + keyValues.Value + ", ";
    }
    return dictionaryString.TrimEnd(',', ' ') + "}";
  }

  public void StartVibrate(string other)
  {
    isTriggered = true;
    GetComponent<Renderer>().material = highlightMaterial;
    // send TCP command
    Debug.Log("Send start command to PORT:9051");
    command["mode"] = 1;
    string commandString = DictionaryToString(command) + "\n";
    Debug.Log(commandString);
    sender.SendData(commandString);
  }

  public void Vibrate()
  {
    // trigger some animation.
    
  }

  public void StopVibrate(string other, string detail)
  {
    GetComponent<Renderer>().material = defaultMaterial;
    //send TCP command
    Debug.Log("Send stop command to PORT:9051");
    command["mode"] = 0;
    string commandString = DictionaryToString(command) + "\n";
    Debug.Log(commandString);
    sender.SendData(commandString);
    sender.SendData(commandString);
  }


  // Update is called once per frame
  void Update()
  {
        /*
    if (isTriggered)
    {
      Vibrate();
    }
        */
  }
    /*
  void OnTriggerEnter(Collider other)
  {
    // TODO: check if is haptic mode

    if (other.transform.parent && other.transform.parent.GetComponent<ShapeObject>() != null)
    {
      Debug.Log("collision starts with " + other.transform.parent.parent.name);
      StartVibrate(other.transform.parent.parent.name);
    }
  }

  void OnTriggerExit(Collider other)

  {

   // TODO: check if is haptic mode

        Debug.Log("exit");
    if (other.transform.parent && other.transform.parent.GetComponent<ShapeObject>() != null)
    {

      Debug.Log("collision ends with " + other.transform.parent.parent.name);
      StopVibrate(other.transform.parent.parent.name, other.transform.name);
    }

  }
    */
}
