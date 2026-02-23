namespace Eclipse.Loader
{
    using Eclipse.API.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class Config : IConfig
    {
        public bool isEnabled { get; set; } = true;
        public bool Debug { get; set; } = false;
    }
}
