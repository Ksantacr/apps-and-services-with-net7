// <copyright file="Program.cs" company="Packt">
// Copyright (c) Packt. All rights reserved.
// </copyright>

#nullable disable
using System.Diagnostics;

namespace CodeAnalyzing;

/// <summary>
/// The main class for this console app.
/// </summary>
public class Program
{
    /// <summary>
    /// The main entry point for this console app.
    /// </summary>
    /// <param name="args">
    /// A string array of arguments passed to the console app.
    /// </param>
    public static void Main(string[] args)
    {
        string nombre = null;
        ImmutablePerson jeff = new()
        {
            FirstName = "Jeff", // allowed
            LastName = "Winger",
        };

        // jeff.FirstName = "Geoff"; <--- not allowed
        Debug.WriteLine("Hello, Debugger!", nombre);

        WriteLine(DateTime.Now.ToShortTimeString());

        InmutableAnimal animal = new("name", "specie");

        Hire();

        float value = 10.2f;
        var sum = 10.1f + value;

        WriteLine("result:" + sum);

        // Raw string literals


    }

    public static void Hire(string manager = null, string name = null)
    {
        //if (manager == null)
        //{
        //    throw new ArgumentNullException(nameof(manager));
        //}
        //if (name == null)
        //{
        //    throw new ArgumentNullException(nameof(manager));
        //}

        // With .NET6
        //ArgumentNullException.ThrowIfNull(manager);
        //ArgumentNullException.ThrowIfNull(name);
    }

    // c# 11 preview
    //public static void Hire(Person manager!!, Person employee!!)
    //{
    //    Console.WriteLine(manager, name);
    //}
}

public record InmutableAnimal(string Name, string Species);


/*
 Exercise 1.1 – Test your knowledge
Use the web to answer the following questions:
1. Why is it good practice to add the following setting to your project files? And when should 
you not set it?
<TreatWarningsAsErrors>true</TreatWarningsAsErrors>
Warnings are given for a reason, so ignoring warnings encourages poor development practice.
The exceptions are the gRPC projects

2. Which service technology requires a minimum HTTP version of 2?
gRPC

3. In 2010, your organization created a service using .NET Framework and Windows Communication Foundation. What is the best technology to migrate it to and why?
Technologies like WCF allow for the building of distributed applications. A client application can make 
remote procedure calls (RPCs) to a server application. Instead of using a port of WCF to do this, we 
should use an alternative RPC technology like gRPC

4. Which code editor or IDE should you install for .NET development?
Visual Studio Code, Visual Studio 2022, Rider

5. What should you be aware of when creating Azure resources?
Only use what you need

6. Which type of .NET release is higher quality, STS or LTS?
LTS because Long Term Support releases are stable and require fewer updates over their lifetime. 
These are a good choice for applications that you do not intend to update frequently.
In other hand Standard Term Support (STS) releases include features that may change based on feedback.

7. In new .NET projects, nullable checks are enabled. What are two ways to disable them?
Add  <Nullable>disable</Nullable> in the csproj or #nullable enable in the file where you need it

8. If you define any types in a top-level program, where must they go in the Program.cs file?
If you declare any classes or other types, they must go at the bottom of the file.

9. How do you import a class like Console so that its static members like WriteLine are available 
in all code files throughout a project?
Set <ImplicitUsings>enable</ImplicitUsings> in .csproj and
<ItemGroup>
    <Using Include="System.Console" Static="true" />
</ItemGroup>

10. What is the best new C# 11 language feature?
- Require properties to be set during the instantiation
- Generic math support
- Raw string literals
- You can define generic attributes.

 */