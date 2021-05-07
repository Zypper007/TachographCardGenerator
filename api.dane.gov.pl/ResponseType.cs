using System;
using System.Collections.Generic;
using System.Text;

namespace api.dane.gov.pl
{
    internal abstract class ResponseType
    {
        abstract public List<string> GetData();
    }
}
