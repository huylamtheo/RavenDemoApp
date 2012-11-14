﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIT.NoSQL.Core.IService;
using UIT.NoSQL.Core.Domain;
using Raven.Client;

namespace UIT.NoSQL.Service
{
    public class GroupService : IGroupService
    {
        private IDocumentSession session;

        public GroupService(IDocumentSession session)
        {
            this.session = session;
        }

        public GroupObject Load(string id)
        {
            return session.Load<GroupObject>(id);
        }
        
        public GroupObject LoadWithUser(string id)
        {
            var groupObject = session.Include<GroupObject>(u => u.Id).Load(id);

            foreach (var userGroup in groupObject.ListUserGroup)
            {
                var user = session.Load<UserObject>(userGroup.UserId);
                userGroup.User = user;
            }

            return groupObject;
        }

        public GroupObject LoadByUser(string userId)
        {
            //var userGroup = session.Include<UserGroupObject>(u => u.GroupId).Where(u => u.UserId == userId);
            //return session.Query<GroupObject>().Where(g => g.);
            return null;
        }

        public void Save(GroupObject group)
        {
            session.Store(group);
            session.SaveChanges();
        }

        public List<GroupObject> GetAll()
        {
            var groups = session.Query<GroupObject>();
            return groups.ToList();
        }

        public List<GroupObject> GetByUser(string userId)
        {
            //var groups = session.Query<GroupObject>().Where;
            //return groups.ToList();
            return null;
        }


        public GroupObject LoadWithUser(string groupID, out List<UserObject> listUser, out List<UserGroupObject> listUserGroup)
        {
            listUser = new List<UserObject>();
            listUserGroup = new List<UserGroupObject>();
            var group = session.Include<GroupObject>(u => u.Id).Load(groupID);
            if (group == null)
            {
                return null;
            }

            listUserGroup = group.ListUserGroup;
            foreach (var userGroup in group.ListUserGroup)
	        {
                var user = session.Load<UserObject>(userGroup.UserId);
                listUser.Add(user);
	        }            

            return group;
        }
    }
}
