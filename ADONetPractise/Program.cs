using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ADONetPractise
{
    class Program
    {
        static void Main(string[] args)
        {
            SQLBulkCopyPractise sQLBulkCopyPractise = new SQLBulkCopyPractise();
            sQLBulkCopyPractise.SetActionDictionary();
            var dt = sQLBulkCopyPractise.TransferTypeToDataTable(typeof(SystemManagement));
            sQLBulkCopyPractise.InsertData(dt, "dbo.SystemManagement", typeof(SystemManagement));
            var dt1 = sQLBulkCopyPractise.TransferTypeToDataTable(typeof(Member));
            sQLBulkCopyPractise.InsertData(dt1, "dbo.Member", typeof(Member));
            var dt2 = sQLBulkCopyPractise.TransferTypeToDataTable(typeof(MemberAdditionalInfo));
            sQLBulkCopyPractise.InsertData(dt2, "dbo.MemberAdditionalInfo", typeof(MemberAdditionalInfo));
        }
    }
}
