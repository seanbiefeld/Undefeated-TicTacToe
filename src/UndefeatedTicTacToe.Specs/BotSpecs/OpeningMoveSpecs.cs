using Machine.Specifications;
using Rhino.Mocks;
using UndefeatedTicTacToe.model;

namespace UndefeatedTicTacToe.Specs.BotSpecs
{
	public class MoveContext
	{
		protected class TestableBot : Bot
		{
			public int TestableMovesPlayed
			{
				get
				{
					return MovesPlayed;
				}
				set
				{
					MovesPlayed = value;
				}
			}
		}

		protected static Game _game;
		protected static TestableBot _bot;
		protected static IPlayer _opponent;

		Establish opening_move_context = () =>
		{
			_bot = new TestableBot();
			_opponent = MockRepository.GenerateStub<IPlayer>();
		};
	}

	[Behaviors]
	public class it_incremented_the_number_of_moves_played_by_one : MoveContext
	{
		It should_increment_the_number_of_moves_made_by_one = () => 
			_bot.TestableMovesPlayed.ShouldEqual(1);
	}

	[Subject("Opening Move")]
	public class when_the_bot_has_the_first_move : MoveContext
	{
		Because of = () =>
			_game = new Game(_opponent, _bot, _bot);

		It should_play_top_left_corner = () =>
			_game.Board[0,2].ShouldEqual(_bot);

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Opening Move")]
	public class when_the_bot_has_the_second_move_and_the_opponents_move_was_a_corner : MoveContext
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

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Opening Move")]
	public class when_the_bot_has_the_second_move_and_the_opponents_move_was_the_center : MoveContext
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

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}

	[Subject("Opening Move")]
	public class when_the_bot_has_the_second_move_and_the_opponents_move_was_the_edge : MoveContext
	{
		Establish context = () =>
		{
			_game = new Game(_opponent, _bot, _opponent);
			_game.PlayMove(2, 1, _opponent);
		};

		Because of = () =>
			_game.EndTurn();

		It should_play_the_center = () =>
			_game.Board[1,1].ShouldEqual(_bot);

		Behaves_like<it_incremented_the_number_of_moves_played_by_one> should_increment_the_number_of_moves_made_by_one;
	}
}
