using System;
using TMPro;
using UnityEngine;

namespace UI.Suites {
  public class SuitePropertyBlock : MonoBehaviour {
    [SerializeField]
    private TMP_Text _propertyName;
    [SerializeField]
    private TMP_Text _propertyCountText;

    public void SetupBlock (SuitePropertyData suitePropertyData) {
      _propertyName.name = suitePropertyData.PropertyName;
      _propertyCountText.text = suitePropertyData.Count.ToString();
    }
  }

  [Serializable]
  public class SuitePropertyData {
    public string PropertyName;
    public int Count;
  }
}