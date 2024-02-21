## Exercise 5.1 - Test your knowledge

1.  What is the most downloaded third-party NuGet package of all time?

    newtonsoft.json

    [Nuget stats](https://www.nuget.org/stats)

2.  What method do you call on the ImageSharp Image class to make a change like resizing the image or replacing colors with grayscale?

    ```csharp
    image.Mutate(x => x.Resize(image.Width / 50, image.Height / 50 ));image.Mutate(x=> x.Grayscale());
    ```

3.  What is a key benefit of using Serilog for logging?

    Serilog offer a structured logging setup to capture and store log data.
    [Serilog Logging Framework: A Comprehensive Guide](https://dlcoder.medium.com/serilog-logging-framework-a-comprehensive-guide-4537989636c2)

4.  What is a Serilog sink?

    With sink we can define where and how logs are stored or displayed.

5.  Should you always use a package like AutoMapper to map between objects?

    AutoMapper is helpful in the following situations:

        - Complex object mapping
        - Maintaining consistency
        - Reducing boilerplate

    Also we can have other situation where is better avoid AutoMapper:

        - Simple mappings.
        - Domain-specific logic

6.  Which FluentAssertions method should you call to start a fluent assertion on a value?

    `object.Should()`

7.  Which FluentAssertions method should you call to assert that all items in a sequence conform to a condition, like a string item must have less than six characters?

    `items.Should().OnlyContain(item => item.Length < 6);`

8.  Which FluentValidation class should you inherit from to define a custom validator?

    `AbstractValidator<T>`

9.  With FluentValidation, how can you set a rule to only apply in certain conditions?

    `When()` and `Unless()`

    ```csharp
    RuleFor(customer => customer.Email)
        .EmailAddress()
        .When(customer => customer.OptInForEmails);
    ```

10. With QuestPDF, which interface must you implement to define a document for a PDF, and what methods of that interface must you implement?

    `IDocument`

    [QuestPDF Documentation](https://www.questpdf.com/getting-started.html)
