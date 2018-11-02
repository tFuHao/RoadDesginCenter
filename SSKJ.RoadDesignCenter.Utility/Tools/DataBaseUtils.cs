using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SSKJ.RoadDesignCenter.Utility.Tools
{
    public static class DataBaseUtils
    {
        public static readonly string connStr = "server=139.224.200.194;port=3306;user id=root;password=SSKJ*147258369;";
        public static async Task<bool> CreateDataBase(string dataBaseName)
        {
            try
            {
                var sql = "create database " + dataBaseName;
                var con = new MySqlConnection(connStr);
                con.Open();
                var cmd = new MySqlCommand(sql, con);
                var result = cmd.ExecuteNonQuery();
                con.Close();
                return await CreateTableAsync(dataBaseName);
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static async Task<bool> DeleteDataBase(string dataBaseName)
        {
            try
            {
                var sql = "drop database " + dataBaseName;
                var con = new MySqlConnection(connStr);
                con.Open();
                var cmd = new MySqlCommand(sql, con);
                var result = await cmd.ExecuteNonQueryAsync();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private static async Task<bool> CreateTableAsync(string dataBaseName)
        {
            try
            {
                var sql = $"USE {dataBaseName};CREATE TABLE `addstake` (\r\n  `AddStakeId` varchar(50) NOT NULL,\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Stake` double(18,4) DEFAULT NULL,\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  PRIMARY KEY (`AddStakeId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'加桩\';CREATE TABLE `authorize` (\r\n  `IsHalf` int(1) DEFAULT NULL,\r\n  `AuthorizeId` varchar(50) NOT NULL,\r\n  `Category` int(11) DEFAULT NULL,\r\n  `ObjectId` varchar(50) DEFAULT NULL,\r\n  `ItemType` int(11) DEFAULT NULL,\r\n  `ItemId` varchar(50) DEFAULT NULL,\r\n  `SortCode` int(11) DEFAULT NULL,\r\n  `CreateDate` datetime DEFAULT NULL,\r\n  `CreateUserId` varchar(50) DEFAULT NULL,\r\n  PRIMARY KEY (`AuthorizeId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'权限\';CREATE TABLE `authorizedata` (\r\n  `AuthorizeDataId` varchar(50) NOT NULL,\r\n  `AuthorizeType` int(11) DEFAULT NULL,\r\n  `Category` int(11) DEFAULT NULL,\r\n  `ObjectId` varchar(50) DEFAULT NULL,\r\n  `ItemId` varchar(50) DEFAULT NULL,\r\n  `ItemName` varchar(50) DEFAULT NULL,\r\n  `ResourceId` varchar(50) DEFAULT NULL,\r\n  `IsRead` int(11) DEFAULT NULL,\r\n  `AuthorizeConstraint` varchar(200) DEFAULT NULL,\r\n  `SortCode` int(11) DEFAULT NULL,\r\n  `CreateDate` datetime DEFAULT NULL,\r\n  `CreateUserId` varchar(50) DEFAULT NULL,\r\n  PRIMARY KEY (`AuthorizeDataId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'权限数据\';CREATE TABLE `brokenchainage` (\r\n  `BrokenId` varchar(50) NOT NULL,\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `FrontStake` double(18,4) DEFAULT NULL COMMENT \'来向（断前）桩号\',\r\n  `AfterStake` double(18,4) DEFAULT NULL COMMENT \'去向（断后）桩号\',\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  `BrokenType` int(11) DEFAULT NULL,\r\n  `RawStakeBack` varchar(50) DEFAULT NULL,\r\n  PRIMARY KEY (`BrokenId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'断链\';CREATE TABLE `crosssectiongroundline` (\r\n  `CrossSectionGroundLineId` varchar(50) NOT NULL,\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `Stake` double(18,4) DEFAULT NULL,\r\n  PRIMARY KEY (`CrossSectionGroundLineId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'横断面地面线\';\r\nCREATE TABLE `crosssectiongroundlinedata` (\r\n  `CrossSectionGroundLineDataId` varchar(50) NOT NULL,\r\n  `CrossSectionGroundLineId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Dist` double(18,4) DEFAULT NULL,\r\n  `H` double(18,4) DEFAULT NULL,\r\n  PRIMARY KEY (`CrossSectionGroundLineDataId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'横断面地面线数据\';\r\nCREATE TABLE `flatcurve` (\r\n  `FlatCurveId` varchar(50) NOT NULL,\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `FlatCurveType` int(11) DEFAULT NULL COMMENT \'1交点法 2曲线要素法\',\r\n  `IntersectionNumber` int(11) DEFAULT NULL COMMENT \'交点数\',\r\n  `CurveNumber` int(11) DEFAULT NULL COMMENT \'曲线数\',\r\n  `FlatCurveLength` double(18,4) DEFAULT NULL,\r\n  `BeginStake` double(18,4) DEFAULT NULL,\r\n  `EndStake` double(18,4) DEFAULT NULL,\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  PRIMARY KEY (`FlatCurveId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'平曲线\';CREATE TABLE `flatcurve_curveelement` (\r\n  `CurveElementId` varchar(50) NOT NULL,\r\n  `FlatCurveId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Stake` double(18,4) DEFAULT NULL,\r\n  `X` double(18,4) DEFAULT NULL,\r\n  `Y` double(18,4) DEFAULT NULL,\r\n  `Azimuth` double(18,4) DEFAULT NULL COMMENT \'方位角\',\r\n  `TurnTo` int(11) DEFAULT NULL COMMENT \'线型及转向:1直线 2圆左转 3圆右转 4缓左转 5缓右转\',\r\n  `R` double(18,4) DEFAULT NULL COMMENT \'曲线半径\',\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  PRIMARY KEY (`CurveElementId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'平曲线-曲线要素法\';CREATE TABLE `flatcurve_intersection` (\r\n  `IntersectionPointId` varchar(50) NOT NULL,\r\n  `FlatCurveId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `IntersectionName` varchar(50) DEFAULT NULL COMMENT \'交点名\',\r\n  `Stake` double(18,4) DEFAULT NULL COMMENT \'桩号\',\r\n  `X` double(18,4) DEFAULT NULL COMMENT \'x坐标\',\r\n  `Y` double(18,4) DEFAULT NULL COMMENT \'y坐标\',\r\n  `R` double(18,4) DEFAULT NULL COMMENT \'圆曲半径\',\r\n  `Ls1` double(18,4) DEFAULT NULL COMMENT \'第一缓和曲线长度\',\r\n  `Ls2` double(18,4) DEFAULT NULL COMMENT \'第二缓和曲线长度\',\r\n  `Ls1R` double(18,4) DEFAULT NULL COMMENT \'起点半径\',\r\n  `Ls2R` double(18,4) DEFAULT NULL COMMENT \'终点半径\',\r\n  PRIMARY KEY (`IntersectionPointId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'平曲线-交点法\';CREATE TABLE `projectinfo` (\r\n  `ProjectId` varchar(50) NOT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `PrjName` varchar(50) DEFAULT NULL,\r\n  `Identification` varchar(50) DEFAULT NULL COMMENT \'项目唯一标识码\',\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  `OwnerUnit` varchar(50) DEFAULT NULL COMMENT \'业主单位\',\r\n  `DesignUnit` varchar(50) DEFAULT NULL COMMENT \'设计单位\',\r\n  `SupervisoryUnit` varchar(50) DEFAULT NULL COMMENT \'监理单位\',\r\n  `ConstructionUnit` varchar(50) DEFAULT NULL COMMENT \'建设单位\',\r\n  `ModifyDate` datetime DEFAULT NULL,\r\n  PRIMARY KEY (`ProjectId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'项目\';CREATE TABLE `projectlog` (\r\n  `LogId` varchar(50) NOT NULL,\r\n  `CategoryId` int(11) DEFAULT NULL,\r\n  `SourceObjectId` varchar(50) DEFAULT NULL,\r\n  `SourceContentJson` longtext,\r\n  `OperateTime` datetime DEFAULT NULL,\r\n  `OperateUserId` varchar(50) DEFAULT NULL,\r\n  `OperateAccount` varchar(50) DEFAULT NULL,\r\n  `OperateTypeId` varchar(50) DEFAULT NULL,\r\n  `OperateType` varchar(50) DEFAULT NULL,\r\n  `ModuleId` varchar(50) DEFAULT NULL,\r\n  `Module` varchar(50) DEFAULT NULL,\r\n  `IPAddress` varchar(50) DEFAULT NULL,\r\n  `Host` varchar(200) DEFAULT NULL,\r\n  `Browser` varchar(50) DEFAULT NULL,\r\n  `ExecuteResult` int(11) DEFAULT NULL,\r\n  `ExecuteResultJson` longtext,\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  `DeleteMark` int(11) DEFAULT NULL,\r\n  `EnabledMark` int(11) DEFAULT NULL,\r\n  PRIMARY KEY (`LogId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'项目日志\';CREATE TABLE `role` (\r\n  `RoleId` varchar(50) NOT NULL,\r\n  `FullName` varchar(50) DEFAULT NULL,\r\n  `SortCode` int(11) DEFAULT NULL,\r\n  `DeleteMark` int(11) DEFAULT NULL,\r\n  `EnabledMark` int(11) DEFAULT NULL,\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  `CreateDate` datetime DEFAULT NULL,\r\n  `CreateUserId` varchar(50) DEFAULT NULL,\r\n  `ModifyDate` datetime DEFAULT NULL,\r\n  `ModifyUserId` varchar(50) DEFAULT NULL,\r\n  PRIMARY KEY (`RoleId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'角色\';CREATE TABLE `route` (\r\n  `RouteId` varchar(50) NOT NULL,\r\n  `ParentId` varchar(50) DEFAULT NULL,\r\n  `ProjectId` varchar(50) DEFAULT NULL,\r\n  `RouteName` varchar(50) DEFAULT NULL,\r\n  `RouteLength` double(18,4) DEFAULT NULL,\r\n  `StartStake` double(18,4) DEFAULT NULL,\r\n  `EndStake` double(18,4) DEFAULT NULL,\r\n  `RouteType` int(11) DEFAULT NULL COMMENT \'1交点法 2曲线要素法\',\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  `DesignSpeed` varchar(50) DEFAULT NULL,\r\n  `CreateDate` datetime DEFAULT NULL,\r\n  `CreateUserId` varchar(50) DEFAULT NULL,\r\n  PRIMARY KEY (`RouteId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'线路\';CREATE TABLE `sampleline` (\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `SampleLineId` varchar(50) NOT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Stake` double(18,4) DEFAULT NULL,\r\n  `LeftOffset` double(18,4) DEFAULT NULL COMMENT \'左偏距\',\r\n  `RightOffset` double(18,4) DEFAULT NULL COMMENT \'右偏距\',\r\n  PRIMARY KEY (`SampleLineId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'采样线\';CREATE TABLE `stake` (\r\n  `StakeId` varchar(50) NOT NULL,\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Offset` double(18,4) DEFAULT NULL,\r\n  `StakeName` double(18,4) DEFAULT NULL,\r\n  `RightCorner` double(18,4) DEFAULT NULL,\r\n  PRIMARY KEY (`StakeId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'桩号\';CREATE TABLE `user` (\r\n  `UserId` varchar(50) NOT NULL,\r\n  `Account` varchar(50) DEFAULT NULL,\r\n  `Password` varchar(50) DEFAULT NULL,\r\n  `Secretkey` varchar(50) DEFAULT NULL,\r\n  `RealName` varchar(50) DEFAULT NULL,\r\n  `HeadIcon` varchar(200) DEFAULT NULL,\r\n  `Gender` int(11) DEFAULT NULL,\r\n  `Birthday` datetime DEFAULT NULL,\r\n  `Mobile` varchar(50) DEFAULT NULL,\r\n  `Email` varchar(50) DEFAULT NULL,\r\n  `ManagerId` varchar(50) DEFAULT NULL,\r\n  `RoleId` varchar(50) DEFAULT NULL,\r\n  `FirstVisit` datetime DEFAULT CURRENT_TIMESTAMP,\r\n  `LastVisit` datetime DEFAULT NULL,\r\n  `LogOnCount` int(11) DEFAULT NULL,\r\n  `DeleteMark` int(11) DEFAULT NULL,\r\n  `EnabledMark` int(11) DEFAULT NULL,\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  `CreateDate` datetime DEFAULT NULL,\r\n  `CreateUserId` varchar(50) DEFAULT NULL,\r\n  `ModifyDate` datetime DEFAULT NULL,\r\n  `ModifyUserId` varchar(50) DEFAULT NULL,\r\n  PRIMARY KEY (`UserId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'用户 \';CREATE TABLE `userrelation` (\r\n  `UserRelationId` varchar(50) NOT NULL,\r\n  `UserId` varchar(50) DEFAULT NULL,\r\n  `Category` int(11) DEFAULT NULL,\r\n  `ObjectId` varchar(50) DEFAULT NULL,\r\n  `IsDefault` int(11) DEFAULT NULL,\r\n  `SortCode` int(11) DEFAULT NULL,\r\n  `CreateDate` datetime DEFAULT NULL,\r\n  `CreateUserId` varchar(50) DEFAULT NULL,\r\n  PRIMARY KEY (`UserRelationId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'用户关系\';CREATE TABLE `verticalcurve` (\r\n  `VerticalCurveId` varchar(50) NOT NULL,\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `VerticalCurveType` int(11) DEFAULT NULL COMMENT \'竖曲线类型:1变坡点 2竖曲线要素法\',\r\n  `GradeChangePointNumber` int(11) DEFAULT NULL,\r\n  `CurveNumber` int(11) DEFAULT NULL,\r\n  `VerticalCurveLength` double(18,4) DEFAULT NULL,\r\n  `BeginStake` double(18,4) DEFAULT NULL,\r\n  `EndStake` double(18,4) DEFAULT NULL,\r\n  `Description` varchar(200) DEFAULT NULL,\r\n  PRIMARY KEY (`VerticalCurveId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'竖曲线\';CREATE TABLE `verticalcurve_curveelement` (\r\n  `CurveElementId` varchar(50) NOT NULL,\r\n  `VerticalCurveId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Stake` double(18,4) DEFAULT NULL,\r\n  `H` double(18,4) DEFAULT NULL,\r\n  `i` double(18,4) DEFAULT NULL,\r\n  `R` double(18,4) DEFAULT NULL,\r\n  `Length` double(18,4) DEFAULT NULL,\r\n  PRIMARY KEY (`CurveElementId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'竖曲线-曲线要素法\';CREATE TABLE `verticalcurve_gradechangepoint` (\r\n  `GradeChangePointId` varchar(50) NOT NULL,\r\n  `VerticalCurveId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Stake` double(18,4) DEFAULT NULL,\r\n  `H` double(18,4) DEFAULT NULL,\r\n  `R` double(18,4) DEFAULT NULL,\r\n  PRIMARY KEY (`GradeChangePointId`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'变坡点\';CREATE TABLE `verticalsectiongroundline` (\r\n  `Id` varchar(50) NOT NULL,\r\n  `RouteId` varchar(50) DEFAULT NULL,\r\n  `SerialNumber` int(11) DEFAULT NULL,\r\n  `Stake` double(18,4) DEFAULT NULL,\r\n  `H` double(18,4) DEFAULT NULL,\r\n  PRIMARY KEY (`Id`)\r\n) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT=\'纵断面地面线数据\';";

                var con = new MySqlConnection(connStr);
                con.Open();
                var cmd = new MySqlCommand(sql, con);
                var result = await cmd.ExecuteNonQueryAsync();
                con.Close();
                return true;
            }
            catch (Exception)
            {
                await DeleteDataBase(dataBaseName);
                return false;
            }
        }
    }
}
