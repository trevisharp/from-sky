/* Date: 13/02/2024
 * Author: Leonardo Trevisan Silio
 */
namespace FromSky;

/// <summary>
/// Represents a game global data.
/// </summary>
public abstract class SaveData
{
    [SaveResume]
    public string SaveFileName { get; set; } = null;
}