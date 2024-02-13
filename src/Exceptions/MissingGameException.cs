/* Author:  Leonardo Trevisan Silio
 * Date:    12/02/2024
 */
using System;

namespace FromSky.Exceptions;

/// <summary>
/// Represents a error throwed when a empty game is used.
/// </summary>
public class MissingGameException : Exception
{
    public override string Message =>
        $"The game do not started yet. Use Game<T>.New() to init.";
}