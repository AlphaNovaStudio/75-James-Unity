using System.Collections.Generic;
using System.Linq;
using Factories;
using Other;
using StateMachine;
using UI.ViewTabs;
using UI.Windows;
using UnityEngine;

namespace Controllers {
  public class ViewTabsController : MonoBehaviour {
    private readonly StateMachine<Enums.WindowTabType> _stateMachine = new StateMachine<Enums.WindowTabType>();
    [SerializeField]
    private GameObjectFactory _tabsFactory;
    [SerializeField]
    private DialogWindowFactory _dialogWindowsFactory;
    [SerializeField]
    private Transform _dialogsParent;

    private Dictionary<Enums.WindowTabType, ViewTab> _viewTabs;
    private DialogWindow [] _dialogWindows;

    private void Awake() {
      SpawnViewTabs();
      SpawnDialogWindows();
      InitViewTabs();
      SubscribeEvents(true);
      InitDialogWindows();
      UpdateTabsActiveness();
    }

    private void Update() {
      CheckForDeselectingClick();
    }

    private void OnDestroy() {
      SubscribeEvents(false);
    }

    private void UpdateTabsActiveness() {
      foreach (KeyValuePair<Enums.WindowTabType, ViewTab> keyValuePair in _viewTabs) {
        if (keyValuePair.Key == Enums.WindowTabType.Email) {
          _viewTabs[Enums.WindowTabType.Email].UpdateTabActiveness(false);
          continue;
        }

        keyValuePair.Value.UpdateTabActiveness(true);
      }
    }

    private void InitDialogWindows() {
      foreach (DialogWindow dialogWindow in _dialogWindows) {
        dialogWindow.SetupWindow(DeselectActiveTabs);
      }
    }

    private void DeselectActiveTabs() {
      foreach (KeyValuePair<Enums.WindowTabType, ViewTab> viewTab in _viewTabs) {
        viewTab.Value.SetSelectStatus(false);
      }
    }

    private void SpawnViewTabs() {
      GameObject tabsParent = _tabsFactory.GetNewInstance(transform);
      tabsParent.transform.SetSiblingIndex(0);
      ViewTab [] viewTabs = tabsParent.GetComponentsInChildren<ViewTab>();
      _viewTabs = new Dictionary<Enums.WindowTabType, ViewTab>();

      foreach (ViewTab viewTab in viewTabs) {
        _viewTabs.Add(viewTab.Type, viewTab);
      }
    }

    private void SubscribeEvents (bool value) {
      if (value) {
        Events.SuiteCardsEvents.OnKeeperChanged += SetEmailTabActive;
        return;
      }

      Events.SuiteCardsEvents.OnKeeperChanged -= SetEmailTabActive;
    }

    private void SetEmailTabActive (bool value) {
      _viewTabs[Enums.WindowTabType.Email].UpdateTabActiveness(value);
    }

    private void SpawnDialogWindows() {
      _dialogWindows = _dialogWindowsFactory.GetNewInstances(_dialogsParent);
    }

    private void InitViewTabs() {
      foreach ((Enums.WindowTabType key, ViewTab viewTab) in _viewTabs) {
        viewTab.Init(_stateMachine, _dialogWindows.FirstOrDefault(x => x.Type == viewTab.Type));
      }
    }

    private void CheckForDeselectingClick() {
      if (!Input.GetMouseButton(0)) {
        return;
      }

      if (!Utils.IsPointerOverGameObject) {
        _stateMachine.ChangeState(Enums.WindowTabType.None);
      }
    }
  }
}