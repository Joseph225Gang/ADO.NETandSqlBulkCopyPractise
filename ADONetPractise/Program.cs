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
                SQLBulkCopyPractise SqLBulkCopyPractise = SQLBulkCopyPractise.Instance;
                SqLBulkCopyPractise.SetActionDictionary();
                SqLBulkCopyPractise.InsertData(SqLBulkCopyPractise.TransferTypeToDataTable(typeof(SystemManagement)), $"{_tablePrex}{nameof(SystemManagement)}", typeof(SystemManagement));
                SqLBulkCopyPractise.InsertData(SqLBulkCopyPractise.TransferTypeToDataTable(typeof(Member)), $"{_tablePrex}{nameof(Member)}", typeof(Member));
                SqLBulkCopyPractise.InsertData(SqLBulkCopyPractise.TransferTypeToDataTable(typeof(MemberAdditionalInfo)), $"{_tablePrex}{nameof(MemberAdditionalInfo)}", typeof(MemberAdditionalInfo));
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
