/* Author:  Leonardo Trevisan Silio
 * Date:    12/02/2024
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace FromSky;

using Exceptions;

/// <summary>
/// Represents a game with GameData type internal information.
/// </summary>
public class Game<T>
    where T : GameData, new()
{
    private static Game<T> current = null;
    public static Game<T> Current => current;

    public static string GameMainFolder { get; set; } 
        = AppDomain.CurrentDomain.BaseDirectory;

    public static void New()
        => current = new();

    public static async Task Save(bool editable = false)
    {
        var path = getSaveFile(GameMainFolder, editable);
        var json = JsonSerializer.Serialize(Current.Data);
        if (editable)
        {
            await File.WriteAllTextAsync(path, json);
            return;
        }
        
        var jsonBytes = Encoding.UTF32.GetBytes(json);
        var payload = Convert.ToBase64String(jsonBytes);

        var key = getSecurityKey();
        using var ms = new MemoryStream(
            Encoding.UTF32.GetBytes(payload + key)
        );
        var signatureBytes = await SHA256.HashDataAsync(ms);
        var signature = Convert.ToBase64String(signatureBytes);
        
        var content = $"{payload}.{signature}";
        await File.WriteAllTextAsync(path, content);
    }

    public static void Load(string saveFileName)
    {
        if (!File.Exists(saveFileName))
            throw new MissingSaveException(saveFileName);
        var extension = Path.GetExtension(saveFileName);
        
        // TODO
    }

    private static string getSecurityKey()
    {
        // temporary
        const string key = "from-sky-cripto-key";
        return key;
    }

    private static string getSaveFolder(string folder)
    {
        var saveFolder = Path.Combine(folder, "saves");
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);
        return saveFolder;
    }

    private static void resetSaveFileName(string saveFolder, bool editable)
    {
        var files = Directory.GetFiles(saveFolder);
        var lastId = files
            .Select(Path.GetFileNameWithoutExtension)
            .Select(file => file.Replace("_", ""))
            .Select(int.Parse)
            .Max();
        
        var newId = (lastId + 1).ToString("00_000_000");
        var extension = editable ? ".fss" : ".rlfss";
        Current.SaveFileName = $"{newId}.{extension}";
    }

    private static string getSaveFile(string mainFolder, bool editable)
    {
        var saveFolder = getSaveFolder(mainFolder);
        if (Current.SaveFileName is null)
            resetSaveFileName(saveFolder, editable);
        var savePath = Path.Combine(saveFolder, Current.SaveFileName);
        return savePath;
    }
    
    public string SaveFileName { get; private set; }
    public T Data { get; set; }
}