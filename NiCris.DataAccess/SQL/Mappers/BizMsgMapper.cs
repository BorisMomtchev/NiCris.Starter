using System;

namespace NiCris.DataAccess.SQL.Mappers
{
    public class BizMsgMapper
    {
        public static NiCris.BusinessObjects.BizMsg ToBusinessObject(NiCris.DataAccess.SQL.LinqToSQL.BizMsg entity)
        {
            if (entity == null) return null;

            return new NiCris.BusinessObjects.BizMsg()
            {
                Id = entity.Id,
                Name = entity.Name,
                Date = entity.Date,
                User = entity.User,

                Description = entity.Description,
                AppId = entity.AppId,
                ServiceId = entity.ServiceId,
                StyleId = entity.StyleId,
                Roles = entity.Roles,

                RowVersion = VersionConverter.ToString(entity.rowversion)
            };
        }

        public static NiCris.DataAccess.SQL.LinqToSQL.BizMsg ToEntity(NiCris.BusinessObjects.BizMsg model)
        {
            if (model == null) return null;

            return new NiCris.DataAccess.SQL.LinqToSQL.BizMsg()
            {
                Id = model.Id,
                Name = model.Name,
                Date = model.Date,
                User = model.User,

                Description = model.Description,
                AppId = model.AppId,
                ServiceId = model.ServiceId,
                StyleId = model.StyleId,
                Roles = model.Roles,

                rowversion = VersionConverter.ToBinary(model.RowVersion)
            };
        }

    }
}
