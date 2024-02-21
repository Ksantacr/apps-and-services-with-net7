

namespace WorkingWithReflection.Shared;

public class Animal
{
    [Coder("Kevin Santacruz", "21/02/2024")]
    [Coder("Kevin Santacruz", "20/02/2024")]
    [Obsolete($"use {nameof(SpeakBetter)} instead.")]
    public void Speak() {

        WriteLine("Woof");

    }

    public void SpeakBetter()
    {
        WriteLine("Wooooooooof...");
    }
}
