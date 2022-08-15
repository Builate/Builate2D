using System.IO;
using System.Linq;
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
        // �v���C���[�̈ʒu
        dataWriter.Put(GameManager.Instance.Player.transform.position);
        // �`�����N��
        int chunksCount = MapManager.Instance.map.Count;
        dataWriter.Put(chunksCount);
        // �`�����N���̐������J��Ԃ�
        for (int i = 0; i < chunksCount; i++)
        {
            // �L�[
            Vector2Int key = MapManager.Instance.map.Keys.ToArray()[i];
            dataWriter.Put(key);

            // �e�`�����N�̃Z�[�u����
            MapManager.Instance.map[key].Writer(dataWriter);
        }
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
        // �v���C���[�̈ʒu
        GameManager.Instance.Player.transform.position = dataReader.GetVector3();
        // �`�����N��
        int chunksCount = dataReader.GetInt();
        // �`�����N���̐������J��Ԃ�
        for (int i = 0; i < chunksCount; i++)
        {
            // �L�[
            Vector2Int key = dataReader.GetVector2Int();

            // �e�`�����N�̃��[�h����
            Chunk chunk = new Chunk();
            chunk.Reader(dataReader);
            MapManager.Instance.map[key] = chunk;
            MapManager.Instance.SetTilemap(key);
        }
        #endregion
    }
}
