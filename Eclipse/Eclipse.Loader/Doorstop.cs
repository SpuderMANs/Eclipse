namespace Doorstop
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    class Entrypoint
    {
        public static void Start()
        {
            Eclipse.Loader.Loader.Load();
        }
    }
}
