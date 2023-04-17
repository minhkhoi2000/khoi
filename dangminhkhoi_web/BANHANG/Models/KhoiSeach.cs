using BANHANG.contex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BANHANG.Models
{
    public class KhoiSeach
    {
        webbanhangEntities5 objQLWEBEntities3 = new webbanhangEntities5();

        public List<product> SearchByKey(string key)
        {
            return objQLWEBEntities3.products.SqlQuery("Select * From product Where Name like '%" + key + "%'").ToList();
        }
    }
}
