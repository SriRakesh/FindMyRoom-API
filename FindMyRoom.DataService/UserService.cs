using FindMyRoom.Entities.Models;
using FindMyRoom.Entities;
using FindMyRoom.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Data.SqlClient;
using FindMyRoom.Entities;

namespace FindMyRoom.DataService
{
    public class UserService
    {
     

        FindMyRoomDb roomDb = new FindMyRoomDb();

        //This method will get the data from database
        public IEnumerable<User> Getdata()
        {
            try
            {
                return roomDb.FMRUsers;
            }
            catch (RetrievingDataException ex)
            {
                throw ex;
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        //This method will post the data to database

        public void PostUser(User user)
        {
            try
            {
                roomDb.FMRUsers.Add(user);
                roomDb.SaveChanges();
            }
            catch (InsertingDataException ex)
            {
                throw ex;
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public virtual List<User> GetDetails()
        {
            try
            {
                using (var context = new FindMyRoomDb())
                {
                    var list = from u in context.FMRUsers select u;
                    return list.ToList<User>();
                }
            }
            catch (Exception ex)
            {
                throw new RetrievingDataException(ex.Message);
            }
        }

        public virtual User GetUserDetails(int UserId)
        {
            try
            {
                using (var context = new FindMyRoomDb())
                {
                    User list = (from u in context.FMRUsers where u.UserId==UserId select u).SingleOrDefault();
                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual string ForgetPasswordService(string email, string password)
        {
            try
            {
                int count = 0;
                foreach (User user in roomDb.FMRUsers)
                {
                    //if (user.UserEmail.Equals(email))
                    if(string.Equals(email, user.UserEmail, StringComparison.OrdinalIgnoreCase))
                    {
                        user.UserPassword = password;
                        count++;
                        break;
                    }
                }
                roomDb.SaveChanges();
                if (count == 0)
                {
                    return null;
                }
                return "success";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual string EmailPassword(string email)
        {
            try
            {
                //string emailtemp = email.ToLower();
                foreach (User user in roomDb.FMRUsers)
                {
                    //if (user.UserEmail.Equals(email))
                    if(string.Equals(email,user.UserEmail,StringComparison.OrdinalIgnoreCase))
                    {
                        //string password = user.UserPassword;
                        //return password;
                        return "Done";
                    }
                }
                return null;
            }
            catch (SqlException sqlex)
            {
                throw sqlex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual Acknowledgement<User> postData(Fbuser fbuser)
        {
            List<User> list = new List<User>();
            Acknowledgement<User> payload = new Acknowledgement<User>();
            try
            {
                int count = 0;
                list = GetDetails();
                foreach (User user in list)
                {
                    if (user.UserEmail.Equals(fbuser.UserEmail,
                        StringComparison.OrdinalIgnoreCase))
                    {
                        count++;
                    }
                }
                if (count == 0)
                {
                    User fbusers = new User
                    {
                        UserEmail = fbuser.UserEmail,
                        UserName = fbuser.UserName,
                        UserAddress = "From fb",
                        UserPassword = "Facebook@123",
                        UserPhoneNumber = "**********",
                        UserType = fbuser.UserType,
                        UserStatus="valid",
                    };
                    roomDb.FMRUsers.Add(fbusers);
                    roomDb.SaveChanges();
                }
                List<User> lists = new List<User>();
                int id = 0;
                foreach (User user in roomDb.FMRUsers)
                {
                    if (user.UserEmail.Equals(fbuser.UserEmail)&&(user.UserType.Equals(fbuser.UserType)))
                    {
                        lists.Add(user);
                        id = user.UserId;
                    }
                }
                int countloginuser = 0;
                foreach (var item in roomDb.FMRSociallogin)
                {
                    if (fbuser.ProviderId.Equals(item.ProviderId))
                    {
                        countloginuser++;
                    }
                }
                if (countloginuser == 0)
                {
                    SocialLogin socialLogin = new SocialLogin
                    {
                        ProviderName = fbuser.Provider,
                        ProviderId = fbuser.ProviderId,
                        UserId = id,

                    };
                    roomDb.FMRSociallogin.Add(socialLogin);
                    roomDb.SaveChanges();
                   
                }
                if (lists.Count != 0)
                {
                    payload.code = 1;
                    payload.Set = lists;
                    payload.Message = "Successfully Loggedin";
                    return payload;
                }
                else
                {
                    payload.code = 2;
                    payload.Set = null;
                    payload.Message = "Please Login as Valid User";
                    return payload;
                }


            }
            catch(Exception e)
            {
                throw e;
            }
            //return payload;
            
        }
        

        public virtual void UpdateUser(UserPutHelp user)
        {
            try
            {
                User users=roomDb.FMRUsers.Find(user.UserId);
                if (users == null)
                {
                    return;
                }
                users.UserId = user.UserId;
                users.UserName = user.UserName;
                users.UserAddress = user.UserAddress;
                users.UserPhoneNumber = user.UserPhoneNumber;
                roomDb.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
