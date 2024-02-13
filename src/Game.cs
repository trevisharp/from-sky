/* Author:  Leonardo Trevisan Silio
 * Date:    13/02/2024
 */
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
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

    /// <summary>
    /// Get the current game instance.
    /// </summary>
    public static Game<T> Current => current;

    /// <summary>
    /// Get or set the path of Game files folder.
    /// </summary>
    public static string GameMainFolder { get; set; } 
        = AppDomain.CurrentDomain.BaseDirectory;

    /// <summary>
    /// Reset the game.
    /// </summary>
    public static void New()
        => current = new() { 
            SaveFileName = null,
            Data = new()
        };

    /// <summary>
    /// Save current game in saves folder. Is editable parameter is true (the 
    /// default is false) generate a editable json save.
    /// If the save do not exit, create a new save file.
    /// </summary>
    public static async Task Save(bool editable = false)
    {
        if (Current is null)
            throw new MissingGameException();

        var path = getSaveFile(GameMainFolder, editable);
        var json = JsonSerializer.Serialize(Current.Data);
        var content = editable ? json : await getReadOnlyContent(json);
        await File.WriteAllTextAsync(path, content);

        var resumePath = path + ".resume";
        var resumeJson = JsonSerializer.Serialize(
            GameDataResume.GetResume(Current.Data, Current.SaveFileName)
        );
        await File.WriteAllTextAsync(resumePath, resumeJson);
    }

    /// <summary>
    /// Load game the from a save in save folder in main game folder.
    /// </summary>
    public static async void Load(string saveFileName)
    {
        var saveFolder = getSaveFolder(GameMainFolder);
        var saveFilePath = Path.Combine(saveFolder, saveFileName);
        if (!File.Exists(saveFilePath))
            throw new MissingSaveException(saveFilePath);
        var extension = Path.GetExtension(saveFilePath);

        current = new()
        {
            SaveFileName = saveFileName,
            Data = extension switch
            {
                "rlfss" => await loadFromReadOnlyContent(saveFilePath),
                "fss" => await loadFromContent(saveFilePath),
                _ => throw new InvalidExtensionException(saveFilePath)
            }
        };
    }

    /// <summary>
    /// Disattach the game from the current save file.
    /// </summary>
    public static void Disattach()
    {
        if (Current is null)
            throw new MissingGameException();
        
        Current.SaveFileName = null;
    }

    /// <summary>
    /// Attach the game to a specific save file.
    /// </summary>
    public static void Attach(string saveFileName)
    {
        if (Current is null)
            throw new MissingGameException();
        
        Current.SaveFileName = saveFileName;
    }

    /// <summary>
    /// Attach the game to a specific save file.
    /// </summary>
    public static void Attach(GameDataResume resume)
    {
        if (Current is null)
            throw new MissingGameException();
        
        Current.SaveFileName = resume.SaveFileName;
    }

    /// <summary>
    /// Get all save resumes.
    /// </summary>
    public static async Task<IEnumerable<GameDataResume>> GetSaves()
    {
        var saveFolder = getSaveFolder(GameMainFolder);
        var files = Directory.GetFiles(saveFolder)
            .Where(file => Path.GetExtension(file) == ".resume");
        
        List<GameDataResume> resumes = [];
        foreach (var file in files)
        {
            var content = await File.ReadAllTextAsync(file);
            var obj = JsonSerializer.Deserialize<GameDataResume>(content);
            resumes.Add(obj);
        }

        return resumes;
    }

    private static async Task<T> loadFromReadOnlyContent(string path)
    {
        var content = await File.ReadAllTextAsync(path);
        var contentParts = content.Split('.');

        if (contentParts.Length != 2)
            throw new InvalidReadOnlySaveException(path);
        var payload = contentParts[0];
        var signature = contentParts[1];

        var expectedSignature = await getSignature(payload);
        if (signature != expectedSignature)
            throw new InvalidSignatureException(path);
        
        var json = getJsonFromReadOnlyContent(payload);
        return JsonSerializer.Deserialize<T>(json);
    }

    private static async Task<T> loadFromContent(string path)
    {
        var json = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<T>(json);
    }

    private static async Task<string> getSignature(string payload)
    {
        var key = getSecurityKey();
        using var ms = new MemoryStream(
            Encoding.UTF32.GetBytes(payload + key)
        );
        var signatureBytes = await SHA256.HashDataAsync(ms);
        return Convert.ToBase64String(signatureBytes);
    }

    private static async Task<string> getReadOnlyContent(string json)
    {
        var jsonBytes = Encoding.UTF8.GetBytes(json);
        var payload = Convert.ToBase64String(jsonBytes);
        var signature = await getSignature(payload);
        
        return $"{payload}.{signature}";
    }

    private static string getJsonFromReadOnlyContent(string content)
    {
        var contentBytes = Convert.FromBase64String(content);
        return Encoding.UTF8.GetString(contentBytes);
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
    
    /// <summary>
    /// Get the name of save file.
    /// </summary>
    public string SaveFileName { get; private set; }

    /// <summary>
    /// Get the game data.
    /// </summary>
    public T Data { get; private set; }
}