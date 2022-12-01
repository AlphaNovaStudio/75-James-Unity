namespace Interfaces {
  public interface IPausable {
    public void SetPause();

    public void Resume();

    public bool IsPaused { get; set; }
  }
}