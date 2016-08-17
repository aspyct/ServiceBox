using NUnit.Framework;
using System;
using System.Diagnostics;

namespace Aspyct.ServiceBox.Tests
{
	[TestFixture]
	public class Test
	{
		PlayerServiceBox Instance;

		[SetUp]
		public void SetUp()
		{
			Instance = new PlayerServiceBox();
		}

		[Test]
		public void WorksWithMethodProviders()
		{
			var dependency = Instance.Get<IPlayerManager>();

			Assert.NotNull(dependency);
		}

		[Test]
		public void WorksWithAutoProviders()
		{
			var dependency = Instance.Get<IViewModel>();

			Assert.NotNull(dependency);
		}

		[Test]
		public void MakesOnlyOneSingleton()
		{
			var first = Instance.Get<IPlayerManager>();
			var second = Instance.Get<IPlayerManager>();

			Assert.AreSame(first, second, "This should be the same instance (singleton)");
		}

		[Test]
		public void MakesMultipleNonSingleton()
		{
			var first = Instance.Get<IViewModel>();
			var second = Instance.Get<IViewModel>();

			Assert.AreNotSame(first, second, "This shouldn't be a singleton");
		}

		[Test]
		public void ProvidesExpectedUnderlyingClass()
		{
			var playerManager = Instance.Get<IPlayerManager>();
			Assert.IsInstanceOf<PlayerManager>(playerManager);

			var viewModel = Instance.Get<IViewModel>();
			Assert.IsInstanceOf<ViewModel>(viewModel);
		}

		[Test]
		public void ProvidesFromSubclasses()
		{
			var subServiceBox = new PlayerServiceBoxChild();

			ISubDep subDep = subServiceBox.Get<ISubDep>();
			Assert.IsInstanceOf<SubDep>(subDep);
		}

		[Test]
		public void ProvidesFromSuperclasses()
		{
			var subServiceBox = new PlayerServiceBoxChild();

			IPlayerManager playerManager = subServiceBox.Get<IPlayerManager>();
			Assert.IsInstanceOf<PlayerManager>(playerManager);
		}

		[Test]
		public void PrefersProvidersFromSubclasses()
		{
			var subServiceBox = new PlayerServiceBoxChild();

			IViewModel viewModel = subServiceBox.Get<IViewModel>();
			Assert.IsInstanceOf<SpecializedViewModel>(viewModel);
		}
	}

	[AutoProvider(
		typeof(IViewModel),
		typeof(ViewModel)
	)]
	class PlayerServiceBox : Box
	{
		[Provider(singleton: true)]
		public IPlayerManager MakePlayerManager()
		{
			return CreateAndInject<PlayerManager>();
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

	interface IPlayerManager
	{
		void Play();
	}

	class PlayerManager : IPlayerManager
	{
		public void Play()
		{
			Debug.WriteLine("Aye, starrrrrting playback!");
		}
	}

	interface IViewModel
	{
		void PlayButtonTapped();
	}

	[AutoProvider(
		typeof(ISubDep),
		typeof(SubDep)
	)]
	[AutoProvider(
		typeof(IViewModel),
		typeof(SpecializedViewModel)
	)]
	class PlayerServiceBoxChild : PlayerServiceBox
	{
	}

	interface ISubDep
	{

	}

	class SubDep : ISubDep
	{

	}

	class SpecializedViewModel : IViewModel
	{
		public void PlayButtonTapped()
		{
			
		}
	}
}

