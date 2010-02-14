using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.ConsoleUI
{
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
			{
				NewGame(_player, _bot, _player);
				_view.ValidFirstPlayer = true;
			}
			else if (firstPlayerName.ToLower() == BotName.ToLower())
			{
				NewGame(_bot, _player, _bot);
				_view.ValidFirstPlayer = true;
			}
			else
			{
				_view.ShowSpecifyUserMessage();
				_view.ValidFirstPlayer = false;
			}
		}

		void NewGame(IPlayer somePlayer, IPlayer someOtherPlayer, IPlayer firstPlayer)
		{
			CurrentGame = new Game(somePlayer, someOtherPlayer, firstPlayer);
			_view.GameOver = CurrentGame.Over;
		}
	}
}