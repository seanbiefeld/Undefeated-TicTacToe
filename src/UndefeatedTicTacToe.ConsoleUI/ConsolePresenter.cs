using System;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.ConsoleUI
{
	public class ConsolePresenter
	{
		readonly IConsoleView _view;
		readonly Bot _bot;
		readonly Player _player;
		readonly string PlayerName = "me";
		readonly string BotName = "bot";
		
		protected Game CurrentGame { get; set; }

		public ConsolePresenter(IConsoleView view)
		{
			_view = view;
			_bot = new Bot();
			_player = new Player();
			_player.MoveNeeded += MakeMove;
		}

		Coordinate MakeMove(IGame game)
		{
			_view.ShowBoard(game.Board);

			bool invalidMove = true;

			int xCoord = 0;
			int yCoord = 0;

			while (invalidMove)
			{
				_view.RetrievePlayersMove();
				string playersMove = _view.PlayersMove;

				if(playersMove.Contains(","))
				{
					string[] coordinates = playersMove.Split(',');

					if(coordinates.Length == 2)
					{
						string xCoordString = coordinates[0];
						string yCoordString = coordinates[1];

						if(IsValidCoordinate(xCoordString, out xCoord) && IsValidCoordinate(yCoordString, out yCoord))
						{
							if(game.MoveIsValid(xCoord, yCoord))
								invalidMove = false;
						}
							
					}
				}
			}

			return new Coordinate(xCoord,yCoord);
		}

		static bool IsValidCoordinate(string stringCoord, out int intCoord)
		{
			bool isValid = false;
			
			if (int.TryParse(stringCoord, out intCoord))
				isValid = (intCoord > -1 && intCoord < 3);

			return isValid;
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
			CurrentGame = new Game(somePlayer, someOtherPlayer, firstPlayer, GameOverHandler);
		}

		void GameOverHandler(IGame game)
		{
			_view.ShowBoard(game.Board);

			if (game.Over && !game.Draw)
				_view.ShowGameOverMessage(game);
			else
				_view.ShowDrawMessage(game);

			_view.ShowPlayAgainMessage();

			if(UserWantsToPlayAgain(_view.PlayAgain))
				_view.StartNewGame();
			else
				_view.EndGame();
		}

		bool UserWantsToPlayAgain(string playAgain)
		{
			if(playAgain.ToLower() == "yes")
				return true;

			return false;
		}
	}
}