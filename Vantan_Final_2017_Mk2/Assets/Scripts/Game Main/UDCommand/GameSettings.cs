using UnityEngine;
using System.IO;
using UDC = UDCommand;

namespace UDCommand {

  [System.Serializable]
  public struct Settings {

    public int gameTime;

    public int easyDecrease;
    public int mediumDecrease;
    public int hardDecrease;


    public Settings(int gT, int eD, int mD, int hD) {
      gameTime = gT;

      easyDecrease = eD;
      mediumDecrease = mD;
      hardDecrease = hD;
    }
  }

  [System.Serializable]
  public class GameSettings {

    [SerializeField]
    private UDC.Settings gameSettings;

    public int GetGameTime() {
      return gameSettings.gameTime;
    }

    public int GetDecreaseByDificulty(UDC.Dificulty dif) {
      switch (dif) {
        case UDC.Dificulty.Easy:
          return gameSettings.easyDecrease;
        case UDC.Dificulty.Medium:
          return gameSettings.mediumDecrease;
        case UDC.Dificulty.Hard:
          return gameSettings.hardDecrease;
        default:
          return gameSettings.easyDecrease;
      }
    }

    public void SetData(UDC.Settings newSettings) {
      gameSettings = newSettings;
    }

    public void SerializeToJson() {
      string json = JsonUtility.ToJson(this, true);
      FileStream fs = new FileStream(Application.dataPath + "/GameData/UDCommand/gameSettings.json", FileMode.Create);
      StreamWriter sw = new StreamWriter(fs);
      sw.Write(json);
      sw.Close();
      fs.Close();
    }

    public bool LoadFromJson() {
      string json;
      if (Directory.Exists(Path.GetDirectoryName(Application.dataPath + "/GameData/UDCommand/gameSettings.json"))) {
        FileStream fs = new FileStream(Application.dataPath + "/GameData/UDCommand/gameSettings.json", FileMode.OpenOrCreate);
        StreamReader sr = new StreamReader(fs);
        json = sr.ReadToEnd();
        sr.Close();
        fs.Close();
        if (json == "") return false;
      }
      else {
        Directory.CreateDirectory(Path.GetDirectoryName(Application.dataPath + "/GameData/UDCommand/gameSettings.json"));
        return false;
      }
      var temp = JsonUtility.FromJson<UDC.GameSettings>(json);
      gameSettings = temp.gameSettings;
      return true;
    }

    public void SerializeDefaultData() {
      SetData(new UDC.Settings(10, 50, 100, 150));
      SerializeToJson();
    }
  }
}