using RoadStoneLib;
using SSKJ.RoadDesignCenter.Utility.CalculateModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Utility
{
    public class CalculateUtility
    {
        // 实例
        private Alignment am;
        //private Alignment am=new Alignment();

        // 实例一个datatable
        DataTable dt=new DataTable();

        // 中桩坐标_X
        double x_z = 0;

        // 中桩坐标_Y
        double y_z = 0;

        // 中桩高程
        double h_z = 0;

        // 边桩坐标_X
        double x_b = 0;

        // 边桩坐标_Y
        double y_b = 0;

        //public CalculateUtility(Alignment am, DataTable dt)
        //{
        //    this.am = am;
        //    this.dt = dt;
        //}

        /// <summary>
        /// 加载平曲线
        /// </summary>
        /// <param name="routeid">线路ID</param>
        private void LoadFlatCurve(IEnumerable<Intersection> list)
        {
            foreach (var item in list)
            {
                string jdbh = item.IntersectionName.ToString().Trim();
                double x = Convert.ToDouble(item.X);
                double y = Convert.ToDouble(item.Y);
                double r = Convert.ToDouble(item.R);
                double ls1 = Convert.ToDouble(item.Ls1);
                double ls2 = Convert.ToDouble(item.Ls2);
                double ls1r = Convert.ToDouble(item.Ls1R);
                double ls2r = Convert.ToDouble(item.Ls2R);
                am.AppendIntersectPoint(jdbh, x, y, r, ls1, ls2, ls1r, ls2r);
            }
        }

        /// <summary>
        /// 加载竖曲线
        /// </summary>
        /// <param name="routeid">线路ID</param>
        private void LoadVerticalCurve(IEnumerable<GradeChangePoint> list)
        {
            foreach (var item in list)
            {
                string stake = item.Stake.ToString().Trim();
                double h = Convert.ToDouble(item.H);
                double r = Convert.ToDouble(item.R);
                //double i1 = Convert.ToDouble(item.i1);
                //double i2 = Convert.ToDouble(item.i2);

                am.AppendGradePoint(stake, h, r);
            }
        }

        /// <summary>
        /// 加载断链
        /// </summary>
        /// <param name="routeid">线路ID</param>
        private void LoadBrokenChainage(IEnumerable<BrokenChainage> list)
        {
            foreach (var item in list)
            {
                double fStake = Convert.ToDouble(item.FrontStake);
                double aStake = Convert.ToDouble(item.AfterStake);
                am.AppendBroken(fStake, aStake);
            }
        }


        /// <summary>
        /// 创建一条线路
        /// </summary>
        /// <param name="routeid">线路ID</param>
        private void CreatRoute(IEnumerable<Intersection> intersection, IEnumerable<GradeChangePoint> gradeChangePoint, IEnumerable<BrokenChainage> brokenChainage, double starStake)
        {
            //调用方法
            LoadBrokenChainage(brokenChainage);
            LoadFlatCurve(intersection);

            LoadVerticalCurve(gradeChangePoint);
            //设置桩号前缀
            //rd.SetStartingStake(0, "");
            am.SetStartingStake(starStake, "");
            //调用方法创建一条线路
            //rd.GenerateRoute();
            am.ReGenerate();
        }

        /// <summary>
        /// 计算中桩坐标
        /// </summary>
        /// <param name="beginStake">开始桩号</param>
        /// <param name="endStake">结束桩号</param>
        /// <param name="routeid">线路ID</param>
        public List<CenterCoord> CalcCenterCoord(IEnumerable<Intersection> intersection, IEnumerable<GradeChangePoint> gradeChangePoint, IEnumerable<BrokenChainage> brokenChainage, string beginStake, string endStake, int interval, double starStake, double[] stkes = null)
        {
            am = new Alignment();
            //创建一条线路
            CreatRoute(intersection, gradeChangePoint, brokenChainage, starStake);

            //增加datatable字段
            DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("X", Type.GetType("System.Double"));
            DataColumn dc3 = new DataColumn("Y", Type.GetType("System.Double"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);

            am.SetStakeParameter(true, true, false, true);
            int j = 0;
            if (stkes.Length > 0)
            {
                for (j = 0; j < stkes.Length; j++)
                {
                    am.AppendAddiontalStake(stkes[j].ToString());
                }
            }
            if (Convert.ToDouble(beginStake) > Convert.ToDouble(endStake))
            {
                Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
                for (Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M >= end.M; sk = am.GetPreviousStake(sk, interval))
                {
                    //rd.CalcCenterCoord(i.ToString(), out x_z, out y_z);
                    am.CalcCenterCoord(sk.S.ToString(), out x_z, out y_z);
                    DataRow dr = dt.NewRow();
                    dr["Stake"] = am.GetKString(sk.S, 3);
                    dr["X"] = x_z.ToString("F4");
                    dr["Y"] = y_z.ToString("F4");
                    dt.Rows.Add(dr);
                    //sk.S = Begin_End_Stake_Set(beginStake, endStake, interval, sk.S);
                    if (sk.S == Convert.ToDouble(endStake))
                    {
                        break;
                    }
                }
            }
            else
            {
                Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
                for (Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M <= end.M; sk = am.GetNextStake(sk, interval))
                {
                    string s = sk.S.ToString();
                    am.CalcCenterCoord(sk.S.ToString(), out x_z, out y_z);
                    DataRow dr = dt.NewRow();
                    dr["Stake"] = am.GetKString(sk.S, 3);
                    dr["X"] = x_z.ToString("F4");
                    dr["Y"] = y_z.ToString("F4");
                    dt.Rows.Add(dr);
                    if (sk.S == Convert.ToDouble(endStake))
                    {
                        break;
                    }
                }
            }
            List<CenterCoord> list = new List<CenterCoord>();
            list = ToList<CenterCoord>(dt);
            return list;
        }
        /// <summary>
        /// 将DataTable数据集转换成List集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dt">数据集</param>
        /// <returns></returns>
        public static List<T> ToList<T>(DataTable dt)
        {
            var list = new List<T>();
            Type t = typeof(T);
            var plist = new List<PropertyInfo>(typeof(T).GetProperties());

            foreach (DataRow item in dt.Rows)
            {
                T s = Activator.CreateInstance<T>();
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    PropertyInfo info = plist.Find(p => p.Name == dt.Columns[i].ColumnName);
                    if (info != null)
                    {
                        if (!Convert.IsDBNull(item[i]))
                        {
                            info.SetValue(s, item[i], null);
                        }
                    }
                }
                list.Add(s);
            }
            return list;
        }
    }
}
