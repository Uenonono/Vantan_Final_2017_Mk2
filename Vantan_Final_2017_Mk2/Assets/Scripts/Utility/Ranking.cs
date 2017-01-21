using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

namespace MSMM {
  [System.Serializable]
  public class RankingData : IComparable<RankingData> {

    [SerializeField]
    private string name;

    public string Name {
      get { return name; }
      set { name = value; }
    }

    [SerializeField]
    private uint score;

    public uint Score {
      get { return score; }
      set { score = value; }
    }

    public int CompareTo(RankingData other) {
      if(other == null) {
        return 1;
      }
      else {
        return Score.CompareTo(other.Score);
      }
    }
  }

  [System.Serializable]
  public class RankingArray {
    public RankingData[] data;

    public void SaveData(string path) {
      string json = JsonUtility.ToJson(this, true);
      FileStream fs = new FileStream(Application.dataPath + "/GameData/" + path, FileMode.Create);
      StreamWriter sw = new StreamWriter(fs);
      sw.Write(json);
      sw.Close();
      fs.Close();
    }

    public bool LoadData(string path) {
      string json = "";
      if (Directory.Exists(Path.GetDirectoryName(Application.dataPath + "/GameData/" + path))) {
        FileStream fs = new FileStream(Application.dataPath + "/GameData/" + path, FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        json = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        if (json == "") return false;
      }
      else {
        Directory.CreateDirectory(Path.GetDirectoryName(Application.dataPath + "/GameData/" + path));
        return false;
      }

      var temp = JsonUtility.FromJson<RankingArray>(json);
      data = temp.data;
      return true;
    }

    public void SerializeDefaultData(string path) {
      RankingArray temp = new RankingArray();
      temp.data = new RankingData[5];
      for(uint i = 0; i < temp.data.Length; i++) {
        temp.data[i].Name = "AAA";
        temp.data[i].Score = 100 + 100 * i;
      }
    }
  }

  public class Ranking : MonoBehaviour {

    RankingArray rankingArray;

    [SerializeField]
    Text[] rankingTexts = null;

    [SerializeField]
    private string path = "";

    void Start() {
      rankingArray = new RankingArray();
      rankingArray.LoadData(path);
      Array.Sort(rankingArray.data);
      UpdateTexts();
    }

    void Update() {

    }


    void UpdateTexts() {
      if(rankingTexts == null || rankingTexts.Length < 5) {
        Debug.Log("Texts Missing");
      }

      for(int i = 0;i < rankingTexts.Length; i++) {
        rankingTexts[i].text = "Name : " + rankingArray.data[i].Name + " Score : " + rankingArray.data[i].Score.ToString();
      }
    }

  }
}
