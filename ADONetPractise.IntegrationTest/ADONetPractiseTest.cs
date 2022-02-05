using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ADONetPractise.IntegrationTest
{
    [TestClass]
    public class ADONetPractiseTest
    {
        static private string _tablePrex = "dbo.";

        SQLBulkCopyPractise _sqLBulkCopyPractise = SQLBulkCopyPractise.Instance;

        [TestInitialize]
        public void Setup()
        {
            _sqLBulkCopyPractise.SetActionDictionary();
        }

        [TestMethod]
        public void SystemManagement_IntegrationTest_DBInsert()
        {
            //Arrange
            var dt = _sqLBulkCopyPractise.TransferTypeToDataTable(typeof(SystemManagement));
            //Act
            _sqLBulkCopyPractise.InsertData(dt, $"{_tablePrex}{nameof(SystemManagement)}", typeof(SystemManagement));
            //Assert
            Assert.IsTrue(true, "pass test");
        }

        [TestMethod]
        public void Member_IntegrationTest_DBInsert()
        {
            //Arrange
            var dt = _sqLBulkCopyPractise.TransferTypeToDataTable(typeof(Member));
            //Act
            _sqLBulkCopyPractise.InsertData(dt, $"{_tablePrex}{nameof(Member)}", typeof(Member));
            //Assert
            Assert.IsTrue(true, "pass test");
        }

        [TestMethod]
        public void MemberAdditionalInfo_IntegrationTest_DBInsert()
        {
            //Arrange
            var dt = _sqLBulkCopyPractise.TransferTypeToDataTable(typeof(MemberAdditionalInfo));
            //Act
            _sqLBulkCopyPractise.InsertData(dt, $"{_tablePrex}{nameof(MemberAdditionalInfo)}", typeof(MemberAdditionalInfo));
            //Assert
            Assert.IsTrue(true, "pass test");
        }

    }
}