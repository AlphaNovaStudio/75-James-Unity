using System;
using Abstract;
using Cinemachine;
using Other;
using StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Controls {
  public class CameraZoomBehaviour : BaseBehaviour {
    private readonly CameraData _cameraData;
    private readonly StateMachine<Enums.CameraControlType> _stateMachine;

    private readonly Camera _camera;
    private readonly float _sensitivity;
    private float _zoom = 0.5f;

    public CameraZoomBehaviour (CameraData data, StateMachine<Enums.CameraControlType> stateMachine) {
      _camera = Camera.main;
      _sensitivity = data.ZoomSensitivity;
      _cameraData = data;
      _stateMachine = stateMachine;
      SubscribeEvents();
      SetZoom();
    }

    public override void Update() {
      if (IsPaused) {
        return;
      }

      Zoom();
    }

    public override void FixedUpdate() {}

    public override void OnDestroy() {
      SubscribeEvents(false);
    }

    private float GetTouchZoom() {
      if (Input.touchCount != 2) {
        return 0;
      }

      Touch touchA = Input.GetTouch(0);
      Touch touchB = Input.GetTouch(1);
      Vector2 touchADirection = touchA.position - touchA.deltaPosition;
      Vector2 touchBDirection = touchB.position - touchB.deltaPosition;
      float dstBtwTouchesPos = Vector2.Distance(touchA.position, touchB.position);
      float dstBtwTouchesDirections = Vector2.Distance(touchADirection, touchBDirection);
      float zoom = dstBtwTouchesPos - dstBtwTouchesDirections;
      return zoom / _camera.pixelWidth;
    }

    private void SubscribeEvents (bool state = true) {
      if (state) {
        _stateMachine.OnChangeState += OnStateChange;
        return;
      }

      _stateMachine.OnChangeState -= OnStateChange;
    }

    private void OnStateChange (Enums.CameraControlType newType) {
      bool active = newType is Enums.CameraControlType.Full or Enums.CameraControlType.Zoom;

      if (!active) {
        SetPause();
      } else {
        Resume();
      }
    }

    private void Zoom() {
      if (_cameraData.CinemachineFreeLook == null || EventSystem.current.IsPointerOverGameObject()) {
        return;
      }

      if (_cameraData.OriginalOrbits.Length != _cameraData.CinemachineFreeLook.m_Orbits.Length) {
        _cameraData.OriginalOrbits = new CinemachineFreeLook.Orbit[_cameraData.CinemachineFreeLook.m_Orbits.Length];
        Array.Copy(_cameraData.CinemachineFreeLook.m_Orbits, _cameraData.OriginalOrbits, _cameraData.CinemachineFreeLook.m_Orbits.Length);
      }

      if (Input.mouseScrollDelta == Vector2.zero && Input.touchCount != 2) {
        return;
      }

      SetZoom();
    }

    private void SetZoom() {
      _zoom -= Input.mouseScrollDelta.y * Time.deltaTime * _sensitivity;
      _zoom -= GetTouchZoom();
      _zoom = Mathf.Clamp(_zoom, 0, 1);
      float scale = Mathf.Lerp(_cameraData.MinScale, _cameraData.MaxScale, _zoom);
      float minOrbitLength = Mathf.Min(_cameraData.OriginalOrbits.Length, _cameraData.CinemachineFreeLook.m_Orbits.Length);

      for (var i = 0; i < minOrbitLength; i++) {
        _cameraData.CinemachineFreeLook.m_Orbits[i].m_Height = _cameraData.OriginalOrbits[i].m_Height * scale;
        _cameraData.CinemachineFreeLook.m_Orbits[i].m_Radius = _cameraData.OriginalOrbits[i].m_Radius * scale;
      }
    }
  }
}