using Machine.Specifications;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.BotSpecs
{
	[Subject("Move Strategy")]
	[Ignore]
	public class when_the_bot_has_two_horizontal_in_a_row : MoveContext
	{
		Establish context = () =>
		{
			_game = new Game(_bot, _opponent, _opponent);
		};

		Because of = () =>
		{
			_game.PlayMove(2, 2, _opponent);
			_game.PlayMove(1, 2, _opponent);
			_game.PlayMove(0, 1, _bot);
			_game.PlayMove(2, 1, _bot);
		};

		It should_play_the_third_and_win = () => 
			_game.Board[1, 1].ShouldEqual(_bot);

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}
}
