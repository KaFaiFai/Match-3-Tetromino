using Match_3_Tetromino.Codes.Core;
using Match_3_Tetromino.Codes.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Match_3_Tetromino.Codes.Processors
{
    internal class BoardEventHandler : Processor
    {
        public void Move(WorldContext context, bool toLeft)
        {
            foreach (Entity entity in context.Entities)
            {
                if (entity.HasProperties(new List<Type> { typeof(MainBoard) }))
                {
                    MainBoard blockBoard = entity.GetProperty<MainBoard>();
                    blockBoard.LeftIndex += toLeft ? -1 : 1;
                }
            }
        }

        public void Rotate(WorldContext context, bool clockwise)
        {
            // TODO
        }

        public void Confirm(WorldContext context)
        {
            // TODO
        }
    }
}
