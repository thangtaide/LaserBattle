using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveDataUtil
{
    public static bool ExistsFile(string name)
    {
        return File.Exists(FileNameToPath(name));
    }

    public static string FileNameToPath(string fileName)
    {
        string path = Path.Combine(Application.persistentDataPath, fileName);
        Debug.Log("path : "+ path);
        return path;
    }

    public static void SaveData<T>(string fileName, object data, bool isDefault = true)
    {
        try
        {
            if (data == null) return;
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            var path = isDefault ? FileNameToPath(fileName + ".txt") : (fileName + ".txt");
            using (FileStream file = File.Open(path, FileMode.OpenOrCreate))
            {
                binaryFormatter.Serialize(file, (T)data);
                file.Close();
                Debug.Log("[SaveData] Done: " + fileName + " " + DateTime.Now + "\n" + path);
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[SaveData] Exception: " + fileName + " " + ex.Message);
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }

    public static object LoadData<T>(string fileName, bool isDefault = true)
    {
        try
        {
            var data = default(T);
            var path = isDefault ? FileNameToPath(fileName + ".txt") : (fileName + ".txt");
            if (File.Exists(path))
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                using (FileStream file = File.Open(path, FileMode.Open))
                {
                    data = (T)binaryFormatter.Deserialize(file);
                    file.Close();
                    Debug.Log("[LoadData] Done: " + fileName);
                }

                return data;
            }
            else
            {
                Debug.LogWarning("[LoadData] Error " + fileName + " " + "NOT found");
                return null;
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[LoadData]: Exception: " + fileName + " " + ex.Message);
            return null;
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }

    public static void DeleteData(string fileName)
    {
        try
        {
            var path = FileNameToPath(fileName + ".txt");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            else
            {
                Debug.LogWarning("[DeleteData] Error " + fileName + " " + "NOT found");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("[DeleteData]: Exception: " + fileName + " " + ex.Message);
        }
        finally
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
    }
}
