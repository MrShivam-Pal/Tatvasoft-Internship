﻿using Data_Logic_Layer.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Logic_Layer
{
    public class DALAdminUser
    {
        private readonly AppDbContext _context;

        public DALAdminUser(AppDbContext context)
        {
            _context = context;
        }

        public string AddUser(User user)
        {
            var result = "";
            try
            {
                var userEmailExists = _context.User.FirstOrDefault(x => !x.IsDeleted && x.EmailAddress == user.EmailAddress);
                if (userEmailExists == null)
                {
                    var newUser = new User
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        Password = user.Password,
                        UserType = user.UserType,
                        CreatedDate = DateTime.UtcNow,
                        IsDeleted = false,
                    };
                    _context.User.Add(newUser);
                    _context.SaveChanges();
                    var maxEmployeeId = 0;
                    var lastUserDetail = _context.UserDetail.ToList().LastOrDefault();

                    if (lastUserDetail != null)
                    {
                        maxEmployeeId = Convert.ToInt32(lastUserDetail.EmployeeId);
                    }
                    int newEmployeeId = maxEmployeeId + 1;
                    var existingUser = _context.User.FirstOrDefault(x => x.EmailAddress == user.EmailAddress);
                    var newUserDetail = new UserDetail
                    {
                        UserId = existingUser.Id,
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        PhoneNumber = user.PhoneNumber,
                        EmailAddress = user.EmailAddress,
                        UserType = user.UserType,
                        Name = user.FirstName,
                        Surname = user.LastName,
                        EmployeeId = newEmployeeId.ToString(),
                        Department = "IT",
                        Status = true
                    };
                    _context.UserDetail.Add(newUserDetail);
                    _context.SaveChanges();

                    result = "User Add Suceessfully.";
                }
                else
                {
                    result = "Email is already exists.";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        public List<UserDetail> GetUserList()
        {
            var userDetailList = from u in _context.User
                                 join ud in _context.UserDetail on u.Id equals ud.UserId into userDetailGroup
                                 from userDetail in userDetailGroup.DefaultIfEmpty()
                                 where !u.IsDeleted && u.UserType == "user" && !userDetail.IsDeleted
                                 select new UserDetail
                                 {
                                     Id = u.Id,
                                     FirstName = u.FirstName,
                                     LastName = u.LastName,
                                     PhoneNumber = u.PhoneNumber,
                                     EmployeeId = userDetail.EmployeeId,
                                     Department = userDetail.Department,
                                     Status = userDetail.Status,
                                 };
            return userDetailList.ToList();
        }

        public async Task<string> DeleteUser(int userId)
        {
            try
            {
                var result = string.Empty;
                using (var transaction = await _context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var userDetail = await _context.UserDetail.FirstOrDefaultAsync(x => x.UserId == userId);
                        if (userDetail != null)
                        {
                            userDetail.IsDeleted = true;
                        }
                        var user = await _context.User.FirstOrDefaultAsync(x => x.Id == userId);
                        if (user != null)
                        {
                            user.IsDeleted = true;
                        }

                        await _context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        result = "Delete user successfully";
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        throw ex;
                    }

                }
                return result;
            }
            catch(Exception ex)
            {
                throw;
            }

        }

        public string UpdateUser(User user){
        var result = "";
        try
        {
            // Check if the user exists and is not deleted
            var existingUser = _context.User.FirstOrDefault(x => !x.IsDeleted && x.Id == user.Id);
            if (existingUser != null)
            {
                // Update the User entity
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.PhoneNumber = user.PhoneNumber;
                existingUser.EmailAddress = user.EmailAddress;
                existingUser.UserType = user.UserType;
                existingUser.ModifiedDate = DateTime.UtcNow; // Assuming you have an UpdatedDate field

                // Save changes to the User entity
                _context.SaveChanges();

                // Update the UserDetail entity
                var existingUserDetail = _context.UserDetail.FirstOrDefault(x => x.UserId == user.Id);
                if (existingUserDetail != null)
                {
                    existingUserDetail.FirstName = user.FirstName;
                    existingUserDetail.LastName = user.LastName;
                    existingUserDetail.PhoneNumber = user.PhoneNumber;
                    existingUserDetail.EmailAddress = user.EmailAddress;
                    existingUserDetail.UserType = user.UserType;
                    existingUserDetail.Name = user.FirstName; // Assuming Name is the same as FirstName
                    existingUserDetail.Surname = user.LastName; // Assuming Surname is the same as LastName
                    existingUserDetail.ModifiedDate = DateTime.UtcNow; // Assuming you have an UpdatedDate field

                    // Save changes to the UserDetail entity
                    _context.SaveChanges();

                    result = "User updated successfully.";
                }
                else
                {
                    result = "User detail not found.";
                }
            }
            else
            {
                result = "User not found.";
            }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
            }
    }
}
