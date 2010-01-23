using Machine.Specifications;

namespace UndefeatedTicTacToe.Specs.GameSpecs
{
	public class GameOverContext : PlayMoveContext
	{

	}

	[Subject("Game OVer")]
	[Ignore]
	public class when_setting_the_one_one_to_get_horizontal_three_in_a_row : GameOverContext
	{
		Establish context = () =>
		{
			_game.PlayMove(0, 1, _somePlayer);
			_game.PlayMove(2, 1, _somePlayer);
		};

		Because of = () =>
			_game.PlayMove(1, 1, _somePlayer);

		It should_end_the_game = () =>
			_game.Over.ShouldBeTrue();

		It should_declare_the_winner = () =>
			_game.Winner.ShouldEqual(_somePlayer);

		It should_declare_the_loser = () =>
			_game.Loser.ShouldEqual(_someOtherPlayer);
	}

}