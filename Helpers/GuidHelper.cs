using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLEditor.Helpers
{
    class GuidHelper
    {
        public static Guid? parse(String uid)
        {
            Guid newGuid;
            if (!Guid.TryParse(uid, out newGuid))
            {
                return null;
            }

            return newGuid;
        }

    }
}
