using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.BotSpecs
{

	/// <summary>
	///1. Win: If you have two in a row, play the third to get three in a row.
	///2. Block: If the opponent has two in a row, play the third to block them.
	///3. Fork: Create an opportunity where you can win in two ways.
	///4. Block Opponent's Fork:
	///       Option 1: Create two in a row to force the opponent into defending, as long as it doesn't result in them creating a fork or winning. For example, if "X" has a corner, "O" has the center, and "X" has the opposite corner as well, "O" must not play a corner in order to win. (Playing a corner in this scenario creates a fork for "X" to win.)
	///       Option 2: If there is a configuration where the opponent can fork, block that fork.
	///5. Center: Play the center.
	///6. Opposite Corner: If the opponent is in the corner, play the opposite corner.
	///7. Empty Corner: Play in a corner square.
	///8. Empty Side: Play in a middle square on any of the 4 sides.
	/// </summary>
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
			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_and_win = () => 
			_game.AssertWasCalled(game=>game.PlayMove(1,1,_bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Move Strategy")]
	public class when_the_bot_has_two_vertical_in_a_row : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[1, 1] = _bot;
			_game.TestableBoard[2, 2] = _opponent;
			_game.TestableBoard[1, 2] = _bot;
			_game.TestableBoard[0, 2] = _opponent;

			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_and_win = () =>
			_game.AssertWasCalled(game => game.PlayMove(1, 0, _bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Move Strategy")]
	public class when_the_bot_has_two_in_a_row_diagonally : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[0, 0] = _bot;
			_game.TestableBoard[1, 2] = _opponent;
			_game.TestableBoard[1, 1] = _bot;
			_game.TestableBoard[1, 0] = _opponent;

			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_and_win = () =>
			_game.AssertWasCalled(game => game.PlayMove(2, 2, _bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}
}
