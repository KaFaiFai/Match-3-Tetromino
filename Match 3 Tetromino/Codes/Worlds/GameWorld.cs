using Match_3_Tetromino.Codes.Core;
using Match_3_Tetromino.Codes.Entities;
using Match_3_Tetromino.Codes.Processors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Worlds
{
    internal class GameWorld : World
    {
        public override void Initialize()
        {
            base.Initialize();
            Entities.Add(new BoardEntity());

            Processors.Add(new BlockGridRenderer());
            InputTracker inputTracker = new InputTracker();
            BoardEventHandler boardEventHandler = new BoardEventHandler();
            inputTracker.PressedMoveRight += (context) => boardEventHandler.Move(context, toLeft: false);
            inputTracker.PressedMoveLeft += (context) => boardEventHandler.Move(context, toLeft: false);
            inputTracker.PressedConfirm += (context) => boardEventHandler.Confirm(context);
            Processors.Add(inputTracker);
            Processors.Add(boardEventHandler);
        }
    }
}
