using Machine.Specifications;
using Rhino.Mocks;

namespace UndefeatedTicTacToe.Specs.BotSpecs
{
    public class ForkStrategySpecs : MoveStrategyContext
    {
    }

    [Subject("Bot fork strategy")]
    public class when_blocking_opponents_fork : ForkStrategySpecs
    {
        private Establish context = () =>
        {
			_game.TestableBoard[1, 2] = _opponent;
			_game.TestableBoard[0, 2] = _bot;
            _game.TestableBoard[2, 0] = _opponent;
            _numberOfMoves = 1;
            _bot.TestableMovesPlayed = _numberOfMoves;  
        };

        private Because of = () =>
			_bot.MakeMove(_game);

        private It should_block_fork = () =>
			_game.AssertWasCalled(game => game.PlayMove(2, 2, _bot));
    }

	[Subject("Bot fork strategy")]
	public class when_forking_and_opposite_unblocked_corner_is_available : ForkStrategySpecs
	{
		private Establish context = () =>
		{
			_game.TestableBoard[1, 2] = _bot;
			_game.TestableBoard[1, 1] = _opponent;
			_numberOfMoves = 1;
			_bot.TestableMovesPlayed = _numberOfMoves;
		};

		private Because of = () =>
			_bot.MakeMove(_game);

		private It should_block_fork = () =>
			_game.AssertWasCalled(game => game.PlayMove(0, 0, _bot));
	}

	
}
