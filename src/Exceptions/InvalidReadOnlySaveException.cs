/* Author:  Leonardo Trevisan Silio
 * Date:    12/02/2024
 */
using System;

namespace FromSky.Exceptions;

/// <summary>
/// Represents a error throwed when it's tried to load from a file with invalid format.
/// </summary>
public class InvalidReadOnlySaveException(string path) : Exception
{
    public override string Message =>
        $"The save in file {path} has a not valid format.";
}