using Machine.Specifications;
using Rhino.Mocks;

namespace UndefeatedTicTacToe.Specs.BotSpecs
{
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
	public class when_the_bot_has_two_in_a_row_diagonally_from_bottom_right_to_top_left : MoveStrategyContext
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

	[Subject("Move Strategy")]
	public class when_the_bot_has_two_in_a_row_diagonally_from_bottom_left_to_top_right : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[2, 0] = _bot;
			_game.TestableBoard[1, 2] = _opponent;
			_game.TestableBoard[1, 1] = _bot;
			_game.TestableBoard[1, 0] = _opponent;

			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_and_win = () =>
			_game.AssertWasCalled(game => game.PlayMove(0, 2, _bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Move Strategy")]
	public class when_the_opponent_has_two_horizontal_in_a_row : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[2, 2] = _bot;
			_game.TestableBoard[0, 1] = _opponent;
			_game.TestableBoard[1, 0] = _bot;
			_game.TestableBoard[2, 1] = _opponent;
			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_to_block_opponent_win = () =>
			_game.AssertWasCalled(game => game.PlayMove(1, 1, _bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Move Strategy")]
	public class when_the_opponent_has_two_vertical_in_a_row : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[2, 2] = _bot;
			_game.TestableBoard[1, 1] = _opponent;
			_game.TestableBoard[0, 1] = _bot;
			_game.TestableBoard[1, 2] = _opponent;

			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_to_block_opponent_win = () =>
			_game.AssertWasCalled(game => game.PlayMove(1, 0, _bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Move Strategy")]
	public class when_the_opponent_has_two_in_a_row_diagonally_from_bottom_right_to_top_left : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[1, 0] = _bot;
			_game.TestableBoard[0, 0] = _opponent;
			_game.TestableBoard[2, 0] = _bot;
			_game.TestableBoard[1, 1] = _opponent;

			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_to_block_opponent_win = () =>
			_game.AssertWasCalled(game => game.PlayMove(2, 2, _bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Move Strategy")]
	public class when_the_opponent_has_two_in_a_row_diagonally_from_bottom_left_to_top_right : MoveStrategyContext
	{
		Establish context = () =>
		{
			_game.TestableBoard[2, 2] = _bot;
			_game.TestableBoard[2, 0] = _opponent;
			_game.TestableBoard[0, 1] = _bot;
			_game.TestableBoard[1, 1] = _opponent;

			_numberOfMoves = 2;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		Because of = () => _bot.MakeMove(_game);

		It should_play_the_third_to_block_opponent_win = () =>
			_game.AssertWasCalled(game => game.PlayMove(0, 2, _bot));

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}
}