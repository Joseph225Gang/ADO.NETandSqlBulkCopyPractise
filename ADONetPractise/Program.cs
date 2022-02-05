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
            try
            {
                SQLBulkCopyPractise sQLBulkCopyPractise = SQLBulkCopyPractise.Instance;
                sQLBulkCopyPractise.SetActionDictionary();
                sQLBulkCopyPractise.InsertData(sQLBulkCopyPractise.TransferTypeToDataTable(typeof(SystemManagement)), $"{_tablePrex}{nameof(SystemManagement)}", typeof(SystemManagement));
                sQLBulkCopyPractise.InsertData(sQLBulkCopyPractise.TransferTypeToDataTable(typeof(Member)), $"{_tablePrex}{nameof(Member)}", typeof(Member));
                sQLBulkCopyPractise.InsertData(sQLBulkCopyPractise.TransferTypeToDataTable(typeof(MemberAdditionalInfo)), $"{_tablePrex}{nameof(MemberAdditionalInfo)}", typeof(MemberAdditionalInfo));
            }
            catch(SqlException ex)
            { 
                Console.WriteLine(ex.Message); 
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.Message); 
            }

        }
    }
}
