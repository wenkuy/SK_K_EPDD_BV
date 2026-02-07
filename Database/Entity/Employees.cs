using CS.Database.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS.Database.Entity
{

    public class Employee
    {
        /// <summary>
        /// 职工类型(员工、管理员、老板)
        /// </summary>
        public EEmployeeType EmployeeType { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public string Account { get; set; } = "";
        /// <summary>
        /// 账户密码
        /// </summary>
        public string Password { get; set; } = "";
        /// <summary>
        /// 员工编号
        /// </summary>
        public int EmployeeNumber { get; set; }
        /// <summary>
        /// 员工姓名
        /// </summary>
        public string Name { get; set; } = "";
        /// <summary>
        /// 最近登录时间
        /// </summary>
        public string latestLoadingTime { get; set; } = "";

    }
}
