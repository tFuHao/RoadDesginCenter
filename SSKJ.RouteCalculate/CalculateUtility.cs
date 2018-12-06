using RoadStoneLib;
using SSKJ.RouteCalculate.CalculateModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Linq;

namespace SSKJ.RouteCalculate
{
    public class CalculateUtility
    {
        // 实例
        private Alignment am = new Alignment();

        // 实例一个datatable
        DataTable dt = new DataTable();

        // 中桩坐标_X
        double x_z = 0;

        // 中桩坐标_Y
        double y_z = 0;

        // 边桩坐标_Y
        double y_b = 0;

        // 反算桩号
        string s_f = "";

        // 反算偏距
        double d_f = 0;

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
        private void CreatRoute(RouteElementModel routeElementModel, double starStake)
        {
            //调用方法
            LoadBrokenChainage(routeElementModel.BrokenChainages);
            LoadFlatCurve(routeElementModel.Intersections);
            LoadVerticalCurve(routeElementModel.GradeChangePoints);
            //设置桩号前缀
            am.SetStartingStake(starStake, "");
            //调用方法创建一条线路
            am.ReGenerate();
        }

        /// <summary>
        /// 计算中桩坐标
        /// </summary>
        public List<CenterCoord> CalcCenterCoord(RouteElementModel routeElementModel, CalcParams calcParams)
        {
            try
            {
                //创建一条线路
                CreatRoute(routeElementModel, 1);

                //增加datatable字段
                DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
                DataColumn dc2 = new DataColumn("X", Type.GetType("System.Double"));
                DataColumn dc3 = new DataColumn("Y", Type.GetType("System.Double"));
                dt.Columns.Add(dc1);
                dt.Columns.Add(dc2);
                dt.Columns.Add(dc3);

                am.SetStakeParameter(true, calcParams.FlatCurveMainPoint, calcParams.VerticalCurveMainPoint, calcParams.AddStake);
                int j = 0;
                if (calcParams.AddStake && calcParams.Stakes.Count > 0)
                {
                    for (j = 0; j < calcParams.Stakes.Count; j++)
                    {
                        am.AppendAddiontalStake(calcParams.Stakes[j].ToString());
                    }
                }
                if (Convert.ToDouble(calcParams.BeginStake) > Convert.ToDouble(calcParams.EndStake))
                {
                    Stake end = am.Stake2Mileage(Convert.ToDouble(calcParams.EndStake));
                    for (Stake sk = am.Stake2Mileage(Convert.ToDouble(calcParams.BeginStake)); sk.M >= end.M; sk = am.GetPreviousStake(sk, Convert.ToDouble(calcParams.Interval)))
                    {
                        am.CalcCenterCoord(sk.S.ToString(), out x_z, out y_z);
                        DataRow dr = dt.NewRow();
                        dr["Stake"] = am.GetKString(sk.S, 3);
                        dr["X"] = x_z.ToString("F4");
                        dr["Y"] = y_z.ToString("F4");
                        dt.Rows.Add(dr);
                        if (sk.S == Convert.ToDouble(calcParams.EndStake)) break;
                    }
                }
                else
                {
                    Stake end = am.Stake2Mileage(Convert.ToDouble(calcParams.EndStake));
                    for (Stake sk = am.Stake2Mileage(Convert.ToDouble(calcParams.BeginStake)); sk.M <= end.M; sk = am.GetNextStake(sk, Convert.ToDouble(calcParams.Interval)))
                    {
                        string s = sk.S.ToString();
                        am.CalcCenterCoord(s, out x_z, out y_z);
                        DataRow dr = dt.NewRow();
                        dr["Stake"] = am.GetKString(sk.S, 3);
                        dr["X"] = x_z.ToString("F4");
                        dr["Y"] = y_z.ToString("F4");
                        dt.Rows.Add(dr);
                        if (sk.S == Convert.ToDouble(calcParams.EndStake)) break;
                    }
                }
                return ToList<CenterCoord>(dt);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 计算边桩坐标
        /// </summary>
        public List<SideCoord> CalcSideCoord(RouteElementModel routeElementModel, CalcParams calcParams)
        {
            //创建一条线路
            CreatRoute(routeElementModel, 1);
            //增加datatable字段
            DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("Dist", Type.GetType("System.Double"));
            DataColumn dc3 = new DataColumn("X", Type.GetType("System.Double"));
            DataColumn dc4 = new DataColumn("Y", Type.GetType("System.Double"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);

            am.SetStakeParameter(true, calcParams.FlatCurveMainPoint, calcParams.VerticalCurveMainPoint, calcParams.AddStake);
            if (calcParams.AddStake && calcParams.Stakes.Count > 0)
            {
                for (int j = 0; j < calcParams.Stakes.Count; j++)
                {
                    am.AppendAddiontalStake(calcParams.Stakes[j].ToString());
                }
            }
            if (Convert.ToDouble(calcParams.BeginStake) > Convert.ToDouble(calcParams.EndStake))
            {
                Stake end = am.Stake2Mileage(Convert.ToDouble(calcParams.EndStake));
                for (Stake sk = am.Stake2Mileage(Convert.ToDouble(calcParams.BeginStake)); sk.M >= end.M; sk = am.GetPreviousStake(sk, Convert.ToDouble(calcParams.Interval)))
                {
                    am.CalcSideCoord(sk.S.ToString(), Convert.ToDouble(calcParams.Dist), out x_z, out y_z);
                    DataRow dr = dt.NewRow();
                    dr["Stake"] = am.GetKString(sk.S, 3);
                    dr["Dist"] = Convert.ToDouble(calcParams.Dist).ToString("F4");
                    dr["X"] = x_z.ToString("F4");
                    dr["Y"] = y_z.ToString("F4");
                    dt.Rows.Add(dr);
                    if (sk.S == Convert.ToDouble(calcParams.EndStake)) break;
                }
            }
            else
            {
                Stake end = am.Stake2Mileage(Convert.ToDouble(calcParams.EndStake));
                for (Stake sk = am.Stake2Mileage(Convert.ToDouble(calcParams.BeginStake)); sk.M <= end.M; sk = am.GetNextStake(sk, Convert.ToDouble(calcParams.Interval)))
                {
                    am.CalcSideCoord(sk.S.ToString(), Convert.ToDouble(calcParams.Dist), out x_z, out y_z);
                    DataRow dr = dt.NewRow();
                    dr["Stake"] = am.GetKString(sk.S, 3);
                    dr["Dist"] = Convert.ToDouble(calcParams.Dist).ToString("F4");
                    dr["X"] = x_z.ToString("F4");
                    dr["Y"] = y_z.ToString("F4");
                    dt.Rows.Add(dr);
                    if (sk.S == Convert.ToDouble(calcParams.EndStake)) break;
                }
            }
            return ConvertTo(dt);
        }

        /// <summary>
        /// 计算中桩高程
        /// </summary>
        public List<CenterH> CalcCenterHight(RouteElementModel routeElementModel, CalcParams calcParams)
        {
            //创建一条线路
            CreatRoute(routeElementModel, 1);
            DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("H", Type.GetType("System.Double"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);

            am.SetStakeParameter(true, calcParams.FlatCurveMainPoint, calcParams.VerticalCurveMainPoint, calcParams.AddStake);
            int j = 0;
            if (calcParams.AddStake && calcParams.Stakes.Count > 0)
            {
                for (j = 0; j < calcParams.Stakes.Count; j++)
                {
                    am.AppendAddiontalStake(calcParams.Stakes[j].ToString());
                }
            }

            if (Convert.ToDouble(calcParams.BeginStake) > Convert.ToDouble(calcParams.EndStake))
            {
                Stake end = am.Stake2Mileage(Convert.ToDouble(calcParams.EndStake));
                for (Stake sk = am.Stake2Mileage(Convert.ToDouble(calcParams.BeginStake)); sk.M >= end.M; sk = am.GetPreviousStake(sk, Convert.ToDouble(calcParams.Interval)))
                {
                    DataRow dr = dt.NewRow();
                    dr["Stake"] = am.GetKString(sk.S, 3);
                    dr["H"] = am.CalcCenterHight(sk.S.ToString()).ToString("F4");
                    dt.Rows.Add(dr);
                    if (sk.S == Convert.ToDouble(calcParams.EndStake)) break;
                }
            }
            else
            {
                Stake end = am.Stake2Mileage(Convert.ToDouble(calcParams.EndStake));
                for (Stake sk = am.Stake2Mileage(Convert.ToDouble(calcParams.BeginStake)); sk.M <= end.M; sk = am.GetNextStake(sk, Convert.ToDouble(calcParams.Interval)))
                {
                    DataRow dr = dt.NewRow();
                    dr["Stake"] = am.GetKString(sk.S, 3);
                    dr["H"] = am.CalcCenterHight(sk.S.ToString()).ToString("F4");
                    dt.Rows.Add(dr);
                    if (sk.S == Convert.ToDouble(calcParams.EndStake)) break;
                }
            }
            return ToList<CenterH>(dt);
        }

        /// <summary>
        /// 坐标反算桩号
        /// </summary>
        public List<SideCoord> CoordBackCalcStake(RouteElementModel routeElementModel, List<Coord> coords)
        {
            //创建一条线路
            CreatRoute(routeElementModel, 1);
            //增加datatable字段
            DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
            DataColumn dc2 = new DataColumn("Dist", Type.GetType("System.Double"));
            DataColumn dc3 = new DataColumn("X", Type.GetType("System.Double"));
            DataColumn dc4 = new DataColumn("Y", Type.GetType("System.Double"));
            dt.Columns.Add(dc1);
            dt.Columns.Add(dc2);
            dt.Columns.Add(dc3);
            dt.Columns.Add(dc4);

            if (coords.Count > 0)
            {
                for (int j = 0; j < coords.Count; j++)
                {
                    am.CalcStake(coords[j].X, coords[j].Y, out s_f, out d_f);
                    DataRow dr = dt.NewRow();
                    dr["Stake"] = s_f;
                    dr["Dist"] = d_f;
                    dr["X"] = coords[j].X;
                    dr["Y"] = coords[j].Y;
                    dt.Rows.Add(dr);
                }
            }
            return ConvertTo(dt);
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

        public static List<SideCoord> ConvertTo(DataTable dt)
        {
            if (dt == null) return null;
            if (dt.Rows.Count <= 0) return null;

            List<SideCoord> list = new List<SideCoord>();
            list = (from DataRow dr in dt.Rows
                    select new SideCoord
                    {
                        Stake = dr["Stake"].ToString().Trim(),
                        Dist = Convert.ToDouble(dr["Dist"]),
                        X = Convert.ToDouble(dr["X"]),
                        Y = Convert.ToDouble(dr["Y"])
                    }).ToList();
            return list;
        }
    }
}
