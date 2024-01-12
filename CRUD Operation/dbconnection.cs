using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Operation
{
    internal class dbconnection
    {
        public string dbconnect() {
            string conn = "server=localhost;user=root;password=Admin123;database=crud_operation";
            return conn;
        
                
        }
    }
}
