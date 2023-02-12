// <copyright file="Program.cs" company="Packt">
// Copyright (c) Packt. All rights reserved.
// </copyright>

#nullable disable
global using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace CodeAnalyzing;

public class Book
{
    // required word to force to init this value
    /*
         Error CS9035 Required member 'Book.Isbn' must be set in the object 
        initializer or attribute constructor.
     */
    //public required string Isbn { get; set; }
    public string? Title { get; set; }
}