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
		

		public static void UpdateLevel(int levelId)
		{
			List<LevelComplete> allLevelComplete = GetAllLevelComplete();
			//List<LevelComplete> allLevelComplete = GetAllLevel();
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
					list.Add(new LevelComplete(2, true));
					list.Add(new LevelComplete(3, true));
					list.Add(new LevelComplete(4, true));
					list.Add(new LevelComplete(5, true));
					list.Add(new LevelComplete(6, true));
					list.Add(new LevelComplete(7, true));
					list.Add(new LevelComplete(8, true));
					list.Add(new LevelComplete(9, true));
					list.Add(new LevelComplete(10, true));
					list.Add(new LevelComplete(11, true));
					list.Add(new LevelComplete(12, true));
					list.Add(new LevelComplete(13, true));
					list.Add(new LevelComplete(14, true));
					list.Add(new LevelComplete(15, true));
					list.Add(new LevelComplete(16, true));
					list.Add(new LevelComplete(17, true));
					list.Add(new LevelComplete(18, true));
					list.Add(new LevelComplete(19, true));
					list.Add(new LevelComplete(20, true));
					list.Add(new LevelComplete(21, true));
					list.Add(new LevelComplete(22, true));
					list.Add(new LevelComplete(23, true));
					list.Add(new LevelComplete(24, true));
					list.Add(new LevelComplete(25, true));
					list.Add(new LevelComplete(26, true));
					list.Add(new LevelComplete(27, true));
					list.Add(new LevelComplete(28, true));
					list.Add(new LevelComplete(29, true));
					list.Add(new LevelComplete(30, true));
					list.Add(new LevelComplete(31, true));
					list.Add(new LevelComplete(32, true));
					list.Add(new LevelComplete(33, true));
					list.Add(new LevelComplete(34, true));
					list.Add(new LevelComplete(35, true));
					list.Add(new LevelComplete(36, true));
					list.Add(new LevelComplete(37, true));
					list.Add(new LevelComplete(38, true));
					list.Add(new LevelComplete(39, true));
					list.Add(new LevelComplete(40, true));
					list.Add(new LevelComplete(41, true));
					list.Add(new LevelComplete(42, true));
					list.Add(new LevelComplete(43, true));
					list.Add(new LevelComplete(44, true));
					list.Add(new LevelComplete(45, true));
					list.Add(new LevelComplete(46, true));
					list.Add(new LevelComplete(47, true));
					list.Add(new LevelComplete(48, true));
					list.Add(new LevelComplete(49, true));
					list.Add(new LevelComplete(50, true));
					list.Add(new LevelComplete(51, true));
					list.Add(new LevelComplete(52, true));
					list.Add(new LevelComplete(53, true));
					list.Add(new LevelComplete(54, true));
					list.Add(new LevelComplete(55, true));
					list.Add(new LevelComplete(56, true));
					list.Add(new LevelComplete(57, true));
					list.Add(new LevelComplete(58, true));
					list.Add(new LevelComplete(59, true));
					list.Add(new LevelComplete(60, true));
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
