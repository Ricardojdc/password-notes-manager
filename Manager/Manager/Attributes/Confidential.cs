using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Utils;

namespace Manager.Attributes
{
    class Confidential: Attribute
    {
        public string Warning { get; }

        public Confidential(string warning = ErrorMessages.CONFIDENTIAL_DATA)
        {

            Warning = warning;
        }

    }
}
