namespace Interfaces {
  public interface IViewTab {
    public void SetSelectStatus (bool selected);

    public void UpdateWindowActiveness (bool active);

    public void UpdateTabActiveness (bool active);

    public bool Active { get; set; }
    public bool Selected { get; set; }
  }
}