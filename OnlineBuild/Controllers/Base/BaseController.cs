using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OnlineBuild.Controllers
{
    public abstract class BaseController : Controller
    {

        protected ILogger logger;

        protected IServiceProvider svp;

        public BaseController(ILoggerFactory factory, IServiceProvider svp)
        {
            if (logger == null)
                this.logger = factory.CreateLogger(this.GetType());
            this.svp = svp;
        }
    }
}
