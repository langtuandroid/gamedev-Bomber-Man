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
			InitializeFile();
		}

		private void Start()
		{
		}

		public static void UpdateLevel(int levelId)
		{
			List<LevelComplete> allLevelComplete = GetAllLevelComplete();
			allLevelComplete[levelId - 1].mCompleted = true;
			PlayerPrefs.SetInt(KEY_CURRENT_LEVEL, levelId);
			PlayerPrefs.Save();
			try
			{
				string path = Application.persistentDataPath + "/LevelManager.dat";
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = null;
				fileStream = File.Open(path, FileMode.Open);
				binaryFormatter.Serialize(fileStream, allLevelComplete);
				fileStream.Close();
			}
			catch (IOException ex)
			{
				MonoBehaviour.print("error: " + ex.ToString());
			}
		}

		private static List<LevelComplete> GetAllLevel()
		{
			List<LevelComplete> allLevelComplete = GetAllLevelComplete();
			try
			{
				string path = Application.persistentDataPath + "/LevelManager.dat";
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				FileStream fileStream = null;
				fileStream = File.Open(path, FileMode.Open);
				binaryFormatter.Serialize(fileStream, allLevelComplete);
				fileStream.Close();
			}
			catch (IOException ex)
			{
				MonoBehaviour.print("err: " + ex.ToString());
			}
			return allLevelComplete;
		}

		private void InitializeFile()
		{
			try
			{
				PlayerPrefs.SetInt(KEY_CURRENT_LEVEL, 1);
				PlayerPrefs.Save();
				string path = Application.persistentDataPath + "/LevelManager.dat";
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				List<LevelComplete> list = new List<LevelComplete>();
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
			{
				File.Delete(Application.persistentDataPath + "/LevelManager.dat");
			}
		}

		public static List<LevelComplete> GetAllLevelComplete()
		{
			try
			{
				string path = Application.persistentDataPath + "/LevelManager.dat";
				BinaryFormatter binaryFormatter = new BinaryFormatter();
				List<LevelComplete> list = new List<LevelComplete>();
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
