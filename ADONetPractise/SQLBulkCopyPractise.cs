using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ADONetPractise
{
    public class SQLBulkCopyPractise
    {
        string con = ConfigurationManager.ConnectionStrings["ADONETConn"].ConnectionString;
        int insertCount = 5;

        private IEnumerable<PropertyInfo> TransferObjectIntoProperty(Object transferObject)
        {
            IEnumerable<PropertyInfo> props = transferObject.GetType().GetProperties();
            if (transferObject is Member)
                props = props.Where(tx => tx.Name != "memberId");
            else if (transferObject is MemberAdditionalInfo)
                props = props.Where(tx => tx.Name != "memberAdiId");
            else if (transferObject is SystemManagement)
                props = props.Where(tx => tx.Name != "systemId");
            return props;
        }

        public DataTable TransferTypeToDataTable(Object transferObject)
        {

            var dt = new DataTable();
            IEnumerable<PropertyInfo> props = TransferObjectIntoProperty(transferObject);

            foreach (var item in props)
            {
                dt.Columns.Add(item.Name, GetPropertyType(item.PropertyType.ToString()));
            }
            for (int i = 0; i < insertCount; i++)
            {

                var row = dt.NewRow();
                foreach (var item in props)
                {
                    Type type = GetPropertyType(item.PropertyType.ToString());
                    if (type == typeof(Guid))
                        row[item.Name] = Guid.NewGuid();
                    else if (type == typeof(int))
                        row[item.Name] = 5;
                    else if (type == typeof(string))
                        row[item.Name] = "jjm";
                    else
                        row[item.Name] = "" ?? null;
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public void InsertData(DataTable dt, string tableName, Object transferObject)
        {
            IEnumerable<PropertyInfo> props = TransferObjectIntoProperty(transferObject);

            using (var sql = new SqlConnection(con))
            {
                sql.Open();
                using(var sqlBulkCopy = new SqlBulkCopy(sql))
                {
                    sqlBulkCopy.DestinationTableName = tableName;
                    foreach (var item in props)
                    {
                        sqlBulkCopy.ColumnMappings.Add(item.Name, item.Name);
                    }
                    sqlBulkCopy.WriteToServer(dt);
                }
            }
        }

        private Type GetPropertyType(string type)
        {
            if (type == "System.Guid")
                return typeof(Guid);
            else if (type == "System.String")
                return typeof(string);
            else if (type == "System.Int32")
                return typeof(int);
            else
                return typeof(object);
        }
    }
}
