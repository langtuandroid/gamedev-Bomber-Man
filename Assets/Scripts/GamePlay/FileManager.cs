using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace XX
{
    public class FileManager : MonoBehaviour
    {
        public static string KEY_CURRENT_LEVEL = "CURRENT_LEVEL";

        private void Awake()
        {
            //DelLevel();
            InitializeFile();
        }


        public static void UpdateLevel(int levelId)
        {
            var allLevelComplete = GetAllLevelComplete();
            allLevelComplete[levelId - 1].mCompleted = true;
            PlayerPrefs.SetInt(KEY_CURRENT_LEVEL, levelId);
            PlayerPrefs.Save();
            try
            {
                var path = Application.persistentDataPath + "/LevelManager.dat";
                var binaryFormatter = new BinaryFormatter();
                FileStream fileStream = null;
                fileStream = File.Open(path, FileMode.Open);
                binaryFormatter.Serialize(fileStream, allLevelComplete);
                fileStream.Close();
            }
            catch (IOException ex)
            {
                print("error: " + ex);
            }
        }

        private static List<LevelComplete> GetAllLevel()
        {
            var allLevelComplete = GetAllLevelComplete();
            try
            {
                var path = Application.persistentDataPath + "/LevelManager.dat";
                var binaryFormatter = new BinaryFormatter();
                FileStream fileStream = null;
                fileStream = File.Open(path, FileMode.Open);
                binaryFormatter.Serialize(fileStream, allLevelComplete);
                fileStream.Close();
            }
            catch (IOException ex)
            {
                print("err: " + ex);
            }

            return allLevelComplete;
        }

        private void InitializeFile()
        {
            try
            {
                PlayerPrefs.SetInt(KEY_CURRENT_LEVEL, 1);
                PlayerPrefs.Save();
                var path = Application.persistentDataPath + "/LevelManager.dat";
                var binaryFormatter = new BinaryFormatter();
                var list = new List<LevelComplete>();
                FileStream fileStream = null;
                if (!File.Exists(path))
                {
                    fileStream = File.Create(path);
                    list.Add(new LevelComplete(1, true));
                    list.Add(new LevelComplete(2, false));
                    list.Add(new LevelComplete(3, false));
                    list.Add(new LevelComplete(4, false));
                    list.Add(new LevelComplete(5, false));
                    list.Add(new LevelComplete(6, false));
                    list.Add(new LevelComplete(7, false));
                    list.Add(new LevelComplete(8, false));
                    list.Add(new LevelComplete(9, false));
                    list.Add(new LevelComplete(10, false));
                    list.Add(new LevelComplete(11, false));
                    list.Add(new LevelComplete(12, false));
                    list.Add(new LevelComplete(13, false));
                    list.Add(new LevelComplete(14, false));
                    list.Add(new LevelComplete(15, false));
                    list.Add(new LevelComplete(16, false));
                    list.Add(new LevelComplete(17, false));
                    list.Add(new LevelComplete(18, false));
                    list.Add(new LevelComplete(19, false));
                    list.Add(new LevelComplete(20, false));
                    list.Add(new LevelComplete(21, false));
                    list.Add(new LevelComplete(22, false));
                    list.Add(new LevelComplete(23, false));
                    list.Add(new LevelComplete(24, false));
                    list.Add(new LevelComplete(25, false));
                    list.Add(new LevelComplete(26, false));
                    list.Add(new LevelComplete(27, false));
                    list.Add(new LevelComplete(28, false));
                    list.Add(new LevelComplete(29, false));
                    list.Add(new LevelComplete(30, false));
                    list.Add(new LevelComplete(31, false));
                    list.Add(new LevelComplete(32, false));
                    list.Add(new LevelComplete(33, false));
                    list.Add(new LevelComplete(34, false));
                    list.Add(new LevelComplete(35, false));
                    list.Add(new LevelComplete(36, false));
                    list.Add(new LevelComplete(37, false));
                    list.Add(new LevelComplete(38, false));
                    list.Add(new LevelComplete(39, false));
                    list.Add(new LevelComplete(40, false));
                    list.Add(new LevelComplete(41, false));
                    list.Add(new LevelComplete(42, false));
                    list.Add(new LevelComplete(43, false));
                    list.Add(new LevelComplete(44, false));
                    list.Add(new LevelComplete(45, false));
                    list.Add(new LevelComplete(46, false));
                    list.Add(new LevelComplete(47, false));
                    list.Add(new LevelComplete(48, false));
                    list.Add(new LevelComplete(49, false));
                    list.Add(new LevelComplete(50, false));
                    list.Add(new LevelComplete(51, false));
                    list.Add(new LevelComplete(52, false));
                    list.Add(new LevelComplete(53, false));
                    list.Add(new LevelComplete(54, false));
                    list.Add(new LevelComplete(55, false));
                    list.Add(new LevelComplete(56, false));
                    list.Add(new LevelComplete(57, false));
                    list.Add(new LevelComplete(58, false));
                    list.Add(new LevelComplete(59, false));
                    list.Add(new LevelComplete(60, false));
                    binaryFormatter.Serialize(fileStream, list);
                    fileStream.Close();
                }
            }
            catch (IOException message)
            {
                Debug.Log(message);
            }
        }

        private void DelLevel()
        {
            if (File.Exists(Application.persistentDataPath + "/LevelManager.dat"))
                File.Delete(Application.persistentDataPath + "/LevelManager.dat");
        }

        public static List<LevelComplete> GetAllLevelComplete()
        {
            try
            {
                var path = Application.persistentDataPath + "/LevelManager.dat";
                var binaryFormatter = new BinaryFormatter();
                var list = new List<LevelComplete>();
                FileStream fileStream = null;
                fileStream = File.Open(path, FileMode.Open);
                list = (List<LevelComplete>)binaryFormatter.Deserialize(fileStream);
                fileStream.Close();
                return list;
            }
            catch (IOException)
            {
                return new List<LevelComplete>();
            }
        }
    }
}