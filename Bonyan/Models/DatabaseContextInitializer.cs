using System;

namespace Models
{
    internal static class DatabaseContextInitializer
    {
        static DatabaseContextInitializer()
        {

        }

        internal static void Seed(DatabaseContext databaseContext)
        {
            InitialRoles(databaseContext);
            InitialMessageStatus(databaseContext);
        }

        #region Role
        public static void InitialRoles(DatabaseContext databaseContext)
        {
            InsertRole("5ddf1dee-cae1-44cc-8ed7-09ec28f7bbf8", "superadministrator", "راهبر ویژه", databaseContext);
            InsertRole("80322f1e-8192-4c62-a826-edd9912fd4c9", "administrator", "راهبر", databaseContext);
            InsertRole("84e7e52e-2f40-4c35-8f17-360c90835906", "customet", "مشتری", databaseContext);
        }

        public static void InsertRole(string roleId, string roleName, string roleTitle, DatabaseContext databaseContext)
        {
            Guid id = new Guid(roleId);
            Role role = new Role();
            role.Id = id;
            role.Title = roleTitle;
            role.Name = roleName;
            role.CreationDate = DateTime.Now;
            role.IsActive = true;
            role.IsDeleted = false;

            databaseContext.Roles.Add(role);
            databaseContext.SaveChanges();
        }
        #endregion

       
        #region MessageStatus
        public static void InitialMessageStatus(DatabaseContext databaseContext)
        {
           InsertMessageStatus(Guid.NewGuid(),1, "Waiting", "در انتظار پاسخ", databaseContext);
           InsertMessageStatus(Guid.NewGuid(),2, "InProgress", "در دست بررسی", databaseContext);
            InsertMessageStatus(Guid.NewGuid(),3, "Responsed", "پاسخ داده شد", databaseContext);
        }

        public static void InsertMessageStatus(Guid id,int order, string title, string titleEn, DatabaseContext databaseContext)
        {
            MessageStatus messageStatus = new MessageStatus();
            messageStatus.Id = id;
            messageStatus.Title = title;
            messageStatus.TitleEn = titleEn;
            messageStatus.Order = order;
            messageStatus.CreationDate = DateTime.Now;
            messageStatus.IsActive = true;
            messageStatus.IsDeleted = false;

            databaseContext.MessageStatuses.Add(messageStatus);
            databaseContext.SaveChanges();
        }
        #endregion

       
    }
}
