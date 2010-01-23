using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.GameSpecs
{
	public class PlayMoveContext : GameContext
	{
		protected static bool _moveIsValid;
		protected static int _someXCoordinate;
		protected static int _someYCoordinate;

		Establish play_move_context = () =>
		{
			_somePlayer = new Player();
			_someOtherPlayer = new Player();
			_game = new TestableGame(_somePlayer, _someOtherPlayer, _somePlayer);
		};
	}

	[Subject("Move Played")]
	public class when_a_valid_move_is_played : PlayMoveContext
	{
		Establish context = () =>
		{
			_someXCoordinate = 2;
			_someYCoordinate = 0;
		};

		Because of = () =>
			_moveIsValid = _game.PlayMove(_someXCoordinate, _someYCoordinate, _somePlayer);

		It should_set_move_on_board = () =>
			_game.Board[2, 0].ShouldEqual(_somePlayer);

		It should_specify_move_is_valid = () =>
			_moveIsValid.ShouldBeTrue();

		It should_set_next_player = () => 
			_game.TestableNextPlayer.ShouldEqual(_someOtherPlayer);
	}

	[Behaviors]
	public class move_is_not_valid_behavior : PlayMoveContext
	{
		It should_specify_move_is_not_valid = () =>
			_moveIsValid.ShouldBeFalse();
	}

	[Subject("Move Played")]
	public class when_coordinates_are_in_conflict : PlayMoveContext
	{
		Establish context = () =>
		{
			_someXCoordinate = 1;
			_someYCoordinate = 1;

			_game.PlayMove(_someXCoordinate, _someYCoordinate, _somePlayer);
		};

		Because of = () =>
			_moveIsValid = _game.PlayMove(_someXCoordinate, _someYCoordinate, _someOtherPlayer);

		It should_not_set_other_player_at_coordinates = () =>
			_game.Board[1, 1].ShouldNotEqual(_someOtherPlayer);

		Behaves_like<move_is_not_valid_behavior> move_is_not_valid;
	}

	[Subject("Move Played")]
	public class when_coordinates_are_negative : PlayMoveContext
	{
		Establish context = () =>
		{
			_someXCoordinate = -1;
			_someYCoordinate = -1;
		};

		Because of = () =>
			_moveIsValid = _game.PlayMove(_someXCoordinate, _someYCoordinate, _somePlayer);

		Behaves_like<move_is_not_valid_behavior> move_is_not_valid;
	}

	[Subject("Move Played")]
	public class when_coordinates_are_greater_than_tictactoe_grid : PlayMoveContext
	{
		Establish context = () =>
		{
			_someXCoordinate = 3;
			_someYCoordinate = 3;
		};

		Because of = () =>
			_moveIsValid = _game.PlayMove(_someXCoordinate, _someYCoordinate, _somePlayer);

		Behaves_like<move_is_not_valid_behavior> move_is_not_valid;
	}

	[Subject("End Turn")]
	public class when_player_ends_turn : GameContext
	{
		Establish context = () =>
		{
			_game = new TestableGame(_somePlayer, _someOtherPlayer, _somePlayer);
			_game.PlayMove(1, 2, _somePlayer);
		};

		Because of = () =>
			_game.EndTurn();

		It should_set_next_player = () =>
			_someOtherPlayer.AssertWasCalled(player=>player.MakeMove(_game));
	}
}