using Machine.Specifications;

namespace UndefeatedTicTacToe.Specs.GameSpecs
{
	public class GameOverContext : PlayMoveContext
	{

	}

	[Behaviors]
	public class game_is_over_and_some_player_won : GameOverContext
	{
		It should_end_the_game = () =>
			_game.Over.ShouldBeTrue();

		It should_declare_the_winner = () =>
			_game.Winner.ShouldEqual(_somePlayer);

		It should_declare_the_loser = () =>
			_game.Loser.ShouldEqual(_someOtherPlayer);
	}

	[Subject("Game Over")]
	public class when_getting_a_horizontal_three_in_a_row : GameOverContext
	{
		Establish context = () =>
		{
			_game.PlayMove(0, 1, _somePlayer);
			_game.PlayMove(2, 1, _somePlayer);
		};

		Because of = () =>
			_game.PlayMove(1, 1, _somePlayer);

		Behaves_like<game_is_over_and_some_player_won> game_should_be_over_some_player_should_have_won;
	}


	[Subject("Game Over")]
	public class when_getting_a_vertical_three_in_a_row : GameOverContext
	{
		Establish context = () =>
		{
			_game.PlayMove(1, 0, _somePlayer);
			_game.PlayMove(1, 2, _somePlayer);
		};

		Because of = () =>
			_game.PlayMove(1, 1, _somePlayer);

		Behaves_like<game_is_over_and_some_player_won> game_should_be_over_some_player_should_have_won;
	}

	[Subject("Game Over")]
	public class when_not_getting_three_in_a_row : GameOverContext
	{
		Establish context = () =>
		{
			_game.PlayMove(0, 1, _somePlayer);
			_game.PlayMove(0, 2, _somePlayer);
		};

		Because of = () =>
			_game.PlayMove(1,1, _somePlayer);

		It should_not_end_the_game = () =>
			_game.Over.ShouldBeFalse();
	}

	[Subject("Game Over")]
	public class when_getting_three_in_a_row_diagonally_from_bottom_left_to_top_right : GameOverContext
	{
		Establish context = () =>
		{
			_game.PlayMove(0, 0, _somePlayer);
			_game.PlayMove(2, 2, _somePlayer);
		};

		Because of = () =>
			_game.PlayMove(1,1,_somePlayer);

		Behaves_like<game_is_over_and_some_player_won> game_should_be_over_some_player_should_have_won;
	}

	[Subject("Game Over")]
	public class when_getting_three_in_a_row_diagonally_from_bottom_right_to_top_left : GameOverContext
	{
		Establish context = () =>
		{
			_game.PlayMove(2, 0, _somePlayer);
			_game.PlayMove(0, 2, _somePlayer);
		};

		Because of = () =>
			_game.PlayMove(1, 1, _somePlayer);

		Behaves_like<game_is_over_and_some_player_won> game_should_be_over_some_player_should_have_won;
	}
}