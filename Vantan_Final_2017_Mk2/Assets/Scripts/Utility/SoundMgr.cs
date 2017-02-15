using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//サウンド管理
public class SoundMgr {
  //SEチャンネル数
  const int CHANNEL = 15;

  //サウンド種別
  enum SoundType {
    Bgm,    //BGM
    Se,     //SE
  }

  //シングルトン
  static SoundMgr singleton = null;
  //インスタンス取得
  public static SoundMgr GetInstance() {
    return singleton ?? (singleton = new SoundMgr());
  }

  //サウンド再生のためのオブジェクト
  GameObject SoundObject = null;
  //サウンドリソース
  AudioSource SourceBgm = null;   //BGM
  AudioSource SourceSe = null;   //SE(デフォルト)
  AudioSource[] SourceSeArray;    //SE(チャンネル)

  //アクセスするためのテーブル
  Dictionary<string, Data> poolBgm = new Dictionary<string, Data>();
  // SEにアクセスするためのテーブル 
  Dictionary<string, Data> poolSe = new Dictionary<string, Data>();


  //保持するデータ
  class Data {
    //アクセス用のキー
    public string Key;
    //リソース名
    public string ResName;
    //AudioClip
    public AudioClip Clip;

    //コンストラクタ
    public Data(string key, string res) {
      Key = key;
      ResName = "Sounds/" + res;

      //AudioClipの取得
      Clip = Resources.Load<AudioClip>(ResName);
    }
  }


  //コンストラクタ
  public SoundMgr() {
    //チャンネル確保
    SourceSeArray = new AudioSource[CHANNEL];
  }


  //AudioSourceを取得する
  AudioSource GetAudioSource(SoundType type, int channel = -1) {
    if (SoundObject == null) {
      //GameObjectがなければ作る
      SoundObject = new GameObject("SoundMgr");
      //破棄しないようにする
      GameObject.DontDestroyOnLoad(SoundObject);
      //AudioSourceを作成
      SourceBgm = SoundObject.AddComponent<AudioSource>();
      SourceSe = SoundObject.AddComponent<AudioSource>();
      for (int i = 0; i < CHANNEL; i++) {
        SourceSeArray[i] = SoundObject.AddComponent<AudioSource>();
      }
    }

    //BGM
    if (type == SoundType.Bgm) {
      return SourceBgm;
    }
    else {
      //チャンネル指定
      if (0 <= channel && channel < CHANNEL) {
        return SourceSeArray[channel];
      }
      else {
        //デフォルト
        return SourceSe;
      }
    }
  }


  //サウンドのロード BGM
  public static void SoundLoadBgm(string key, string resName) {
    GetInstance().LoadBgm(key, resName);
  }
  void LoadBgm(string key, string resName) {
    if (poolBgm.ContainsKey(key)) {
      //すでに登録済みなのでいったん消す
      poolBgm.Remove(key);
    }
    poolBgm.Add(key, new Data(key, resName));
  }


  //BGMの再生 ※事前にLoadBgmでロードしておくこと
  public static bool PlayBgm(string key) {
    return GetInstance()._PlayBgm(key);
  }
  bool _PlayBgm(string key) {
    if (poolBgm.ContainsKey(key) == false) {
      //対応するキーがない
      return false;
    }

    //いったん止める
    _StopBgm();

    //リソースの取得
    var _data = poolBgm[key];
    //チャンネル指定
    var source = GetAudioSource(SoundType.Bgm);
    source.loop = true;
    source.clip = _data.Clip;
    source.Play();
    return true;
  }

  public static bool PlayBgm(string key, float volume) {
    return GetInstance()._PlayBgm(key, volume);
  }
  bool _PlayBgm(string key, float volume) {
    if (poolBgm.ContainsKey(key) == false) {
      //対応するキーがない
      return false;
    }

    var volumeClamp = Mathf.Clamp(volume, 0, 1.0f);

    //いったん止める
    _StopBgm();

    //リソースの取得
    var _data = poolBgm[key];
    //チャンネル指定
    var source = GetAudioSource(SoundType.Bgm);
    source.loop = true;
    source.clip = _data.Clip;
    source.volume = volumeClamp;
    source.Play();
    return true;
  }

  //BGMの停止
  public static bool StopBgm() {
    return GetInstance()._StopBgm();
  }
  bool _StopBgm() {
    GetAudioSource(SoundType.Bgm).Stop();

    return true;
  }

  public static int isBgmPlaying(string key) {
    return GetInstance()._isBgmPlaying(key);
  }
  private int _isBgmPlaying(string key) {
    if (!poolBgm.ContainsKey(key)) {
      return -1;
    }

    if (GetAudioSource(SoundType.Bgm).isPlaying) {
      return 1;
    }
    else {
      return 0;
    }
  }


  //サウンドのロード SE
  public static void SoundLoadSe(string key, string resName) {
    GetInstance().LoadSe(key, resName);
  }

  void LoadSe(string key, string resName) {
    if (poolSe.ContainsKey(key)) {
      //すでに登録済みなのでいったん消す
      poolSe.Remove(key);
    }
    poolSe.Add(key, new Data(key, resName));
  }


  //SEの再生 ※事前にLoadSeでロードしておくこと
  public static bool PlaySe(string key, int channel = -1) {
    return GetInstance()._PlaySe(key, channel);
  }
  bool _PlaySe(string key, int channel = -1) {
    if (poolSe.ContainsKey(key) == false) {
      //対応するキーがない
      return false;
    }

    //リソースの取得
    var _data = poolSe[key];

    if (0 <= channel && channel < CHANNEL) {
      //チャンネル指定
      var source = GetAudioSource(SoundType.Se, channel);
      source.clip = _data.Clip;
      source.Play();
    }
    else {
      //デフォルトで再生
      var source = GetAudioSource(SoundType.Se);
      source.PlayOneShot(_data.Clip);
    }
    return true;
  }

  public static bool PlayVolSe(string key, float volume, int channel = -1) {
    return GetInstance()._PlayVolSe(key, volume, channel);
  }
  bool _PlayVolSe(string key, float volume, int channel = -1) {
    if (poolSe.ContainsKey(key) == false) {
      //対応するキーがない
      return false;
    }

    var volumeClamp = Mathf.Clamp(volume, 0, 1.0f);

    //リソースの取得
    var _data = poolSe[key];

    if (0 <= channel && channel < CHANNEL) {
      //チャンネル指定
      var source = GetAudioSource(SoundType.Se, channel);
      source.volume = volumeClamp;
      source.clip = _data.Clip;
      source.Play();
    }
    else {
      //デフォルトで再生
      var source = GetAudioSource(SoundType.Se);
      source.volume = volumeClamp;
      source.PlayOneShot(_data.Clip);
    }
    return true;
  }
}