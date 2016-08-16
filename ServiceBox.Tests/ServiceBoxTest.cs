using NUnit.Framework;
using System;
using System.Diagnostics;

namespace ServiceBox.Tests
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

			IViewModel viewModel = subServiceBox.Get<IViewModel>();
			Assert.IsInstanceOf<ViewModel>(viewModel);
		}
	}

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

	class PlayerServiceBoxChild : PlayerServiceBox
	{
		[Provider]
		public ISubDep MakeSubDep()
		{
			return CreateAndInject<SubDep>();
		}
	}

	interface ISubDep
	{

	}

	class SubDep : ISubDep
	{

	}
}

