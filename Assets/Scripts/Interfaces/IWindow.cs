namespace Interfaces {
  public interface IWindow {
    public void Close();

    public void Expand();

    public void Collapse();

    public void ChangeWindowSize();

    public bool IsExpanded { get; set; }
    public bool IsActivated { get; set; }
  }
}