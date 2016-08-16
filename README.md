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
