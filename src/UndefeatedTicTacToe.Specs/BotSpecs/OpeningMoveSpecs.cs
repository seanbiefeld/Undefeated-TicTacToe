using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.BotSpecs
{
	public class OpeningMoveContext
	{
		protected static Game _game;
		protected static Bot _bot;
		protected static IPlayer _opponent;

		Establish opening_move_context = () =>
		{
			_bot = new Bot();
			_opponent = MockRepository.GenerateStub<IPlayer>();
		};
	}

	[Subject("Opening Move")]
	public class when_the_bot_has_the_first_move : OpeningMoveContext
	{
		Because of = () =>
			_game = new Game(_opponent, _bot, _bot);

		It should_play_top_left_corner = () =>
			_game.Board[0,2].ShouldEqual(_bot);
	}

	[Subject("Opening Move")]
	public class when_the_bot_has_the_second_move_and_the_opponents_move_was_a_corner : OpeningMoveContext
	{
		Establish context = () =>
		{
			_game = new Game(_opponent, _bot, _opponent);
			_game.PlayMove(0, 2, _opponent);
		};

		Because of = () =>
			_game.EndTurn();

		It should_play_the_center = () =>
			_game.Board[1, 1].ShouldEqual(_bot);
	}

	[Subject("Opening Move")]
	public class when_the_bot_has_the_second_move_and_the_opponents_move_was_the_center : OpeningMoveContext
	{
		Establish context = () =>
		{
			_game = new Game(_opponent, _bot, _opponent);
			_game.PlayMove(1, 1, _opponent);
		};

		Because of = () =>
			_game.EndTurn();

		It should_play_the_corner = () =>
			_game.Board[0, 2].ShouldEqual(_bot);
	}
}
