using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameDeals.Core.Interfaces
{
    public interface IHtmlParser
    {
        string StripHtmlTags(string text);
    }
}
