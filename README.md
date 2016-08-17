Usage example
===

```csharp
/*
 * First, define your dependencies
 */
[AutoProvider(
    typeof(IMessageService), // the interface you want to provide
    typeof(DefaultMessageService), // the implementing class
    singleton: true // this must be a singleton
)]
public class BaseConfiguration : Box
{
    // Or you can also define a method provider
    [Provider]
    public IGreeter MakeGreeter()
    {
        return CreateAndInject<Greeter>();
    }
}

/*
 * Then, define stuff that requires these dependencies
 */
public class Greeter : IGreeter
{
    [Injected]
    public IMessageService MessageService { get; set; }

    public void Greet()
    {
        Console.WriteLine(MessageService.GetMessage());
    }
}

/*
 * And lastly, get your services when you need them
 */
var configuration = new HelloWorldConfiguration();

var greeter = configuration.Get<IGreeter>();

greeter.Greet();
```

Clone this project and have a look at the `ServiceBox.Sample` project for more examples and details.

Why "Yet Another" library?
===

Most of the dependency injections frameworks in .Net use the constructor to inject dependencies.

This results in a constructor bloated with arguments.
Also, everytime you need one more dependency, some code and tests need to be modified to fit new constructor.

And last but not least, that was fun :)
