using UnityEngine;
using System.Collections;

public class Settings : MonoBehaviour {

    public enum ControlType
    {
        KeyboardAndMouse,
        Controller
    };


    public ControlType controlType = ControlType.KeyboardAndMouse;
}
