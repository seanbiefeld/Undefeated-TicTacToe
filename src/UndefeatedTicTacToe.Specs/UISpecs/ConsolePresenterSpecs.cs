using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.ConsoleUI;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.UISpecs
{
	public class ConsolePresenterContext
	{
		protected static IConsoleView _view;
		protected static TestConsolePresenter _presenter;
	}

	public class TestConsolePresenter : ConsolePresenter
	{
		public TestConsolePresenter(IConsoleView view) : base(view)
		{
		}

		public Game TestableCurrentGame
		{
			get
			{
				return CurrentGame;
			}
			set
			{
				CurrentGame = value;
			}
		}
	}

	[Subject("UI Console")]
	public class when_the_game_begins : ConsolePresenterContext
	{
		Establish context = () =>
    	{
			_view = MockRepository.GenerateStub<IConsoleView>();
			_presenter = new TestConsolePresenter(_view);
    	};

		Because of = () =>
			_presenter.Initialize("bot");

		It should_set_game_over_to_false = () =>
			_view.GameOver.ShouldBeFalse();
	}

	[Subject("UI Console")]
	public class when_the_game_begins_and_no_valid_first_player_is_specified : ConsolePresenterContext
	{
		Establish context = () =>
		{
			_view = MockRepository.GenerateStub<IConsoleView>();
			_presenter = new TestConsolePresenter(_view);
		};

		Because of = () => _presenter.Initialize("");

		It should_notify_the_user_to_specify_a_valid_user = () =>
			_view.AssertWasCalled(view=>view.ShowSpecifyUserMessage());

		It should_not_create_a_game = () => 
			_presenter.TestableCurrentGame.ShouldBeNull();

		It should_validate_name = () => 
			_view.ValidFirstPlayer.ShouldBeFalse();
	}

	[Subject("UI Console")]
	public class when_the_game_begins_and_the_player_is_specified : ConsolePresenterContext
	{
		Establish context = () =>
		{
			_view = MockRepository.GenerateStub<IConsoleView>();
			_presenter = new TestConsolePresenter(_view);
		};

		Because of = () => _presenter.Initialize("me");

		It should_create_the_game = () =>
			_presenter.TestableCurrentGame.ShouldNotBeNull();

		It should_validate_name = () =>
			_view.ValidFirstPlayer.ShouldBeTrue();
	}

	[Subject("UI Console")]
	public class when_the_game_begins_and_the_bot_is_specified : ConsolePresenterContext
	{
		Establish context = () =>
		{
			_view = MockRepository.GenerateStub<IConsoleView>();
			_presenter = new TestConsolePresenter(_view);
		};

		Because of = () => _presenter.Initialize("bot");

		It should_create_the_game = () =>
			_presenter.TestableCurrentGame.ShouldNotBeNull();

		It should_validate_name = () =>
			_view.ValidFirstPlayer.ShouldBeTrue();
	}
}