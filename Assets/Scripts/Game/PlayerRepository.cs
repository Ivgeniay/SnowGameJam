using Assets.Scripts.Game;
using Sirenix.Utilities;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRepository : IRestartable
{
    public string FIRST { get; } = "First";
    public string SECOND { get; } = "Second";
    public string THIRD { get; } = "Third";
    public string FOURTH { get; } = "Fourth";
    public string FIFTH { get; } = "Fifth";

    private Dictionary<string, int> recors = new Dictionary<string, int>();

    public PlayerRepository() {
        Initialize();
    }

    private void Initialize() {
        recors.Add(FIRST, GetRecordsFromRegistry(FIRST));
        recors.Add(SECOND, GetRecordsFromRegistry(SECOND));
        recors.Add(THIRD, GetRecordsFromRegistry(THIRD));
        recors.Add(FOURTH, GetRecordsFromRegistry(FOURTH));
        recors.Add(FIFTH, GetRecordsFromRegistry(FIFTH));

        //Game.Manager.OnInitialized += OnGameInitialized;
    }

    private void OnGameInitialized()
    {
        Game.Manager.Restart.Register(this);
    }

    public void Save(int record) {
        AddNewRecord(recors, record);
        SetRecordsToRegistry(recors);
    }
    public void Save() => SetRecordsToRegistry(recors);
    public void Reset() => recors.ForEach(el => ResetRecord(el.Key, el.Value));

    public Dictionary<string, int> GetRecords() => recors;

    private int GetRecordsFromRegistry(string key) => PlayerPrefs.GetInt(key);
    private void SetRecordToRegistry(string key, int record) => PlayerPrefs.SetInt(key, record);
    private void ResetRecord(string key, int record) => PlayerPrefs.SetInt(key, 0);
    private void SetRecordsToRegistry(Dictionary<string, int> records) => recors.ForEach(el => SetRecordToRegistry(el.Key, el.Value));
    private void AddNewRecord(Dictionary<string, int> records, int record)
    {
        if (record > records[FIRST]) {
            records[FIRST] = record;
            return;
        }
        if (record > records[SECOND]) {
            records[SECOND] = record;
            return;
        }
        if (record > records[THIRD]) {
            records[THIRD] = record;
            return;
        }
        if (record > records[FOURTH]) {
            records[FOURTH] = record;
            return;
        }
        if (record > records[FIFTH]) {
            records[FIFTH] = record;
            return;
        }
    }

    public void Restart()
    {
        Save();
    }
}