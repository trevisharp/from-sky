/* Author:  Leonardo Trevisan Silio
 * Date:    12/02/2024
 */
using System;
using System.IO;

namespace FromSky.Exceptions;

/// <summary>
/// Represents a error throwed when it's tried to load from a file with invalid extension.
/// </summary>
public class InvalidExtensionException(string path) : Exception
{
    public override string Message =>
        $"""
        The save in file {path} has a invalid extension ({Path.GetExtension(path)}).
        The only valid extensions are .fss and .rlfss.
        """;
}