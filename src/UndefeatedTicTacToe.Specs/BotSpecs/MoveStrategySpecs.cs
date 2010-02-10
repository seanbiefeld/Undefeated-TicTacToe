using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.BotSpecs
{
	public class MoveStrategyContext : MoveContext
	{
		protected new static TestableGame _game;

		public class TestableGame : Game
		{
			public TestableGame(IPlayer someIPlayer, IPlayer someOtherIPlayer, IPlayer firstIPlayer) : base(someIPlayer, someOtherIPlayer, firstIPlayer)
			{
			}

			public virtual IPlayer[,] TestableBoard
			{
				get
				{
					return Board;
				}
				set
				{
					Board = value;
				}
			}
		}

		Establish context = () =>
    	{
    		_game = MockRepository.GenerateMock<TestableGame>(_opponent, _bot, _opponent);
    	};
	}

	[Subject("Move strategy")]
	public class when_the_bot_cannot_win_block_fork_and_a_corner_is_open : MoveStrategyContext
	{
		private Establish context = () =>
		{
			_game.TestableBoard[1, 1] = _bot;
			_game.TestableBoard[0, 2] = _opponent;
			_game.TestableBoard[0, 0] = _bot;
			_game.TestableBoard[2, 2] = _opponent;
			_game.TestableBoard[1, 2] = _bot;
			_game.TestableBoard[1, 0] = _opponent;

			_numberOfMoves = 3;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		private Because of = () =>
			_bot.MakeMove(_game);

		private It should_play_the_opposite_corner = () =>
			_game.AssertWasCalled(game => game.PlayMove(2, 0, _bot));
	}

	[Subject("Move strategy")]
	public class when_the_bot_cannot_win_block_fork_and_corners_are_not_open_but_a_side_is_open : MoveStrategyContext
	{
		private Establish context = () =>
		{
			_game.TestableBoard[1, 1] = _bot;
			_game.TestableBoard[0, 2] = _opponent;
			_game.TestableBoard[0, 0] = _bot;
			_game.TestableBoard[2, 2] = _opponent;
			_game.TestableBoard[1, 2] = _bot;
			_game.TestableBoard[1, 0] = _opponent;
			_game.TestableBoard[2, 0] = _bot;
			_game.TestableBoard[0, 1] = _opponent;

			_numberOfMoves = 4;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		private Because of = () =>
			_bot.MakeMove(_game);

		private It should_play_the_open_side = () =>
			_game.AssertWasCalled(game => game.PlayMove(2, 1, _bot));
	}
}