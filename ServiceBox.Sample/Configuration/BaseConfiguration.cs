using ServiceBox;

namespace ServiceBox.Sample
{
	// The easiest way is to use the AutoProvider attribute
	[AutoProvider(
		typeof(IMessageService), // the interface you want to provide
		typeof(DefaultMessageService), // the implementing class
		singleton: true // this must be a singleton
	)]
	public class BaseConfiguration : ServiceBox
	{
		// Or you can also define a method provider
		[Provider]
		public IGreeter MakeGreeter()
		{
			return CreateAndInject<Greeter>();

			// Or you could...
			// var greeter = new Greeter();
			// Inject(greeter);
			// return greeter;
		}
	}
}

