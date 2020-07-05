using Recodme.RD.BoraNow.DataAccessLayer.Context;
using System;

namespace Recodme.RD.BoraNow.PresentationLayer.App
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new BoraNowContext();
            context.Database.EnsureCreated();
        }
    }
}
