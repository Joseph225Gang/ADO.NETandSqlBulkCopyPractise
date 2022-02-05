using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        private static SQLBulkCopyPractise _instance = null;

        public static SQLBulkCopyPractise Instance
        {
            get
            {
                return _instance ?? new SQLBulkCopyPractise();
            }
        }

        private SQLBulkCopyPractise()
        {
            _instance = this;
        }

        string _conn = ConfigurationManager.ConnectionStrings["ADONETConn"]?.ConnectionString ?? "Data Source=./;Initial Catalog = DBPractise; Integrated Security = True";
        int _insertCount = 100;
        delegate object SetValue();
        Dictionary<Type, SetValue> _actionDictionary = new Dictionary<Type, SetValue>();

        private Object AssignIntValue()
        {
            return new Random().Next(100);
        }

        private Object AssignGuidValue()
        {
            return Guid.NewGuid();
        }

        private Object AssignStringValue()
        {
            string[] strArr = new string[] { "Joseph", "Michael", "John", "Marry" };
            return strArr[new Random().Next(strArr.Length)];
        }

        public void SetActionDictionary()
        {
            if(!_actionDictionary.ContainsKey(typeof(int)))
                _actionDictionary.Add(typeof(int), AssignIntValue);
            if (!_actionDictionary.ContainsKey(typeof(string)))
                _actionDictionary.Add(typeof(string), AssignStringValue);
            if (!_actionDictionary.ContainsKey(typeof(Guid)))
                _actionDictionary.Add(typeof(Guid), AssignGuidValue);
        }

        private IEnumerable<PropertyInfo> TransferObjectIntoProperty(Type type)
        {
            PropertyInfo[] props = type.GetProperties();
            foreach (var prop in props)
            {
                object[] attrs = prop.GetCustomAttributes(true);
                foreach (object attr in attrs)
                {
                    KeyAttribute keyAttr = attr as KeyAttribute;
                    if (keyAttr != null)
                    {
                        props.Where(t => t.Name != prop.Name);
                    }
                }
            }
            return props;
        }

        public DataTable TransferTypeToDataTable(Type objectType)
        {

            var dt = new DataTable();
            IEnumerable<PropertyInfo> props = TransferObjectIntoProperty(objectType);

            foreach (var item in props)
            {
                dt.Columns.Add(item.Name, item.PropertyType);
            }

            for (int i = 0; i < _insertCount; i++)
            {
                var row = dt.NewRow();
                foreach (var item in props)
                {
                    Type type = item.PropertyType;
                    row[item.Name] = _actionDictionary[type].Invoke();
                }
                dt.Rows.Add(row);
            }
            return dt;
        }

        public void InsertData(DataTable dt, string tableName, Type type)
        {
            IEnumerable<PropertyInfo> props = TransferObjectIntoProperty(type);

            using (var sql = new SqlConnection(_conn))
            {
                sql.Open();
                using (var sqlBulkCopy = new SqlBulkCopy(sql))
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
    }
}
