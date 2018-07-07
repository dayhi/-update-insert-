using System;
using System.Collections;
using System.Collections.Generic;
using testCons.Interface;

namespace testCons.Helper {

    /// <summary>
    /// author：day
    /// </summary>
    public static class IModelPropHelper {

        private static Hashtable propStat = new Hashtable ();

        /// <summary>
        /// 指定属性不为空，如果该对象已存在则会覆盖
        /// </summary>
        /// <param name="model"></param>
        /// <param name="names">属性的名称，可通过nameof()来指定</param>
        public static void NotNullProp (this IModel model, params string[] names) {
            if (!propStat.ContainsKey (model))
                propStat.Add (model, names);
            else
                propStat[model] = names;
        }

        /// <summary>
        /// 判断当前model的属性是否指定不为空
        /// </summary>
        /// <param name="model"></param>
        /// <param name="name">属性的名称，可通过nameof()来指定</param>
        /// <returns>true为指定，false为未设定</returns>
        public static bool PropIsNull (this IModel model, string name) {
            if (propStat.Count == 0) return false;
            if (!propStat.ContainsKey (model)) return false;

            foreach (var s in (string[]) propStat[model]) {
                if (s.Equals (name)) {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 将无用的属性删除
        /// </summary>
        /// <param name="model"></param>
        public static void RemovePropStat (this IModel model) {
            if (propStat.ContainsKey (model))
                propStat.Remove (model);
        }
    }

}