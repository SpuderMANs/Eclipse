namespace Eclipse.API.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public interface IConfig
    {
        public bool isEnabled { get; }
        public bool Debug { get; }
    }
}
