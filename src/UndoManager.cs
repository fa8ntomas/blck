using GuiLabs.Undo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor
{
    class UndoManager
    {

        private static readonly ActionManager actionManager = new ActionManager();

        internal static void RecordAction(IAction action)
        {
            actionManager.RecordAction(action);
        }

        internal static void Undo()
        {
            actionManager.Undo();
        }
    }
}
