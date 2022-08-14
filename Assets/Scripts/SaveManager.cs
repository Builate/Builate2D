using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveManager : SingletonMonoBehaviour<SaveManager>
{
    public void Save()
    {
        string FolderPath = Application.persistentDataPath;
        string FilePath = Path.Combine(FolderPath, "testworld.builate");
        Debug.Log(FolderPath);
        DataWriter dataWriter = new DataWriter();

        #region シリアライズ処理
        // プレイヤーの位置
        dataWriter.Put(GameManager.Instance.Player.transform.position);
        #endregion

        File.WriteAllBytes(FilePath, dataWriter.GetData());
    }

    public void Load()
    {
        string FolderPath = Application.persistentDataPath;
        string FilePath = Path.Combine(FolderPath, "testworld.builate");
        byte[] data = File.ReadAllBytes(FilePath);
        DataReader dataReader = new DataReader(data);

        #region デシリアライズ処理
        GameManager.Instance.Player.transform.position = dataReader.GetVector3();
        #endregion
    }
}
