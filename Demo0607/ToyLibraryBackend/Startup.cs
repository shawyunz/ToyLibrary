﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ToyLibraryBackend.Startup))]

namespace ToyLibraryBackend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}