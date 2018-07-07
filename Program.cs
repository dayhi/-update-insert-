using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using testCons.Helper;
using testCons.Model;

namespace testCons {
    class Program {
        static void Main (string[] args) {

            var model = new UserModel () {
                id = 0,
                name = "day"
            };

            //TODO:以下代码执行万次平均耗时30ms(不计算初次执行，初次53毫秒)

            model.NotNullProp (nameof (model.id));//? 指定不为空的属性(不管值是否与初始值相同，sql语句中都包含此属性)

            List<MySqlParameter> parameters = new List<MySqlParameter> ();

            string s = parameters.AddDynamic (model, (strSql, name) => {
                strSql.Append ($"{name}=@{name} ,"); //? 当值不为空时，添加的sql语句。name为添加的属性名
            });

            model.RemovePropStat ();//! 在对此出现操作完成后，一定要执行这句，对此对象进行释放
        }
    }
}