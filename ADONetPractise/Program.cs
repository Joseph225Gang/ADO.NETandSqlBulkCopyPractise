using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ADONetPractise
{
    class Program
    {
        static private string _tablePrex = "dbo.";

        static void Main(string[] args)
        {
            SQLBulkCopyPractise sQLBulkCopyPractise = new SQLBulkCopyPractise();
            sQLBulkCopyPractise.SetActionDictionary();
            sQLBulkCopyPractise.InsertData(sQLBulkCopyPractise.TransferTypeToDataTable(typeof(SystemManagement)), $"{_tablePrex}{nameof(SystemManagement)}",typeof(SystemManagement));
            sQLBulkCopyPractise.InsertData(sQLBulkCopyPractise.TransferTypeToDataTable(typeof(Member)), $"{_tablePrex}{nameof(SystemManagement)}", typeof(Member));
            sQLBulkCopyPractise.InsertData(sQLBulkCopyPractise.TransferTypeToDataTable(typeof(MemberAdditionalInfo)), $"{_tablePrex}{nameof(MemberAdditionalInfo)}", typeof(MemberAdditionalInfo));
        }
    }
}
