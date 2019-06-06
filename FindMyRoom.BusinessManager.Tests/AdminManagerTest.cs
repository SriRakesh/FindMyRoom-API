using FindMyRoom.DataService;
using FindMyRoom.Entities;
using FindMyRoom.Entities.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace FindMyRoom.BusinessManager.Tests
{
    [TestClass]
    public class AdminManagerTest
    {
        
        [TestMethod]
        public void DeletePartner_Positive()
        {
            int PartnerId = 400;
            var MockAdminService = new Mock<AdminService>();
            MockAdminService.Setup(p => p.PartnerDelete(PartnerId));
            AdminManager adminManager = new AdminManager(MockAdminService.Object);
            try
            {
                var actualReturnType = adminManager.deletePartnerService(PartnerId);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }
        }
        [TestMethod]
        public void DeletePartner_Negavtive()
        {
            int PartnerId = 1000;
            var MockAdminService = new Mock<AdminService>();
            MockAdminService.Setup(p => p.PartnerDelete(PartnerId));
            AdminManager adminManager = new AdminManager(MockAdminService.Object);
            try
            {
                var actualReturnType = adminManager.deletePartnerService(PartnerId);
                Assert.IsTrue(false);
            }
            catch
            {
                Assert.IsTrue(true);
            }

        }
    }
}
