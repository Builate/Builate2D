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
        DataWriter dataWriter = new DataWriter();

        #region �V���A���C�Y����

        #endregion

        File.WriteAllBytes(FilePath, dataWriter.GetData());
    }

    public void Load()
    {
        string FolderPath = Application.persistentDataPath;
        string FilePath = Path.Combine(FolderPath, "testworld.builate");
        byte[] data = File.ReadAllBytes(FilePath);
        DataReader dataReader = new DataReader(data);

        #region �f�V���A���C�Y����

        #endregion
    }
}
