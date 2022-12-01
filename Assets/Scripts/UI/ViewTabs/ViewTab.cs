using Interfaces;
using Other;
using StateMachine;
using UI.Windows;
using UnityEngine;

namespace UI.ViewTabs {
  public class ViewTab : MonoBehaviour, IViewTab {
    [SerializeField]
    private Enums.WindowTabType _type;
    [SerializeField]
    private ViewTabContainer _viewTabContainer;

    private StateMachine<Enums.WindowTabType> _stateMachine;
    private DialogWindow _controlWindow;

    private void Awake() {
      _viewTabContainer.SubscribeButtons(ChangeSelection);
      _viewTabContainer.InitVisual(_type);
    }

    private void Start() {
      SetSelectStatus(false);
    }

    public void SetSelectStatus (bool selected) {
      if (Selected == selected || !Active) {
        return;
      }

      Selected = selected;
      UpdateWindowActiveness(selected);
      _viewTabContainer.UpdateTabView(Active, Selected);

      if (Selected) {
        _stateMachine.ChangeState(_type);
      }
    }

    public void UpdateWindowActiveness (bool active) {
      if (active) {
        _controlWindow.Show();
      } else {
        _controlWindow.Close();
      }
    }

    public void UpdateTabActiveness (bool active) {
      Active = active;
      _viewTabContainer.UpdateTabView(Active, Selected);
    }

    public void Init (StateMachine<Enums.WindowTabType> stateMachine, DialogWindow controlWindow) {
      _stateMachine = stateMachine;
      _controlWindow = controlWindow;
      _stateMachine.OnChangeState += OnChangeState;
    }

    private void OnChangeState (Enums.WindowTabType newState) {
      SetSelectStatus(_type == newState);
    }

    private void ChangeSelection() {
      SetSelectStatus(!Selected);
    }

    public bool Active { get; set; }
    public bool Selected { get; set; }
    public Enums.WindowTabType Type {
      get {
        return _type;
      }
    }
  }
}