using CS.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Entity
{
    public class Material 
    {
        public string MaterialNumber { get; set; } = "";
        public byte MaterialType { get; set; }
        public ERawMaterialVendor Vendor { get; set; }
    }
}
