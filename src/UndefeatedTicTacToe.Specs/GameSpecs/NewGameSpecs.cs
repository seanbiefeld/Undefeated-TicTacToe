using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.GameSpecs
{
	public class GameContext
	{
		protected static TestableGame _game;
		protected static Player _somePlayer;
		protected static Player _someOtherPlayer;
        
		protected class TestableGame : Game
		{
			public TestableGame(Player somePlayer, Player someOtherPlayer, Player firstPlayer)
				: base(somePlayer, someOtherPlayer, firstPlayer)
			{
			}

			public Player TestableSomePlayer
			{
				get
				{
					return SomePlayer;
				}
			}

			public Player TestableSomeOtherPlayer
			{
				get
				{
					return SomeOtherPlayer;
				}
			}

			public Player TestableNextPlayer
			{
				get
				{
					return NextPlayer;
				}
			}
		}

		Establish game_context = () =>
		{
			_somePlayer = MockRepository.GenerateMock<Player>();
			_someOtherPlayer = MockRepository.GenerateMock<Player>();
		};
	}

	[Subject("New Game")]
	public class when_creating_a_new_game : GameContext
	{
		Because of = () => _game = new TestableGame(_somePlayer, _someOtherPlayer, _somePlayer);

		It should_create_the_board = () => 
			_game.Board.ShouldNotBeNull();

		It should_set_some_player = () =>
			_game.TestableSomePlayer.ShouldEqual(_somePlayer);

		It should_set_some_other_player = () =>
			_game.TestableSomeOtherPlayer.ShouldEqual(_someOtherPlayer);

		It should_set_next_player = () => 
			_game.TestableNextPlayer.ShouldEqual(_somePlayer);
	}

	[Subject("New Game")]
	public class when_opponent_makes_first_move : GameContext
	{
		Because of = () =>
			_game = new TestableGame(_someOtherPlayer, _somePlayer, _somePlayer);

		It should_tell_the_opponent_to_make_move = () =>
			_somePlayer.AssertWasCalled(opponent => opponent.MakeMove(_game));
	}

	[Subject("New Game")]
	public class when_bot_makes_first_move : GameContext
	{
		Because of = () =>
			_game = new TestableGame(_someOtherPlayer, _somePlayer, _someOtherPlayer);

		It should_tell_the_bot_to_make_move = () =>
			_someOtherPlayer.AssertWasCalled(bot => bot.MakeMove(_game));
	}
}