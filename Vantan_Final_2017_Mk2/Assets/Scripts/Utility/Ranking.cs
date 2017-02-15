using UnityEngine;
using UnityEngine.UI;
using System;
using System.IO;
using System.Collections;

namespace MSMM {
  [System.Serializable]
  public struct RankingData : IComparable<RankingData> {

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
      return -Score.CompareTo(other.Score);
    }
  }

  [System.Serializable]
  public class RankingArray {

    public RankingData[] data;

    public void SaveData(string fileName) {
      string json = JsonUtility.ToJson(this, true);
      FileStream fs = new FileStream(Application.dataPath + "/GameData/Ranking/" + fileName + ".json", FileMode.Create);
      StreamWriter sw = new StreamWriter(fs);
      sw.Write(json);
      sw.Close();
      fs.Close();
    }

    public bool LoadData(string fileName) {
      string json = "";
      if (Directory.Exists(Path.GetDirectoryName(Application.dataPath + "/GameData/Ranking/" + fileName + ".json"))) {
        FileStream fs = new FileStream(Application.dataPath + "/GameData/Ranking/" + fileName + ".json", FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        json = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        if (json == "") return false;
      }
      else {
        Directory.CreateDirectory(Path.GetDirectoryName(Application.dataPath + "/GameData/Ranking/" + fileName + ".json"));
        SerializeDefaultData(fileName);
        return false;
      }

      var temp = JsonUtility.FromJson<RankingArray>(json);
      data = temp.data;
      return true;
    }

    public void SerializeDefaultData(string fileName) {
      RankingArray temp = new RankingArray();
      temp.data = new RankingData[5];
      for (uint i = 0; i < temp.data.Length; i++) {
        temp.data[i] = new RankingData();
        temp.data[i].Name = "AAA";
        temp.data[i].Score = 100 + 100 * i;
      }
      temp.SaveData(fileName);
    }
  }

  public enum RankingMode {
    Update,
    Review
  }

  public class Ranking : MonoBehaviour {

    RankingArray rankingArray;

    [SerializeField]
    RankingMode mode = RankingMode.Update;

    [SerializeField]
    Text[] rankingTexts = null;

    [SerializeField]
    private string fileName = "";

    [SerializeField]
    GameObject newRecordCanvas = null;

    [SerializeField]
    MSMM.MenuSelector[] selectors = null;

    [SerializeField]
    string scoreUnit = "";
    [SerializeField]
    string namePlaceholder = "";
    [SerializeField]
    string scorePlaceholder = "";

    [SerializeField]
    bool useNamePlaceholder = false;
    [SerializeField]
    bool useScorePlaceholder = false;

    private bool rankingUpdated = false;
    private bool updatingRanking = false;

    void Start() {
      if (fileName == "") {
        Debug.LogError("fileName not defined!");
      }
      else {
        rankingArray = new RankingArray();
        if (!rankingArray.LoadData(fileName)) {
          rankingArray.SerializeDefaultData(fileName);
          rankingArray.LoadData(fileName);
        }
        Array.Sort(rankingArray.data);
        UpdateTexts();
      }

      if (useNamePlaceholder) {
        if (namePlaceholder == "") {
          namePlaceholder = "Name : ";
        }
      }

      if (useScorePlaceholder) {
        if (scorePlaceholder == "") {
          scorePlaceholder = "Score : ";
        }
      }
    }

    void Update() {
      if (mode == RankingMode.Update) {
        if (updatingRanking && !newRecordCanvas.activeSelf) {
          SwapScores(MSMM.RankingTempData.TempName, MSMM.RankingTempData.TempScore);
          Array.Sort(rankingArray.data);
          rankingArray.SaveData(fileName);
          foreach (var selec in selectors) {
            selec.SetComponentActive(true);
          }
          rankingUpdated = true;
          updatingRanking = false;
        }

        if (!rankingUpdated) {
          if (CheckForNewRecord(MSMM.RankingTempData.TempScore)) {
            ActivateNameEntry();
          }
        }
      }

      UpdateTexts();
    }

    void UpdateTexts() {
      if (rankingTexts == null || rankingTexts.Length < 5) {
        Debug.LogError("Texts Missing");
      }

      for (int i = 0; i < rankingTexts.Length; i++) {
        if (scoreUnit != "") {
          rankingTexts[i].text = namePlaceholder + rankingArray.data[i].Name + "   " + scorePlaceholder + rankingArray.data[i].Score.ToString() + " " + scoreUnit;
        }
        else {
          rankingTexts[i].text = namePlaceholder + rankingArray.data[i].Name + "   " + scorePlaceholder + rankingArray.data[i].Score.ToString();
        }
      }
    }

    bool CheckForNewRecord(uint newScore) {
      for (int i = 0; i < 5; i++) {
        if (newScore > rankingArray.data[i].Score) {
          return true;
        }
      }
      return false;
    }

    void ActivateNameEntry() {
      foreach (var selec in selectors) {
        selec.SetComponentActive(false);
      }
      newRecordCanvas.SetActive(true);
      newRecordCanvas.GetComponent<NewRecord>().PlayNewRecordSE();
      updatingRanking = true;
    }

    void SwapScores(string newName, uint newScore) {
      bool swapped = false;
      RankingArray newAry = new RankingArray();
      newAry.data = new RankingData[5];
      for (int i = 0; i < 5; i++) {
        newAry.data[i] = new RankingData();
      }

      for (int i = 0; i < 5; i++) {
        if ((newScore > rankingArray.data[i].Score) && !swapped) {
          newAry.data[i].Name = newName;
          newAry.data[i].Score = newScore;
          swapped = true;
        }
        else if (!swapped) {
          newAry.data[i].Name = rankingArray.data[i].Name;
          newAry.data[i].Score = rankingArray.data[i].Score;
        }
        else {
          newAry.data[i].Name = rankingArray.data[i - 1].Name;
          newAry.data[i].Score = rankingArray.data[i - 1].Score;
        }
      }

      for (int i = 0; i < 5; i++) {
        rankingArray.data[i].Name = newAry.data[i].Name;
        rankingArray.data[i].Score = newAry.data[i].Score;
      }
    }

  }
}
