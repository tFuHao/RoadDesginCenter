///using RoadStoneLib;
using SSKJ.RoadDesignCenter.IRepository.Project.RouteElement;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using SSKJ.RoadDesignCenter.IBusines.Project.RouteCalculate;

namespace SSKJ.RoadDesignCenter.Busines.Project.RouteCalculate
{
    public class CalculateBusines: ICalculateBusines
    {
        public IFlatCurve_IntersectionRepository intersectionRepo;
        public IVerticalCurve_GradeChangePointRepository gradeRepo;
        public IBrokenChainRepository brokenRepo;

        // 实例
       //private Alignment am;

        // 实例一个datatable
        DataTable dt = new DataTable();

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

        public CalculateBusines(IFlatCurve_IntersectionRepository intersectionRepo, IVerticalCurve_GradeChangePointRepository gradeRepo, IBrokenChainRepository brokenRepo)
        {
            this.intersectionRepo = intersectionRepo;
            this.gradeRepo = gradeRepo;
            this.brokenRepo = brokenRepo;
        }

        /// <summary>
        /// 加载平曲线
        /// </summary>
        /// <param name="routeid">线路ID</param>
        public async Task LoadFlatCurve(string routeId, string dbName)
        {
            var list = await intersectionRepo.GetListAsync(i => i.RouteId == routeId, dbName);
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
                //am.AppendIntersectPoint(jdbh, x, y, r, ls1, ls2, ls1r, ls2r);
            }
        }

        /// <summary>
        /// 加载竖曲线
        /// </summary>
        /// <param name="routeid">线路ID</param>
        public async Task LoadVerticalCurve(string routeId, string dbName)
        {

            var list = await gradeRepo.GetListAsync(g => g.RouteId == routeId, dbName);
            foreach (var item in list)
            {
                string stake = item.Stake.ToString().Trim();
                double h = Convert.ToDouble(item.H);
                double r = Convert.ToDouble(item.R);
                //double i1 = Convert.ToDouble(item.i1);
                //double i2 = Convert.ToDouble(item.i2);

                //am.AppendGradePoint(stake, h, r);
            }
        }

        /// <summary>
        /// 加载断链
        /// </summary>
        /// <param name="routeid">线路ID</param>
        public async Task LoadBrokenChainage(string routeId, string dbName)
        {
            var list = await brokenRepo.GetListAsync(b => b.RouteId == routeId, dbName);
            foreach (var item in list)
            {
                double fStake = Convert.ToDouble(item.FrontStake);
                double aStake = Convert.ToDouble(item.AfterStake);
                //am.AppendBroken(fStake, aStake);
            }
        }

        /// <summary>
        /// 创建一条线路
        /// </summary>
        /// <param name="routeid">线路ID</param>
        public async Task CreatRoute(string routeId, double starStake, string dbName)
        {
            //调用方法
            await LoadBrokenChainage(routeId, dbName);
            await LoadFlatCurve(routeId, dbName);

            await LoadVerticalCurve(routeId, dbName);
            //设置桩号前缀
            //rd.SetStartingStake(0, "");
            //am.SetStartingStake(starStake, "");
            //调用方法创建一条线路
            //rd.GenerateRoute();
            //am.ReGenerate();
        }

        /// <summary>
        /// 计算中桩坐标
        /// </summary>
        /// <param name="beginStake">开始桩号</param>
        /// <param name="endStake">结束桩号</param>
        /// <param name="routeid">线路ID</param>
        //public async Task<List<CenterCoord>> CalcCenterCoord(string beginStake, string endStake, int interval, string routeId, double starStake, string dbName, double[] stkes = null)
        //{
        //    //am = new Alignment();
        //    //创建一条线路
        //    await CreatRoute(routeId, starStake, dbName);

        //    //增加datatable字段
        //    DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
        //    DataColumn dc2 = new DataColumn("X", Type.GetType("System.Double"));
        //    DataColumn dc3 = new DataColumn("Y", Type.GetType("System.Double"));
        //    dt.Columns.Add(dc1);
        //    dt.Columns.Add(dc2);
        //    dt.Columns.Add(dc3);

        //    //am.SetStakeParameter(true, true, false, true);
        //    //int j = 0;
        //    //if (stkes.Length > 0)
        //    //{
        //    //    for (j = 0; j < stkes.Length; j++)
        //    //    {
        //    //        am.AppendAddiontalStake(stkes[j].ToString());
        //    //    }
        //    //}
        //    //if (Convert.ToDouble(beginStake) > Convert.ToDouble(endStake))
        //    //{
        //    //    RoadStoneLib.Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
        //    //    for (RoadStoneLib.Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M >= end.M; sk = am.GetPreviousStake(sk, interval))
        //    //    {
        //    //        //rd.CalcCenterCoord(i.ToString(), out x_z, out y_z);
        //    //        am.CalcCenterCoord(sk.S.ToString(), out x_z, out y_z);
        //    //        DataRow dr = dt.NewRow();
        //    //        dr["Stake"] = am.GetKString(sk.S, 3);
        //    //        dr["X"] = x_z.ToString("F4");
        //    //        dr["Y"] = y_z.ToString("F4");
        //    //        dt.Rows.Add(dr);
        //    //        //sk.S = Begin_End_Stake_Set(beginStake, endStake, interval, sk.S);
        //    //        if (sk.S == Convert.ToDouble(endStake))
        //    //        {
        //    //            break;
        //    //        }
        //    //    }
        //    //}
        //    //else
        //    //{
        //    //    RoadStoneLib.Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
        //    //    for (RoadStoneLib.Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M <= end.M; sk = am.GetNextStake(sk, interval))
        //    //    {
        //    //        string s = sk.S.ToString();
        //    //        am.CalcCenterCoord(sk.S.ToString(), out x_z, out y_z);
        //    //        DataRow dr = dt.NewRow();
        //    //        dr["Stake"] = am.GetKString(sk.S, 3);
        //    //        dr["X"] = x_z.ToString("F4");
        //    //        dr["Y"] = y_z.ToString("F4");
        //    //        dt.Rows.Add(dr);
        //    //        if (sk.S == Convert.ToDouble(endStake))
        //    //        {
        //    //            break;
        //    //        }
        //    //    }
        //    //}
        //    List<CenterCoord> list = new List<CenterCoord>();
        //    list = ToList<CenterCoord>(dt);
        //    return list;
        //}

        /// <summary>
        /// 计算边桩坐标
        /// </summary>
        /// <param name="beginStake">开始桩号</param>
        /// <param name="endStake">结束桩号</param>
        /// <param name="dist">偏距</param>
        /// <param name="routeid">线路ID</param>
        //public async Task<List<SideCoord>> CalcSideCoord(string beginStake, string endStake, double dist, int interval, string routeId,string dbName, double[] stkes = null)
        //{
        //    //创建一条线路
        //    await CreatRoute(routeId, 1, dbName);
        //    //增加datatable字段
        //    DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
        //    DataColumn dc2 = new DataColumn("Dist", Type.GetType("System.Double"));
        //    DataColumn dc3 = new DataColumn("X", Type.GetType("System.Double"));
        //    DataColumn dc4 = new DataColumn("Y", Type.GetType("System.Double"));
        //    dt.Columns.Add(dc1);
        //    dt.Columns.Add(dc2);
        //    dt.Columns.Add(dc3);
        //    dt.Columns.Add(dc4);

        //    am.SetStakeParameter(true, true, false, true);
        //    int j = 0;
        //    if (stkes.Length > 0)
        //    {
        //        for (j = 0; j < stkes.Length; j++)
        //        {
        //            am.AppendAddiontalStake(stkes[j].ToString());
        //        }
        //    }
        //    if (Convert.ToDouble(beginStake) > Convert.ToDouble(endStake))
        //    {
        //        Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
        //        for (Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M >= end.M; sk = am.GetPreviousStake(sk, interval))
        //        {
        //            //rd.CalcCenterCoord(i.ToString(), out x_z, out y_z);
        //            am.CalcSideCoord(sk.S.ToString(), dist, out x_z, out y_z);
        //            DataRow dr = dt.NewRow();
        //            dr["Stake"] = am.GetKString(sk.S, 3);
        //            dr["Dist"] = dist.ToString("F4");
        //            dr["X"] = x_z.ToString("F4");
        //            dr["Y"] = y_z.ToString("F4");
        //            dt.Rows.Add(dr);
        //            //sk.S = Begin_End_Stake_Set(beginStake, endStake, interval, sk.S);
        //            if (sk.S == Convert.ToDouble(endStake))
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
        //        for (Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M <= end.M; sk = am.GetNextStake(sk, interval))
        //        {
        //            am.CalcSideCoord(sk.S.ToString(), dist, out x_z, out y_z);
        //            DataRow dr = dt.NewRow();
        //            dr["Stake"] = am.GetKString(sk.S, 3);
        //            dr["Dist"] = dist.ToString("F4");
        //            dr["X"] = x_z.ToString("F4");
        //            dr["Y"] = y_z.ToString("F4");
        //            dt.Rows.Add(dr);
        //            if (sk.S == Convert.ToDouble(endStake))
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    List<SideCoord> list = new List<SideCoord>();
        //    list = ConvertTo(dt);
        //    return list;
        //}

        /// <summary>
        /// 计算中桩高程
        /// </summary>
        /// <param name="routeid">线路ID</param>
        //public async Task<List<CenterH>> CalcCenterHight(string beginStake, string endStake, int interval, string routeId,string dbName, double[] stkes = null)
        //{
        //    //创建一条线路
        //    await CreatRoute(routeId, 1, dbName);
        //    DataColumn dc1 = new DataColumn("Stake", Type.GetType("System.String"));
        //    DataColumn dc2 = new DataColumn("H", Type.GetType("System.Double"));
        //    dt.Columns.Add(dc1);
        //    dt.Columns.Add(dc2);

        //    am.SetStakeParameter(true, true, false, true);
        //    int j = 0;
        //    if (stkes.Length > 0)
        //    {
        //        for (j = 0; j < stkes.Length; j++)
        //        {
        //            am.AppendAddiontalStake(stkes[j].ToString());
        //        }
        //    }
        //    if (Convert.ToDouble(beginStake) > Convert.ToDouble(endStake))
        //    {
        //        Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
        //        for (Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M >= end.M; sk = am.GetPreviousStake(sk, interval))
        //        {
        //            //rd.CalcCenterCoord(i.ToString(), out x_z, out y_z);

        //            DataRow dr = dt.NewRow();
        //            dr["Stake"] = am.GetKString(sk.S, 3);
        //            dr["H"] = am.CalcCenterHight(sk.S.ToString()).ToString("F4");
        //            dt.Rows.Add(dr);
        //            //sk.S = Begin_End_Stake_Set(beginStake, endStake, interval, sk.S);
        //            if (sk.S == Convert.ToDouble(endStake))
        //            {
        //                break;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        Stake end = am.Stake2Mileage(Convert.ToDouble(endStake));
        //        for (Stake sk = am.Stake2Mileage(Convert.ToDouble(beginStake)); sk.M <= end.M; sk = am.GetNextStake(sk, interval))
        //        {
        //            DataRow dr = dt.NewRow();
        //            dr["Stake"] = am.GetKString(sk.S, 3);
        //            dr["H"] = am.CalcCenterHight(sk.S.ToString()).ToString("F4");
        //            dt.Rows.Add(dr);
        //            if (sk.S == Convert.ToDouble(endStake))
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    List<CenterH> list = new List<CenterH>();
        //    list = ToList<CenterH>(dt);

        //    return list;
        //}

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

        //public static List<SideCoord> ConvertTo(DataTable dt)
        //{
        //    if (dt == null) return null;
        //    if (dt.Rows.Count <= 0) return null;

        //    List<SideCoord> list = new List<SideCoord>();
        //    list = (from DataRow dr in dt.Rows
        //            select new SideCoord
        //            {
        //                Stake = dr["Stake"].ToString().Trim(),
        //                //Stake = Int32.Parse(dr["Stake"].ToString()),
        //                Dist = Convert.ToDouble(dr["Dist"]),
        //                X = Convert.ToDouble(dr["X"]),
        //                Y = Convert.ToDouble(dr["Y"])
        //            }).ToList();
        //    return list;
        //}
    }
}
