using Abstract;
using Other;
using StateMachine;
using UnityEngine;

namespace Controls {
  public class CameraRotateBehaviour : BaseBehaviour {
    private const string X_AXIS = "Mouse X";
    private const string Y_AXIS = "Mouse Y";

    private readonly CameraData _cameraData;
    private readonly StateMachine<Enums.CameraControlType> _stateMachine;
    private bool _isStopped;

    private bool _isCanRotate;

    public CameraRotateBehaviour (CameraData data, StateMachine<Enums.CameraControlType> stateMachine) {
      _cameraData = data;
      _stateMachine = stateMachine;
      SetTarget(_cameraData.Target);
      InitCamera();
      SubscribeEvents();
    }

    public override void Update() {
      if (IsPaused) {
        return;
      }

      DetectFirstEmptyClick();
      Rotate();
    }

    public override void FixedUpdate() {}

    public override void OnDestroy() {
      SubscribeEvents(false);
    }

    private void SubscribeEvents (bool state = true) {
      if (state) {
        _stateMachine.OnChangeState += OnStateChange;
        return;
      }

      _stateMachine.OnChangeState -= OnStateChange;
    }

    private void OnStateChange (Enums.CameraControlType newType) {
      bool active = newType is Enums.CameraControlType.Full or Enums.CameraControlType.Rotate;

      if (!active) {
        SetPause();
      } else {
        Resume();
      }
    }

    private void InitCamera() {
      _cameraData.CinemachineFreeLook.SetAxisNames("", "");
    }

    private void SetTarget (Transform target) {
      _cameraData.CinemachineFreeLook.Follow = target;
      _cameraData.CinemachineFreeLook.LookAt = target;
    }

    private void DetectFirstEmptyClick() {
      if (Input.GetMouseButtonDown(0)) {
        _isCanRotate = !Utils.IsPointerOverGameObject;
      }
    }

    private void Rotate() {
      if (Utils.IsPointerOverGameObject || !_isCanRotate) {
        _isCanRotate = false;
        _cameraData.CinemachineFreeLook.SetAxisValues(0, 0);
        return;
      }

      bool mousePressed = Input.GetMouseButton(0);

      if (mousePressed) {
        _isStopped = false;
        _cameraData.CinemachineFreeLook.SetAxisValues(Input.GetAxis(X_AXIS), Input.GetAxis(Y_AXIS));
        Events.CameraEvents.OnRotate?.Invoke(true);
      } else {
        if (_isStopped) {
          return;
        }

        StopCameraRotating();
      }
    }

    private void StopCameraRotating() {
      _isStopped = true;
      Events.CameraEvents.OnRotate?.Invoke(false);
      _cameraData.CinemachineFreeLook.SetAxisValues(0, 0);
    }
  }
}