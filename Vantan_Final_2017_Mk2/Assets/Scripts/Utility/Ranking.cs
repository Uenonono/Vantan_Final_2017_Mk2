﻿using UnityEngine;
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
      for(uint i = 0; i < temp.data.Length; i++) {
        temp.data[i] = new RankingData();
        temp.data[i].Name = "AAA";
        temp.data[i].Score = 100 + 100 * i;
      }
      temp.SaveData(fileName);
    }
  }

  public class Ranking : MonoBehaviour {

    RankingArray rankingArray;

    [SerializeField]
    Text[] rankingTexts = null;

    [SerializeField]
    private string fileName = "";

    [SerializeField]
    GameObject newRecordCanvas = null;

    [SerializeField]
    MSMM.MenuSelector[] selectors = null;

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
    }

    void Update() {
      if (CheckForNewRecord(MSMM.RankingTempData.TempScore)) {
        ActivateNameEntry();
      }

      if(updatingRanking && !newRecordCanvas.activeSelf) {
        SwapScores(MSMM.RankingTempData.TempName, MSMM.RankingTempData.TempScore);
        Array.Sort(rankingArray.data);
        rankingArray.SaveData(fileName);
      }
    }

    void UpdateTexts() {
      if(rankingTexts == null || rankingTexts.Length < 5) {
        Debug.LogError("Texts Missing");
      }

      for(int i = 0;i < rankingTexts.Length; i++) {
        rankingTexts[i].text = "Name : " + rankingArray.data[i].Name + "      Score : " + rankingArray.data[i].Score.ToString();
      }
    }

    bool CheckForNewRecord(uint newScore) {
      for(int i = 0; i < 5; i++) {
        if(newScore > rankingArray.data[i].Score) {
          return true;
        }
      }
      return false;
    }

    void ActivateNameEntry() {
      foreach(var selec in selectors) {
        selec.SetComponentActive(false);
      }
      newRecordCanvas.SetActive(true);
      updatingRanking = true;
    }

    void SwapScores(string newName, uint newScore) {
      bool swapped = false;
      RankingData temp = new RankingData();
      for (int i = 0; i < 5; i++) {
        if (newScore > rankingArray.data[i].Score && !swapped) {
          temp = rankingArray.data[i];
          rankingArray.data[i].Name = newName;
          rankingArray.data[i].Score = newScore;
          swapped = true;
        }
        if (swapped) {
          if(i == 4) {
            rankingArray.data[i] = temp;
          }
          else {
            rankingArray.data[i + 1] = rankingArray.data[i];
            rankingArray.data[i] = temp;
          }
        }
      }
    }
   
  }
}
