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

	public class ConsolePresenter
	{
		readonly IConsoleView _view;
		readonly Bot _bot;
		readonly IPlayer _player;
		readonly string PlayerName = "me";
		readonly string BotName = "bot";

		protected Game CurrentGame { get; set; }

		public ConsolePresenter(IConsoleView view)
		{
			_view = view;
			_bot = new Bot();
			_player = new Player();
		}

		public void Initialize(string firstPlayerName)
		{
			if (firstPlayerName.ToLower() == PlayerName.ToLower())
				NewGame(_player, _bot, _player);
			else if (firstPlayerName.ToLower() == BotName.ToLower())
				NewGame(_bot, _player, _bot);
			else
				_view.ShowSpecifyUserMessage();
		}

		void NewGame(IPlayer somePlayer, IPlayer someOtherPlayer, IPlayer firstPlayer)
		{
			CurrentGame = new Game(somePlayer, someOtherPlayer, firstPlayer);
			_view.GameOver = CurrentGame.Over;
		}
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

	public interface IConsoleView
	{
		void ShowSpecifyUserMessage();
		bool GameOver { get; set; }
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
	}
}