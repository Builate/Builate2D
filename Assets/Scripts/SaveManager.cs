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

        #region シリアライズ処理
        // プレイヤーの位置
        dataWriter.Put(GameManager.Instance.Player.transform.position);
        // チャンク数
        int chunksCount = MapManager.Instance.map.Count;
        dataWriter.Put(chunksCount);
        // チャンク数の数だけ繰り返す
        for (int i = 0; i < chunksCount; i++)
        {
            // キー
            Vector2Int key = MapManager.Instance.map.Keys.ToArray()[i];
            dataWriter.Put(key);

            // 各チャンクのセーブ処理
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

        #region デシリアライズ処理
        // プレイヤーの位置
        GameManager.Instance.Player.transform.position = dataReader.GetVector3();
        // チャンク数
        int chunksCount = dataReader.GetInt();
        // チャンク数の数だけ繰り返す
        for (int i = 0; i < chunksCount; i++)
        {
            // キー
            Vector2Int key = dataReader.GetVector2Int();

            // 各チャンクのロード処理
            Chunk chunk = new Chunk();
            chunk.Reader(dataReader);
            MapManager.Instance.map[key] = chunk;
            MapManager.Instance.SetTilemap(key);
        }
        #endregion
    }
}
