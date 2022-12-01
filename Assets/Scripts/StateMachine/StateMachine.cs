using System;
using System.Collections.Generic;

namespace StateMachine {
  public class StateMachine<T> where T : Enum {
    public StateMachine() {
      States = new Dictionary<T, State<T>>();

      foreach (T value in Enum.GetValues(typeof(T))) {
        States.Add(value, new State<T>(value));
      }

      ChangeState((T)Enum.GetValues(typeof(T)).GetValue(0));
    }

    public void ChangeState (T newState) {
      if (CurrentState == States[newState]) {
        return;
      }

      CurrentState = States[newState];
      CurrentState.Init?.Invoke();
      OnChangeState?.Invoke(newState);
    }

    public Dictionary<T, State<T>> States { get; }
    public State<T> CurrentState { get; private set; }

    public Action<T> OnChangeState { get; set; }
  }
}