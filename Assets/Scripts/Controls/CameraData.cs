using Cinemachine;
using UnityEngine;

namespace Controls {
  public class CameraData : MonoBehaviour {
    [SerializeField]
    private CinemachineFreeLook _cinemachineFreeLook;
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private CinemachineFreeLook.Orbit [] _originalOrbits = new CinemachineFreeLook.Orbit[0];
    [SerializeField, Range(0.01f, 3f)]
    private float _minScale = 0.5f;
    [SerializeField, Range(1F, 10f)]
    private float _maxScale = 1;
    [SerializeField]
    private float _zoomSensitivity = 1;

    public CinemachineFreeLook CinemachineFreeLook => _cinemachineFreeLook;
    public Transform Target => _target;
    public float MinScale => _minScale;
    public float MaxScale => _maxScale;
    public float ZoomSensitivity => _zoomSensitivity;
    public CinemachineFreeLook.Orbit [] OriginalOrbits { get { return _originalOrbits; } set {} }
  }
}