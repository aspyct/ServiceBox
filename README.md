Usage example
===

```
class PlayerServiceBox : ServiceBox
{
    [Provider(singleton: true)]
    public IPlayerManager MakePlayerManager()
    {
        return CreateAndInject<PlayerManager>();
    }

    [Provider]
    public IViewModel MakeViewModel()
    {
        return CreateAndInject<ViewModel>();
    }
}

class ViewModel : IViewModel
{
    [Injected]
    public IPlayerManager Service { get; set; }

    public void PlayButtonTapped()
    {
        Service.Play();
    }
}
```

Why "Yet Another" library?
===

Most of the dependency injections frameworks in .Net use the constructor to inject dependencies.

This results in a constructor bloated with arguments.
Also, everytime you need one more dependency, some code and tests need to be modified to fit new constructor.

And last but not least, that was fun :)
