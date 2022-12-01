using System;
using System.Drawing;

namespace DataBase {
  [Serializable]
  public struct SuitsData {
    public SuitData [] data;
  }

  [Serializable]
  public struct SuitData {
    public int id;
    public SuiteStatus status;
    public string title;
    public string description;
    public Color color;
    public bool den;
    public int bedrooms;
    public int size;
    public string size_unit;
    public string floorplan;
    public object asset;
    public string thumbnail;
    public int bathrooms;
  }
}