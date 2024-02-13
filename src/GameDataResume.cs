/* Author:  Leonardo Trevisan Silio
 * Date:    12/02/2024
 */
using System.Collections.Generic;
using System.Reflection;

namespace FromSky;

/// <summary>
/// A resume of save.
/// </summary>
public class GameDataResume
{
    public string SaveFileName { get; private set; }
    public Dictionary<string, object> ResumeData { get; private set; }

    public static GameDataResume GetResume<T>(T data, string saveFileName)
        where T : GameData
    {
        var resume = new GameDataResume
        {
            SaveFileName = saveFileName,
            ResumeData = new()
        };

        foreach (var prop in typeof(T).GetProperties())
        {
            var inResume = prop.GetCustomAttributes<SaveResumeAttribute>();
            if (inResume is null)
                continue;
            
            resume.ResumeData.Add(
                prop.Name,
                prop.GetValue(data)
            );
        }
        return resume;
    }
}