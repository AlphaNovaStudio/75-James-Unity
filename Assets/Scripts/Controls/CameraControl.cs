using System.Collections.Generic;
using Interfaces;
using Other;
using StateMachine;
using UnityEngine;

namespace Controls {
  public class CameraControl : MonoBehaviour {
    private StateMachine<Enums.CameraControlType> _stateMachine;
    private CameraData _cameraData;
    private List<IBehaviour> _behaviours;

    private void Awake() {
      InitComponents();
      SubscribeEvents();
    }

    private void Update() {
      UpdateBehaviours();
    }

    private void OnDestroy() {
      SubscribeEvents(false);

      foreach (IBehaviour behaviour in _behaviours) {
        behaviour.Dispose();
      }
    }

    private void InitComponents() {
      _stateMachine = new StateMachine<Enums.CameraControlType>();

      if (TryGetComponent(out CameraData cameraData)) {
        _cameraData = cameraData;

        _behaviours = new List<IBehaviour> {
          new CameraRotateBehaviour(_cameraData, _stateMachine),
          new CameraZoomBehaviour(_cameraData, _stateMachine)
        };
      }
    }

    private void SubscribeEvents (bool state = true) {
      if (state) {
        Events.CameraEvents.SetCameraControlType += OnCameraControlChange;
        return;
      }

      Events.CameraEvents.SetCameraControlType -= OnCameraControlChange;
    }

    private void UpdateBehaviours() {
      foreach (IBehaviour behaviour in _behaviours) {
        behaviour.Update();
      }
    }

    private void OnCameraControlChange (Enums.CameraControlType type) {
      _stateMachine.ChangeState(type);
    }
  }
}