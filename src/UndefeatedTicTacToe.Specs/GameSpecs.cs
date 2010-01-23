using System;
using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs
{
	public class GameContext
	{
		protected static TestableGame _game;
		protected static Player _opponent;
		protected static Bot _bot;
        
		protected class TestableGame : Game
		{
			public TestableGame(Bot bot, Player opponent, Player firstPlayer)
				: base(bot, opponent, firstPlayer)
			{
			}

			public Player TestableOpponent
			{
				get
				{
					return Opponent;
				}
			}

			public Player TestableBot
			{
				get
				{
					return Bot;
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
			_opponent = MockRepository.GenerateMock<Player>();
			_bot = MockRepository.GenerateMock<Bot>();
		};
	}

	public class CreatedGameContext : GameContext
	{
		Establish created_game_context = () =>
		{
			_game = new TestableGame(_bot, _opponent, _bot);
		};
	}

	[Subject("New Game")]
	public class when_creating_a_new_game : GameContext
	{
		Because of = () => _game = new TestableGame(_bot, _opponent, _opponent);

		It should_create_the_board = () => 
			_game.Board.ShouldNotBeNull();

		It should_set_the_opponent = () =>
			_game.TestableOpponent.ShouldEqual(_opponent);

		It should_set_the_bot = () =>
			_game.TestableBot.ShouldEqual(_bot);

		It should_set_next_player = () => 
			_game.TestableNextPlayer.ShouldEqual(_opponent);
	}

	[Subject("New Game")]
	public class when_opponent_makes_first_move : GameContext
	{
		Because of = () =>
			_game = new TestableGame(_bot, _opponent, _opponent);

		It should_tell_the_opponent_to_make_move = () =>
			_opponent.AssertWasCalled(opponent => opponent.MakeMove(_game));
	}

	[Subject("New Game")]
	public class when_bot_makes_first_move : GameContext
	{
		Because of = () =>
			_game = new TestableGame(_bot, _opponent, _bot);

		It should_tell_the_bot_to_make_move = () =>
			_bot.AssertWasCalled(bot => bot.MakeMove(_game));
	}

	[Subject("Move Played")]
	public class when_a_valid_move_is_played : CreatedGameContext
	{
		static int _someXCoordinate = 2;
		static int _someYCoordinate = 0;

		Because of = () =>
			_game.PlayMove(_someXCoordinate, _someYCoordinate, _bot);

		It should_set_move_one_board = () =>
			_game.Board[2,0].ShouldEqual(_bot);
	}
}