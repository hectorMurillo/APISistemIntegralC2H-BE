using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace C2HApiControlInterno.Modules
{
    public class RootModule : NancyModule
    {
        public RootModule()
        {
            Get("/", _ => GetRoot());
        }

        private object GetRoot()
        {
            return Response.AsJson("API C2H FUNCIONANDO! 👍");
        }
    }
}