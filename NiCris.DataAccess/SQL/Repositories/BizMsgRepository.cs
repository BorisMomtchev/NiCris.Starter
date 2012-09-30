using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using NiCris.Core.Validation;
using NiCris.DataAccess.Interfaces;
using NiCris.DataAccess.SQL.LinqToSQL;
using NiCris.DataAccess.SQL.Mappers;

namespace NiCris.DataAccess.SQL.Repositories
{
    public class BizMsgRepository : IBizMsgRepository
    {
        #region ICRUDRepository<BusinessMsg> Members

        public IList<BusinessObjects.BizMsg> GetAll()
        {
            using (Database db = DataContextFactory.CreateContext())
            {
                return db.BizMsgs.Select(x => BizMsgMapper.ToBusinessObject(x)).ToList();
            }
        }

        public BusinessObjects.BizMsg GetById(int id)
        {
            using (Database db = DataContextFactory.CreateContext())
            {
                return BizMsgMapper.ToBusinessObject(db.BizMsgs
                    .Where(x => x.Id == id)
                    .SingleOrDefault());
            }
        }

        public int Insert(BusinessObjects.BizMsg model)
        {
            // Validate the BO
            var errorInfo = Validator.Check(model);
            if (errorInfo.Count() > 0)
                throw new Exception("Validation error(s) occurred! Please make sure all validation rules are met.");

            NiCris.DataAccess.SQL.LinqToSQL.BizMsg entity = BizMsgMapper.ToEntity(model);

            using (Database db = DataContextFactory.CreateContext())
            {
                try
                {
                    db.BizMsgs.InsertOnSubmit(entity);
                    db.SubmitChanges();

                    model.Id = entity.Id;
                    model.RowVersion = VersionConverter.ToString(entity.rowversion);
                    return entity.Id;
                }
                catch (ChangeConflictException)
                {
                    foreach (ObjectChangeConflict conflict in db.ChangeConflicts)
                        conflict.Resolve(RefreshMode.KeepCurrentValues);

                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        throw new Exception("A concurrency error occurred!");
                    }

                    return entity.Id;
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error inserting the record! " + ex.Message);
                }
            }

        }

        public int Update(BusinessObjects.BizMsg model)
        {
            // Validate the BO
            var errorInfo = Validator.Check(model);
            if (errorInfo.Count() > 0)
                throw new Exception("Validation error(s) occurred! Please make sure all validation rules are met.");

            NiCris.DataAccess.SQL.LinqToSQL.BizMsg entity = BizMsgMapper.ToEntity(model);

            using (Database db = DataContextFactory.CreateContext())
            {
                try
                {
                    db.BizMsgs.Attach(entity, true);
                    db.SubmitChanges();

                    model.Id = entity.Id;
                    model.RowVersion = VersionConverter.ToString(entity.rowversion);
                    return entity.Id;
                }
                catch (ChangeConflictException)
                {
                    foreach (ObjectChangeConflict conflict in db.ChangeConflicts)
                        conflict.Resolve(RefreshMode.KeepCurrentValues);

                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        throw new Exception("A concurrency error occurred!");
                    }

                    return entity.Id;
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error updating the record! " + ex.Message);
                }
            }

        }

        public void Delete(BusinessObjects.BizMsg model)
        {
            NiCris.DataAccess.SQL.LinqToSQL.BizMsg entity = BizMsgMapper.ToEntity(model);

            using (Database db = DataContextFactory.CreateContext())
            {
                try
                {
                    db.BizMsgs.Attach(entity, false);
                    db.BizMsgs.DeleteOnSubmit(entity);
                    db.SubmitChanges();
                }
                catch (ChangeConflictException)
                {
                    foreach (ObjectChangeConflict conflict in db.ChangeConflicts)
                        conflict.Resolve(RefreshMode.KeepCurrentValues);

                    try
                    {
                        db.SubmitChanges();
                    }
                    catch (ChangeConflictException)
                    {
                        throw new Exception("A concurrency error occurred!");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception("There was an error updating the record! " + ex.Message);
                }
            }
        }

        #endregion
    }
}
