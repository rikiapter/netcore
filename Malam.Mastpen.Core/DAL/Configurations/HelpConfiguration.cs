using System;
using System.Collections.Generic;
using System.Text;

namespace Malam.Mastpen.Core.DAL.Configurations
{
    class HelpConfiguration
    {
///ניתן להריץ את השאילתא הבאה ב 
///sql
///כדי להוציא את כל השדות בכל טבלה מסור ל
///builder

//  declare @TableName sysname = 'TableName'
//declare @Result varchar(max)=  ''


//select @Result = @Result +
//     'builder.Property(p => p.' + ColumnName + ').HasColumnType("' + ColumnType + '").IsRequired();'

//from
//(
//    select
//         col.name as ColumnName,
//        column_Id ColumnId,
//        typ.name as ColumnType,
//        case 
//            when col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueIdentifier')
//            then '?' 
//            else '' 
//        end NullableSign
//    from sys.columns col
//        join sys.types typ on
//            col.system_type_Id = typ.system_type_Id AND col.user_type_Id = typ.user_type_Id
//    where object_Id = object_Id(@TableName)
//) t
//order by ColumnId

//print @Result
    }
}
