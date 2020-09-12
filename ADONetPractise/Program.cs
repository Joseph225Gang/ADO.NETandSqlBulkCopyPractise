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
            var sysManagement = new SystemManagement();
            var member = new Member();
            var memberAdditionalInfo = new MemberAdditionalInfo();
            SQLBulkCopyPractise sQLBulkCopyPractise = new SQLBulkCopyPractise();
            var dt = sQLBulkCopyPractise.TransferTypeToDataTable(sysManagement);
            sQLBulkCopyPractise.InsertData(dt, "dbo.SystemManagement", sysManagement);
            var dt1 = sQLBulkCopyPractise.TransferTypeToDataTable(member);
            sQLBulkCopyPractise.InsertData(dt1, "dbo.Member", member);
            var dt2 = sQLBulkCopyPractise.TransferTypeToDataTable(memberAdditionalInfo);
            sQLBulkCopyPractise.InsertData(dt2, "dbo.MemberAdditionalInfo", memberAdditionalInfo);
        }
    }
}
