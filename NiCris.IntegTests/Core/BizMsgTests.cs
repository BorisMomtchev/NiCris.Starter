using System;
using System.Collections.Generic;
using NiCris.BusinessObjects;
using NiCris.DataAccess.Interfaces;
using NiCris.DataAccess.SQL.Repositories;
using NUnit.Framework;

namespace NiCris.IntegTests.Core
{
    [TestFixture]
    public class BizMsgTests
    {
        IBizMsgRepository GetRepository()
        {
            return new BizMsgRepository();
        }

        [Test]
        public void BizMsg_CRUD_Test()
        {
            // Arrange -> Act -> Assert
            var repository = GetRepository();

            BizMsg newBizMsg = new BizMsg
            {
                // All required fields have valid values here...
                Name = "Name:XYZ",
                Date = DateTime.Now,
                User = "User:XYZ",
            };

            // save to db
            int id = repository.Insert(newBizMsg);

            // get from db
            BizMsg savedBizMsg = repository.GetById(id);
            Assert.AreEqual("Name:XYZ", savedBizMsg.Name);

            // update
            savedBizMsg.Name = "UPDATED_Name:XYZ";
            int id_updated = repository.Update(savedBizMsg);
            Assert.AreEqual(id, id_updated);

            // delete
            repository.Delete(savedBizMsg);
        }

        [Test]
        public void BizMsg_Populate()
        {
            // Arrange -> Act -> Assert
            var repository = GetRepository();

            var result = new List<BizMsg>();
            for (int i = 0; i < 20; i++)
            {
                result.Add(new BizMsg()
                {
                    // Id = i,
                    Name = "Name: " + i.ToString(),
                    Date = DateTime.Now.AddDays(-i),
                    User = "User: " + i.ToString()
                });
            }

            // Save to db
            foreach (var item in result)
                repository.Insert(item);
        }


        [Test]
        public void BizMsg_Delete()
        {
            // Arrange -> Act -> Assert
            var repository = GetRepository();

            var all = repository.GetAll();

            foreach (var item in all)
                repository.Delete(item);
        }

    }
}
