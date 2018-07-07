using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;
using testCons;
using testCons.Interface;

namespace testCons.Helper {

    public static class ParameterHelper {
        ///获取值的mysql类型，支持varchar\char\double\datetime\int
        private static bool IsNull (object value) {
            if (value.GetType () == typeof (string)) {
                if (!string.IsNullOrEmpty (value.ToString ()))
                    return false;
            } else if (value.GetType () == typeof (double)) {
                if (Convert.ToDouble (value) != 0)
                    return false;
            } else if (value.GetType () == typeof (DateTime)) {
                if (Convert.ToDateTime (value) != new DateTime ())
                    return false;
            } else if (value.GetType () == typeof (int)) {
                if (Convert.ToInt32 (value) != 0)
                    return false;
            }

            return true;
        }

        private static bool AddParameter (this List<MySqlParameter> parameters, string name, object value, bool isNotNull) {

            if (!isNotNull) {
                if (value == null) return false;
                if (IsNull (value)) return false;
            }

            parameters.Add (new MySqlParameter ("@" + name, value));

            return true;
        }

        /// <summary>
        /// 动态插入parameter
        /// </summary>
        /// <param name="parameters">需要插入到的对象</param>
        /// <param name="model">需要插入的对象，需继承自IModel</param>
        /// <param name="action">当插入成功后，执行的str字符串拼接方法</param>
        /// <typeparam name="T">Model所属类型</typeparam>
        /// <returns></returns>
        public static string AddDynamic<T> (this List<MySqlParameter> parameters, T model, Action<StringBuilder, string> action) where T : IModel {

            StringBuilder strSql = new StringBuilder ();

            foreach (var p in model.GetType ().GetProperties ()) {
                string name = p.Name;
                if (parameters.AddParameter (name, p.GetValue (model), model.PropIsNull (name))) {
                    action (strSql, p.Name);
                }
            }
            
            return strSql.ToString ();
        }
    }
}