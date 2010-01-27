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

	[Subject("Move Strategy")]
	public class when_the_bot_has_two_horizontal_in_a_row : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[0, 1] = _bot;
			_game.TestableBoard[2, 2] = _opponent;
			_game.TestableBoard[2, 1] = _bot;
			_game.TestableBoard[1, 2] = _opponent;

			_bot.TestableMovesPlayed = 2;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_and_win = () => 
			_game.AssertWasCalled(game=>game.PlayMove(1,1,_bot));

		It should_increment_moves_played = () => _bot.TestableMovesPlayed.ShouldEqual(3);
	}
}
