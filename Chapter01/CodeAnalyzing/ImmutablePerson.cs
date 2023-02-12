// <copyright file="ImmutablePerson.cs" company="Packt">
// Copyright (c) Packt. All rights reserved.
// </copyright>

namespace CodeAnalyzing;

/// <summary>
/// Record types and init-only setters.
/// </summary>
public class ImmutablePerson
{
    /// <summary>
    /// Gets first name.
    /// </summary>
#nullable enable
    public string? FirstName { get; init; }

    /// <summary>
    /// Gets last name.
    /// </summary>
#nullable enable
    public string? LastName { get; init; }

    public void Dance()
    {
        _ = DateTime.Now;
        Console.WriteLine("Dance");
    }
}