using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LC
{
    public interface ILCPlugin
    {
        string Name { get; }
        Version Version { get; }
        string Description { get; }
        string AuthorName { get; }
        void Initialize(object MainContainer);
        void Dispose();
    }
}
