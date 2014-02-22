using System;
using System.Data;
using NHibernate;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;
using Peach.Core.SemVer;

namespace Peach.Data.Sql.UserTypes
{
    public class SemanticVersionType : IUserType
    {
        public object Assemble(object cached, object owner)
        {
            return cached;
        }

        public object DeepCopy(object value)
        {
            if (value == null) return null;
            return new SemanticVersion(value.ToString());
        }

        public object Disassemble(object value)
        {
            return value;
        }

        public new bool Equals(object x, object y)
        {
            return x != null && x.Equals(y);
        }

        public int GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        public bool IsMutable
        {
            get { return false; }
        }

        public object NullSafeGet(IDataReader rs, string[] names, object owner)
        {
            var versionString = (string)NHibernateUtil.String.NullSafeGet(rs, names[0]);
            return new SemanticVersion(versionString);
        }

        public void NullSafeSet(IDbCommand cmd, object value, int index)
        {
            if (value == null)
            {
                NHibernateUtil.String.NullSafeSet(cmd, null, index);
                return;
            }

            NHibernateUtil.String.NullSafeSet(cmd, value.ToString(), index);
        }

        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        public Type ReturnedType
        {
            get { return typeof (SemanticVersion); }
        }

        public SqlType[] SqlTypes
        {
            get { return new[] {new SqlType(DbType.String)}; }
        }
    }
}
