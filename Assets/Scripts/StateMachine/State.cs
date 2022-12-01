using System;

namespace StateMachine {
  public class State<T> where T : Enum {
    public State (T type) { StateType = type; }

    public Action Update { get; set; }
    public Action FixedUpdate { get; set; }
    public T StateType { get; }
    public Action Init { get; set; }
  }
}