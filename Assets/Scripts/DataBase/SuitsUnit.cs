using System;
using System.Drawing;

namespace DataBase {
  [Serializable]
  public struct SuitsUnitData {
    public SuitsUnit [] data;
  }

  [Serializable]
  public struct SuitsUnit {
    public int id;
    public SuiteStatus status;
    public string title;
    public string description;
    public int number;
    public int size_exterior;
    public SuitsState state;
    public SuitsViews [] views;
    public Color colour;
    public SuitsViews [] balconyview;
    public int suite;
  }

  public enum SuitsViews {
    North,
    West,
    East,
    South
  }

  public enum SuiteStatus {
    published,
    draft
  }

  public enum SuitsState {
    Available,
    Hold,
    Sold
  }
}