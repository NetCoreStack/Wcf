using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NetCoreStack.Wcf.Client;
using NetCoreStack.Wcf.Contracts;

namespace Wcf.Client.Test
{
    [TestClass]
    public class ProxyHelperTests
    {
        [TestMethod]
        public void GuidelineServiceTest()
        {
            var proxyHelper = new ProxyHelper();

            var service = proxyHelper.CreateProxy<IGuidelineService>();

            var model = service.RefTypeParameter(new CompositeType { BoolValue = true, Echo = "Echo from client", StringValue = "Some string value" });
        }

        [TestMethod]
        public void NetTcpServiceTest()
        {
            var proxyHelper = new ProxyHelper();

            var service = proxyHelper.CreateProxy<ITcpService>();

            var model = service.RefTypeParameter(new CompositeType { BoolValue = true, Echo = "Echo from client", StringValue = "Some string value" });
        }
    }
}
