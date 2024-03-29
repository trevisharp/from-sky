/* Author:  Leonardo Trevisan Silio
 * Date:    12/02/2024
 */
using System;

namespace FromSky.Exceptions;

/// <summary>
/// Represents a error throwed when a save do not exits.
/// </summary>
public class MissingSaveException(string path) : Exception
{
    public override string Message =>
        $"The save in file {path} do not exists.";
}