using System;
using Interfaces;

namespace Abstract {
  public abstract class BaseBehaviour : IBehaviour {
    public abstract void Update();

    public abstract void FixedUpdate();

    public abstract void OnDestroy();

    public void SetPause() { IsPaused = true; }

    public void Resume() { IsPaused = false; }

    public void Dispose() { GC.SuppressFinalize(this); }

    public bool IsPaused { get; set; }
  }
}