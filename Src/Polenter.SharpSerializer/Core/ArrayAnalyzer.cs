
// Type: Polenter.Serialization.Core.ArrayAnalyzer
// Assembly: Polenter.SharpSerializer, Version=2.10.0.0, Culture=neutral, PublicKeyToken=8f4f20011571ee5f
// MVID: 748E9843-D6B2-4072-9BB9-08EFC20211B5
// Assembly location: C:\Users\Admin\Desktop\RE\WPV XAP Deployer 2.0\Polenter.SharpSerializer.dll

using System;
using System.Collections.Generic;

#nullable disable
namespace Polenter.Serialization.Core
{
  public class ArrayAnalyzer
  {
    private readonly object _array;
    private readonly ArrayInfo _arrayInfo;
    private IList<int[]> _indexes;

    public ArrayAnalyzer(object array)
    {
      this._array = array;
      this._arrayInfo = this.getArrayInfo(array.GetType());
    }

    public ArrayInfo ArrayInfo => this._arrayInfo;

    private int getRank(Type arrayType) => arrayType.GetArrayRank();

    private int getLength(int dimension, Type arrayType)
    {
      return (int) arrayType.GetMethod("GetLength").Invoke(this._array, new object[1]
      {
        (object) dimension
      });
    }

    private int getLowerBound(int dimension, Type arrayType)
    {
      return this.getBound("GetLowerBound", dimension, arrayType);
    }

    private int getBound(string methodName, int dimension, Type arrayType)
    {
      return (int) arrayType.GetMethod(methodName).Invoke(this._array, new object[1]
      {
        (object) dimension
      });
    }

    private ArrayInfo getArrayInfo(Type arrayType)
    {
      ArrayInfo arrayInfo = new ArrayInfo();
      for (int dimension = 0; dimension < this.getRank(arrayType); ++dimension)
        arrayInfo.DimensionInfos.Add(new DimensionInfo()
        {
          Length = this.getLength(dimension, arrayType),
          LowerBound = this.getLowerBound(dimension, arrayType)
        });
      return arrayInfo;
    }

    public IEnumerable<int[]> GetIndexes()
    {
      if (this._indexes == null)
      {
        this._indexes = (IList<int[]>) new List<int[]>();
        this.ForEach(new Action<int[]>(this.addIndexes));
      }
      foreach (int[] item in (IEnumerable<int[]>) this._indexes)
        yield return item;
    }

    public IEnumerable<object> GetValues()
    {
      foreach (int[] indexSet in this.GetIndexes())
      {
        object value = ((Array) this._array).GetValue(indexSet);
        yield return value;
      }
    }

    private void addIndexes(int[] obj) => this._indexes.Add(obj);

    public void ForEach(Action<int[]> action)
    {
      DimensionInfo dimensionInfo = this._arrayInfo.DimensionInfos[0];
      for (int lowerBound = dimensionInfo.LowerBound; lowerBound < dimensionInfo.LowerBound + dimensionInfo.Length; ++lowerBound)
      {
        List<int> coordinates = new List<int>();
        coordinates.Add(lowerBound);
        if (this._arrayInfo.DimensionInfos.Count < 2)
          action(coordinates.ToArray());
        else
          this.forEach(this._arrayInfo.DimensionInfos, 1, (IEnumerable<int>) coordinates, action);
      }
    }

    private void forEach(
      IList<DimensionInfo> dimensionInfos,
      int dimension,
      IEnumerable<int> coordinates,
      Action<int[]> action)
    {
      DimensionInfo dimensionInfo = dimensionInfos[dimension];
      for (int lowerBound = dimensionInfo.LowerBound; lowerBound < dimensionInfo.LowerBound + dimensionInfo.Length; ++lowerBound)
      {
        List<int> coordinates1 = new List<int>(coordinates);
        coordinates1.Add(lowerBound);
        if (dimension == this._arrayInfo.DimensionInfos.Count - 1)
          action(coordinates1.ToArray());
        else
          this.forEach(this._arrayInfo.DimensionInfos, dimension + 1, (IEnumerable<int>) coordinates1, action);
      }
    }
  }
}
