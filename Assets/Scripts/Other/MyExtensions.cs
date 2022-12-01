using Cinemachine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Other {
  public static class MyExtensions {
    public static void AddListener (this Button button, UnityAction action) {
      button.onClick.AddListener(action);
    }

    public static void AddListener (this Toggle toggle, UnityAction<bool> action) {
      toggle.onValueChanged.AddListener(action);
    }

    public static void AddListener (this TMP_InputField inputField, UnityAction<string> action) {
      inputField.onValueChanged.AddListener(action);
    }

    public static void SetAxisValues (this CinemachineFreeLook cinemachine, float valueX, float valueY) {
      cinemachine.m_XAxis.m_InputAxisValue = valueX;
      cinemachine.m_YAxis.m_InputAxisValue = valueY;
    }

    public static void SetAxisNames (this CinemachineFreeLook cinemachine, string xAxis, string yAxis) {
      cinemachine.m_XAxis.m_InputAxisName = xAxis;
      cinemachine.m_YAxis.m_InputAxisName = yAxis;
    }

    public static float Normalize (this float value) {
      return value switch {
               0 => 0,
               >0 => 1,
               <0 => -1,
               _ => 0
             };
    }
  }
}