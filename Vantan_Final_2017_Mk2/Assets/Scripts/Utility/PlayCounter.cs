using UnityEngine;
using System.Collections;
using System.IO;

namespace MSMM {

  [System.Serializable]
  public struct CounterData {
    [SerializeField]
    private uint udccount;

    public uint UDCCount {
      get { return udccount; }
      set { udccount = value; }
    }
    
    [SerializeField]
    private uint stgcount;

    public uint STGCount {
      get { return stgcount; }
      set { stgcount = value; }
    }

    [SerializeField]
    private uint udccountend;

    public uint UDCCountEnd {
      get { return udccount; }
      set { udccount = value; }
    }

    [SerializeField]
    private uint stgcountend;

    public uint STGCountEnd {
      get { return stgcount; }
      set { stgcount = value; }
    }

    public void SaveData() {
      string json = JsonUtility.ToJson(this, true);
      FileStream fs = new FileStream(Application.dataPath + "/GameData/Counter/data.json", FileMode.Create);
      StreamWriter sw = new StreamWriter(fs);
      sw.Write(json);
      sw.Close();
      fs.Close();
    }

    public bool LoadData() {
      string json = "";
      if (Directory.Exists(Path.GetDirectoryName(Application.dataPath + "/GameData/Counter/data.json"))) {
        FileStream fs = new FileStream(Application.dataPath + "/GameData/Counter/data.json", FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        json = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        if (json == "") return false;
      }
      else {
        Directory.CreateDirectory(Path.GetDirectoryName(Application.dataPath + "/GameData/Counter/data.json"));
        SerializeDefaultData();
        return false;
      }

      var temp = JsonUtility.FromJson<CounterData>(json);
      this.UDCCount = temp.UDCCount;
      this.STGCount = temp.STGCount;
      this.UDCCountEnd = temp.UDCCountEnd;
      this.STGCountEnd = temp.STGCountEnd;
      return true;
    }

    public void SerializeDefaultData() {
      CounterData temp = new CounterData();
      temp.UDCCount = temp.STGCount = temp.UDCCountEnd = temp.STGCountEnd = 0;
      temp.SaveData();
    }
  }

  public enum CountType {
    UDCSelect,
    STGSelect,
    UDCEnd,
    STGEnd,
  }

  public static class PlayCounter{
    static CounterData data;

    public static void AddCount(CountType type) {
      switch (type) {
        case CountType.UDCSelect:
          data.UDCCount++;
          break;
        case CountType.STGSelect:
          data.STGCount++;
          break;
        case CountType.UDCEnd:
          data.UDCCountEnd++;
          break;
        case CountType.STGEnd:
          data.STGCountEnd++;
          break;
      }

      data.SaveData();
    }
  }
}
